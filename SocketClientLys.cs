using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SocketIOClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace socketio_client
{
    class SocketClientLys
    {

        tabloVeriEkle TableOperations = new tabloVeriEkle();
        constants constOperations = new constants();
        mySql_islemler MySqlOperations = new mySql_islemler();
        mbox mBox = new mbox();
        LocalToWebTables localToWebTables = new LocalToWebTables();
        tablolarMysqlSorguIslemleri tablesMysqlQueryOperations = new tablolarMysqlSorguIslemleri();

        public void initSocket(SocketIO socket)
        {
            socket.OnConnected += Socket_OnConnected;
            socket.OnPing += Socket_OnPing;
            socket.OnPong += Socket_OnPong;
            socket.OnDisconnected += Socket_OnDisconnected;
            socket.OnReconnectAttempt += Socket_OnReconnecting;

            InitEventHandlers(socket);
        }

        public async void InitEventHandlers(SocketIO _socket)
        {
            _socket.OnAny((name, response) =>
            {
                //UpdateStatusSocket(name);
                //UpdateStatusSocket(response.ToString());
            });

            _socket.On("tag-data", data =>
            {
                //UpdateStatus(data.GetValue<string>());

                if (data.GetValue<string>()== null) return;

                JObject obj = JObject.Parse(data.GetValue<string>());

                string[] veriler = new string[eDizi.toys_units_tracker_el_count];
               

                if (obj["operation"].ToString() == "PING")
                {
                    if (TableOperations.oyuncak_ping_guncelle(obj["device_id"].ToString(), Convert.ToInt32(constOperations.firmaWebIdGetir()), constants.LOCAL_BAGLANTI))
                    {
                        TableOperations.oyuncak_ping_guncelle(obj["device_id"].ToString(), Convert.ToInt32(constOperations.firmaWebIdGetir()), constants.UZAK_BAGLANTI);
                    }

                    Form1.addRowsDGV1(obj, "tag-data");
                }
                else
                {
                    int ioPort = data.SocketIO.ServerUri.Port;

                    if (ioPort == 9460) //local io server
                    {
                        string toyUnitName = "";
                        toyUnitName = constOperations.oyuncakAdiGetir(obj["device_id"].ToString());

                        veriler[eDizi.toys_units_tracker_firma_web_id] = obj["company_id"].ToString();
                        veriler[eDizi.toys_units_tracker_toy_unit_id] = obj["device_id"].ToString();  // coming value is hex value
                        veriler[eDizi.toys_units_tracker_toy_unit_name] = toyUnitName;
                        veriler[eDizi.toys_units_tracker_tag_id] = obj["tag_id"].ToString();
                        veriler[eDizi.toys_units_tracker_tag_read] = obj["tag_read"].ToString().Replace(".", ",");
                        veriler[eDizi.toys_units_tracker_tag_write] = obj["tag_write"].ToString().Replace(".", ",");
                        veriler[eDizi.toys_units_tracker_operation] = obj["operation"].ToString();
                        veriler[eDizi.toys_units_tracker_total_token_tl] = obj["total_token_tl"].ToString().Replace(".", ",");
                        veriler[eDizi.toys_units_tracker_date_time] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
                        veriler[eDizi.toys_units_tracker_msg_id] = obj["msg_id"].ToString();
                        veriler[eDizi.toys_units_tracker_is_web] = bool.FalseString;

                        var bw = new BackgroundWorker();  // this object workes as async.
                                                          //bw.WorkerReportsProgress= true;
                        bw.DoWork += delegate {
                            if (TableOperations.toysUnitsTrackerAddData(veriler, constants.LOCAL_BAGLANTI))
                            {
                                if (TableOperations.oyuncak_ping_guncelle(obj["device_id"].ToString(), Convert.ToInt32(constOperations.firmaWebIdGetir()), constants.LOCAL_BAGLANTI))
                                {
                                    TableOperations.oyuncak_ping_guncelle(obj["device_id"].ToString(), Convert.ToInt32(constOperations.firmaWebIdGetir()), constants.UZAK_BAGLANTI);
                                }
                            }
                            Thread.Sleep(1000);  // MySql db refresh time.  
                            if (obj["operation"].ToString()== "KART IADE")
                            {
                                tablesMysqlQueryOperations.kartIslemSil(obj["tag_id"].ToString(), "abone_takip");
                                Form1.emitWebSocketIO("card-return", obj["tag_id"].ToString());
                            }

                            if (obj["operation"].ToString() == "KART YUKLEME")
                            {
                                localToWebTables.setTableDataToWeb("satislar");
                            }
                            if (obj["operation"].ToString() == "SURELI" || obj["operation"].ToString() == "SURESIZ PESIN")
                            {
                                localToWebTables.setTableDataToWeb("misafirler");
                                localToWebTables.setTableDataToWeb("misafir_odemeler");

                                if(obj["pay_type"].ToString() == "KART ODEME")
                                    localToWebTables.setTableDataToWeb("abone_takip");
                            }
                        };
                        //bw.ProgressChanged += delegate { };
                        bw.RunWorkerCompleted += delegate {                         
                            Form1.addRowsDGV1(obj, "tag-data");
                            Form1.emitWebSocketIO("tag-data", data.GetValue<string>());                        

                            localToWebTables.delduplicateRowInAboneTakip(constants.UZAK_BAGLANTI);                                                   
                        };
                        bw.RunWorkerAsync();
                    }
                    if (ioPort == 9461) //remote server
                    {
                        // Form1.emitLocalSocket("tag-data", data.GetValue<string>());
                        if (obj["data"].ToString() == null)
                            Form1.addRowsDGV1(obj, "tag-data");
                    }
                }//else ping

            });//_socket.On("tag-data"

            _socket.On("remote-tag-data", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                //JObject obj = JObject.Parse(data.GetValue<string>());

                if (ioPort == 9461)
                    Form1.emitLocalSocket("tag-data", data.GetValue<string>());

            });

            _socket.On("cash-credit-card", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                JObject obj = JObject.Parse(data.GetValue<string>());

                if (ioPort == 9460)
                {                 
                    Form1.emitWebSocketIO("cash-credit-card", data.GetValue<string>());

                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        if (obj["operation"].ToString() == "SURELI" || obj["operation"].ToString() == "SURESIZ PESIN")
                        {
                            localToWebTables.setTableDataToWeb("misafirler");
                            localToWebTables.setTableDataToWeb("misafir_odemeler");
                        }
                    };
                    bw.RunWorkerAsync();
                }
                if (ioPort == 9461)
                {
                    //Form1.emitLocalSocket("cash-credit-card", data.GetValue<string>());
                }

                Form1.addRowsDGV1(obj, "cash-credit-card");
            });

            _socket.On("tag-cancel", data =>
            {
                JObject obj = JObject.Parse(data.GetValue<string>());
                int ioPort = data.SocketIO.ServerUri.Port;

                if (ioPort == 9460)
                {
                    Form1.emitWebSocketIO("tag-cancel", data.GetValue<string>());
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("iptal_takip");
                    };
                    bw.RunWorkerAsync();
                }

                Form1.addRowsDGV1(obj, "tag-cancel");

            });
            _socket.On("cancel", data =>
            {
                JObject obj = JObject.Parse(data.GetValue<string>());
                int ioPort = data.SocketIO.ServerUri.Port;

                if (ioPort == 9460)
                {
                    Form1.emitWebSocketIO("cancel", data.GetValue<string>());
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("iptal_takip");
                    };
                    bw.RunWorkerAsync();
                }

                Form1.addRowsDGV1(obj, "cancel");

            });

            _socket.On("unit-status", data =>
            {
                JObject obj = JObject.Parse(data.GetValue<string>());

                int ioPort = data.SocketIO.ServerUri.Port;

                if (ioPort == 9460)
                    Form1.emitWebSocketIO("unit-status", data.GetValue<string>());
                // if (ioPort == 9461) Form1.emitLocalSocket("unit-status", data.GetValue<string>());
                Form1.addRowsDGV1(obj, "unit-status");

            });

            _socket.On("subscriber-data", data =>
            {
                JObject obj = JObject.Parse(data.GetValue<string>());
                int ioPort = data.SocketIO.ServerUri.Port;

                if (ioPort == 9461)
                {
                    string[] veriler = new string[eDizi.abone_takip_eleman_sayisi];

                    veriler[eDizi.abone_takip_firma_web_id] = obj["company_id"].ToString();

                    veriler[eDizi.abone_takip_ad_soyad] = obj["misafirAdSoyad"].ToString();
                    veriler[eDizi.abone_takip_veli_ad] = obj["misafirVeli"].ToString();
                    veriler[eDizi.abone_takip_tel] = obj["misafirVeliTel"].ToString();
                    veriler[eDizi.abone_takip_abone_tur] = obj["bgwIslemTip"].ToString();//odeme tür
                    veriler[eDizi.abone_takip_abone_bas_bit_tarih] = obj["abone_bas_bit_tarih"].ToString();
                    veriler[eDizi.abone_takip_tag_id] = obj["strTag"].ToString();
                    veriler[eDizi.abone_takip_kontur_tl] = obj["kartMevcutTL"].ToString();
                    veriler[eDizi.abone_takip_durum] = obj["takip_durum"].ToString();

                    TableOperations.aboneTabloVeriKaydet(veriler, constants.LOCAL_BAGLANTI);
                }
            });

            _socket.On("cash-outflow", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                if (ioPort == 9460)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("kasa_cikis");
                    };
                    bw.RunWorkerAsync();
                }

            });

            _socket.On("create-temporary-card", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                if (ioPort == 9460)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("gecici_kart_takip");
                    };
                    bw.RunWorkerAsync();
                }

            });

            _socket.On("create-temporary-card-return", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                string val = data.GetValue<string>();

                if (ioPort == 9460)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.updateGeciciKartWeb(val.Split(',')[0], val.Split(',')[1]);
                    };
                    bw.RunWorkerAsync();
                }

            });

            _socket.On("cash-desk-transfer", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                string val = data.GetValue<string>();

                if (ioPort == 9460)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("gunluk_kasa");
                    };
                    bw.RunWorkerAsync();
                }

            });

            _socket.On("personnel-tracking", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                string val = data.GetValue<string>();

                if (ioPort == 9460)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("personel_takip");
                    };
                    bw.RunWorkerAsync();
                }

            });

            _socket.On("save-staff", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                string val = data.GetValue<string>();

                if (ioPort == 9460)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("personel_listesi");
                    };
                    bw.RunWorkerAsync();
                }

            });

            _socket.On("user-add", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                string val = data.GetValue<string>();

                if (ioPort == 9460)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("kullanicilar-delete-"+val.Split(',')[0]);
                        Thread.Sleep(1000);
                        localToWebTables.setTableDataToWeb("kullanicilar");
                    };
                    bw.RunWorkerAsync();
                }

            });

            _socket.On("product-sale", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                string val = data.GetValue<string>();

                if (ioPort == 9460)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("satislar");
                    };
                    bw.RunWorkerAsync();
                }

            });

            _socket.On("product-add", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                string val = data.GetValue<string>();

                if (ioPort == 9460)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("urun_stok");
                    };
                    bw.RunWorkerAsync();
                }

            });

            _socket.On("product-delete-update", data =>
            {
                int ioPort = data.SocketIO.ServerUri.Port;
                string val = data.GetValue<string>();

                if (ioPort == 9460)
                {
                    var bw = new BackgroundWorker();
                    bw.DoWork += delegate
                    {
                        localToWebTables.setTableDataToWeb("urun-delete-" + val);
                        Thread.Sleep(1000);
                        localToWebTables.setTableDataToWeb("urun_stok");
                    };
                    bw.RunWorkerAsync();
                }

            });


            _socket.On("time_stamp", data =>
            {
                JObject obj = JObject.Parse(data.GetValue<string>());
                Form1.addRowsDGV1(obj, "time_stamp");
            });

            await _socket.ConnectAsync();
        }
        private void Socket_OnReconnecting(object sender, int e)
        {
            var socket = sender as SocketIO;

            if (socket.ServerUri.Port == 9460)
            {
                Form1.updateToolStripSocketStatus($"{DateTime.Now} LOCAL Reconnecting: attempt = {e}");
            }
            if (socket.ServerUri.Port == 9461)
            {
                Form1.updateToolStripSocketStatus($"{DateTime.Now} WEB Reconnecting: attempt = {e}");
            }
        
        }

        private void Socket_OnDisconnected(object sender, string e)
        {
            Form1.updateToolStripSocketStatus("Disconnect: " + e);

        }
         private async void Socket_OnConnected(object sender, EventArgs e)
        {
            var socket = sender as SocketIO;
            
            // await socket.EmitAsync("hi", DateTime.Now.ToString());
            string remoteRoom = MySqlOperations.getSocketConfig("REMOTE_ROOM_ID");
            string serverCompanyId = remoteRoom.Split('-')[0];

            if (remoteRoom == "null" || remoteRoom == null)
                mBox.Show("ROOM id null olamaz", mbox.MSJ_TIP_HATA);

            if (socket.ServerUri.Port == 9460)
            {
                remoteRoom = "UNITS";
                Form1.updateToolStripSocketStatus("LOCAL:" + socket.Id);
            }

            if (socket.ServerUri.Port == 9461)
            {
                Form1.updateToolStripSocketStatus("WEB:" + socket.Id);
            }

            var myUserData = new
            {
                company_id = serverCompanyId,
                user_id = serverCompanyId,
                socket_id = socket.Id,
                room = remoteRoom,
            };

            string jsonData = JsonConvert.SerializeObject(myUserData);

            await socket.EmitAsync("login-user", jsonData);

        }

        private void Socket_OnPing(object sender, EventArgs e)
        {
            Form1.updateToolStripSocketStatus("Ping ...");
               
        }

        private void Socket_OnPong(object sender, TimeSpan e)
        {
            var socket = sender as SocketIO;
            if (socket.ServerUri.Port == 9460)
            {
                Form1.updateToolStripSocketStatus("LOCAL:" + e.TotalMilliseconds.ToString());
            }
            if (socket.ServerUri.Port == 9461)
            {
                Form1.updateToolStripSocketStatus("WEB:" + e.TotalMilliseconds.ToString());
            }

           
        }
    }//class
}
