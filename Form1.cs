
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SocketIOClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Management;
using SocketIOClient.Windows7;

namespace socketio_client
{
    public partial class Form1 : Form
    {
        private static Form1 form = null;
        public Form1()
        {
            form = this;
            InitializeComponent();
        }

        constants constOperations = new constants();
        tablolarMysqlSorguIslemleri TablesMySqlQueryOperations = new tablolarMysqlSorguIslemleri();
        mySql_islemler MySqlOperations = new mySql_islemler();
        tabloVeriEkle TableAddData = new tabloVeriEkle();
        mbox mBox = new mbox();
        LocalToWebTables localToWebTables = new LocalToWebTables();

        SocketIO socketIOLocal = new SocketIO("http://localhost:9460/");
       
        SocketIO socketIOWeb = null;    // new SocketIO("...");

        SocketClientLys webSocketIOClient = new SocketClientLys();
        SocketClientLys localSocketIOClient = new SocketClientLys();

        #region delegate operations

        string tagLoadDailyTurnOver = "";

        private delegate void sendSocketEvent(String eventName, String value);
        public static void emitLocalSocket(String eventName, String value)
        {
            if (form != null) form.emitSocketIO(eventName, value);
        }
        public void emitSocketIO(String eventName, String value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new sendSocketEvent(emitLocalSocket), new object[] { eventName , value });
                return;
            }

            socketIOLocal.EmitAsync(eventName, value);

        }

        private delegate void sendWebSocketServer(String eventName, String value);
        public static void emitWebSocketIO(String eventName, String value)
        {
            if (form != null) form.emitSocketIOweb(eventName, value);
        }
        public void emitSocketIOweb(String eventName, String value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new sendWebSocketServer(emitWebSocketIO), new object[] { eventName, value });
                return;
            }

            socketIOWeb.EmitAsync(eventName, value);

        }

        private delegate void delagateUpdateDgv(JObject obj, string eventName);   // define delagate
        public static void addRowsDGV1(JObject obj, string eventName)   // call update method
        { 
            if (form != null) form.updateStatusDGV(obj, eventName);
        }
        private void updateStatusDGV(JObject obj, string eventName)   // update method
        {
            if (InvokeRequired)
            {
                this.Invoke(new delagateUpdateDgv(addRowsDGV1), new object[] { obj, eventName });  // new delagate take
                return;
            }

            if (obj != null)
            {
                //obj["total_token_tl"].ToString()

                string toyUnitName = constOperations.oyuncakAdiGetir(obj["device_id"].ToString());
                if (toyUnitName == "-1")
                {
                    toyUnitName = obj["device_id"].ToString();
                }

                if (eventName == "unit-status")
                {
                    //unit-status:
                    //  { "company_id":"46","device_id":"46-1","socket_id":"li4sHaUvB8etOivMAAAB","cashier":"k1","total_token_tl":"620.00",
                    //  "moment_guest_count":29,"daily_guest_count":29,"daily_cancel_count":9,"total_compansation":0.0}

                    dgv_birimler.Rows[0].Selected = false;

                    dgv_birimler.Rows.Add(toyUnitName, obj["cashier"].ToString(), obj["total_token_tl"].ToString(),
                         obj["moment_guest_count"].ToString(), obj["daily_guest_count"].ToString(), obj["daily_cancel_count"].ToString(),
                         obj["total_compansation"].ToString(), DateTime.Now.ToString("HH:mm:ss"));

                    dgv_birimler.Sort(dgv_birimler.Columns["Durum"], ListSortDirection.Descending);
                    dgv_birimler.Rows[0].Selected = true;

                }
                else
                {
                    if (obj["operation"].ToString() == "PING")
                    {
                        //dataGridView1.Rows.Add(lineCounter++.ToString(), obj["device_id"].ToString(), "", "",
                        //               "", "", obj["send_data_counter"].ToString(), obj["operation"].ToString());
                        
                        string dataDgv = $"{toyUnitName}, msg_id:{obj["msg_id"]} ";

                        dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), obj["operation"].ToString(),"", dataDgv);  //"yyyy-MM-dd HH:mm:ss"

                    } 
                    else
                    {
                        if(obj["operation"].ToString() == "KART YUKLEME")
                        {
                            tagLoadDailyTurnOver = obj["total_token_tl"].ToString();
                        }

                        if (obj["operation"].ToString() == "OYUNCAK-ODEME" || obj["pay_type"].ToString() == "KART ODEME")
                        {
                            string dataDgv = $"{toyUnitName}, {obj["tag_id"]}, read:{obj["tag_read"]}, write:{obj["tag_write"]}," +
                                             $" total:{obj["total_token_tl"]} ";

                            string payType = "KART ODEME";

                            dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), obj["operation"].ToString(), payType, dataDgv); //"yyyy-MM-dd HH:mm:ss"

                            if (obj["operation"].ToString() == "OYUNCAK-ODEME") //toys operations
                            {
                                string dailyTurnOver = "";
                                string guestCount = "";
                                string toyPay = "";

                                TableAddData.getToysDailyData(obj["device_id"].ToString(), ref dailyTurnOver, ref guestCount, ref toyPay);

                                dgv_oyuncaklar.Rows[0].Selected = false;

                                dgv_oyuncaklar.Rows.Add(toyUnitName, toyPay, dailyTurnOver, obj["total_token_tl"].ToString(), guestCount, DateTime.Now.ToString("HH:mm:ss"));
                                dgv_oyuncaklar.Sort(dgv_oyuncaklar.Columns["Durum"], ListSortDirection.Descending);
                                dgv_oyuncaklar.Rows[0].Selected = true;
                            }
                           

                        }
                        if (obj["pay_type"].ToString() == "NAKIT" || obj["pay_type"].ToString() == "KKARTI")
                        {
                            string dataDgv = "";
                            if (obj["operation"].ToString() == "KART YUKLEME")
                            {
                                dataDgv = $"{toyUnitName}, {obj["tag_id"]}, read:{obj["tag_read"]}, write:{obj["tag_write"]}," +
                                             $" total:{obj["total_token_tl"]} ";
                            }
                            else
                                dataDgv = $"{toyUnitName}, time-button:{obj["time_button"]}, pay:{obj["pay"]}, total:{obj["total_token_tl"]}";

                            dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), obj["operation"].ToString(), obj["pay_type"].ToString(), dataDgv);
                        }

                        if (obj["operation"].ToString() == "KART IPTAL")
                        {
                            string dataDgv = $"{toyUnitName}, {obj["tag_id"]}, read:{obj["tag_read"]}, write:{obj["tag_write"]}," +
                                             $" total:{obj["total_token_tl"]} ";

                            dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), obj["operation"].ToString(),"KART ODEME", dataDgv);
                        }

                        if (obj["operation"].ToString() == "IPTAL NAKIT")
                        {
                            string dataDgv = $"{toyUnitName}, cancel_pay:{obj["cancel_pay"]}, total:{obj["total_token_tl"]}";

                            dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), obj["operation"].ToString(),"NAKIT", dataDgv);
                        }

                        if (obj["operation"].ToString() == "TELAFI YUKLEME")
                        {
                            string dataDgv = $"{toyUnitName}, {obj["tag_id"]}, read:{obj["tag_read"]}, write:{obj["tag_write"]}," +
                                            $" total:{obj["total_token_tl"]} ";

                            dataGridView1.Rows.Add(DateTime.Now.ToString("HH:mm:ss"), obj["operation"].ToString(), "", dataDgv);
                        }


                        dataGridView1.Rows[0].Selected = false;
                        dataGridView1.Sort(dataGridView1.Columns["Durum"], ListSortDirection.Descending);
                        dataGridView1.Rows[0].Selected = true;
                    }//else

                }//else
            }//if(obj != null)

            RemoveDuplicatesDGVRows(dataGridView1, "Veriler");
            RemoveDuplicatesDGVRows(dgv_birimler, "Birim Adı");
            RemoveDuplicatesDGVRows(dgv_oyuncaklar, "Oyuncak Adı");
        }//

        private delegate void updateSocketStatusDelegate(String text); //define delegate
        public static void updateToolStripSocketStatus(String text)    // call update method
        {
            if (form != null) form.UpdateStatusSocket(text);
        }
        public void UpdateStatusSocket(string text)  //update method define
        {
            if (InvokeRequired)
            {
                this.Invoke(new updateSocketStatusDelegate(updateToolStripSocketStatus), new object[] { text });
                return;
            }

            toolStripStatusLabel_socket.Text = text;

        }

        #endregion

        // string objStr = "";
        //int lineCounter = 0;
        // string bgwUnitOp = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
   
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Durum";
            dataGridView1.Columns[1].Name = "İşlem";
            dataGridView1.Columns[2].Name = "Ö. Türü";
            dataGridView1.Columns[3].Name = "Veriler";

            dgv_birimler.ColumnCount = 8;
            dgv_birimler.Columns[0].Name = "Birim Adı";
            dgv_birimler.Columns[1].Name = "Kasiyer";
            dgv_birimler.Columns[2].Name = "Ciro";
            dgv_birimler.Columns[3].Name = "İçerdekiler";
            dgv_birimler.Columns[4].Name = "Top. Mis.";
            dgv_birimler.Columns[5].Name = "İptal Sys.";
            dgv_birimler.Columns[6].Name = "Telafi";
            dgv_birimler.Columns[7].Name = "Durum";

            dgv_oyuncaklar.ColumnCount = 6;
            dgv_oyuncaklar.Columns[0].Name = "Oyuncak Adı";
            dgv_oyuncaklar.Columns[1].Name = "Jtn TL";
            dgv_oyuncaklar.Columns[2].Name = "Günlük Ciro";
            dgv_oyuncaklar.Columns[3].Name = "Toplam Ciro";
            dgv_oyuncaklar.Columns[4].Name = "Mis. Sys.";
            dgv_oyuncaklar.Columns[5].Name = "Durum";


            //string systemInfo = getSystemInfo();

            //if (systemInfo.Contains("Windows 7") || systemInfo.Contains("7"))
            //{
            //    socketIOLocal.ClientWebSocketProvider = () => new ClientWebSocketManaged();
            //}

            string socketWebUrl = MySqlOperations.getSocketConfig("WEB_SOCKET_URL");

            socketIOWeb = new SocketIO(socketWebUrl);

            localSocketIOClient.initSocket(socketIOLocal);
            webSocketIOClient.initSocket(socketIOWeb);
        }//load

        private void RemoveDuplicatesDGVRows(DataGridView dt, string dgvColName)
        {
            if (dt.Rows.Count > 0)
            {
                if (dt.Columns.Contains(dgvColName))
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (i == 0) break;

                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (dt.Rows[i].Cells[dgvColName].Value != null)
                            {
                                bool isEqual = false;
                                for (int k = 0; k < dt.Rows[i].Cells.Count; k++)
                                {
                                    if (dt.Rows[i].Cells[k].Value.ToString() == dt.Rows[j].Cells[k].Value.ToString()) isEqual = true;
                                    else
                                    {
                                        isEqual = false;
                                        break;
                                    }
                                }
                                if (isEqual)
                                {
                                    dt.Rows.RemoveAt(i);
                                    dt.Refresh();
                                }
                            }//if null

                        }//for
                    }//for
                }
            }
               
        }//

        private string getSystemInfo()
        {
            string r = "";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection information = searcher.Get();
                if (information != null)
                {
                    foreach (ManagementObject obj in information)
                    {
                        r = obj["Caption"].ToString() + " - " + obj["OSArchitecture"].ToString();
                    }
                }
                r = r.Replace("NT 5.1.2600", "XP");
                r = r.Replace("NT 5.2.3790", "Server 2003");

                //MessageBox.Show(r);
            }
            return r;
        }

        private async void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            await socketIOLocal.EmitAsync("led1-on");
        }
        bool led = false;
        private async void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            if (led)
            {
                await socketIOLocal.EmitAsync("led1-off");
                led = false;
                toolStripStatusLabel1.Text = "LED Sönük...";
            }
            else
            {
                await socketIOLocal.EmitAsync("led1-on");
                led = true;
                toolStripStatusLabel1.Text = "LED Yanık...";
            }

        }
    }
}
