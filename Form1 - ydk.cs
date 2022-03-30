using SocketIOClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace socketio_client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public delegate void UpdateTextBoxMethodSocket(string text);
        public delegate void UpdateTextBoxMethodDgv(JObject obj);


        SocketIO socket = new SocketIO("http://localhost:3000/");

        string objStr = "";
        int lineCounter = 0;

        private async void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Name = "SN";
            dataGridView1.Columns[1].Name = "Birim";
            dataGridView1.Columns[2].Name = "Kart ID";
            dataGridView1.Columns[3].Name = "Kart OKUMA";
            dataGridView1.Columns[4].Name = "KART YAZMA";
            dataGridView1.Columns[5].Name = "Toplam KONTUR";
            dataGridView1.Columns[6].Name = "Veri SAYICI";
            dataGridView1.Columns[7].Name = "IŞLEM";


            socket.OnConnected += Socket_OnConnected;
            socket.OnPing += Socket_OnPing;
            socket.OnPong += Socket_OnPong;
            socket.OnDisconnected += Socket_OnDisconnected;
            socket.OnReconnectAttempt += Socket_OnReconnecting;

            InitEventHandlers(socket);

           

        }//load

        async void InitEventHandlers(SocketIO _socket)
        {
            _socket.OnAny((name, response) =>
            {
                //UpdateStatusSocket(name);
                //UpdateStatusSocket(response.ToString());
            });

            //_socket.On("hi", response =>
            //{
            //    // Console.WriteLine(response.ToString());
            //    UpdateStatus(response.GetValue<string>());


            //    //Account data = JsonConvert.DeserializeObject<Account>(response.GetValue<string>()); 
            //    //string type = data.type;
            //    //string data = data.data;
            //    //UpdateStatus("type :" + data.ToString());
            //});

            _socket.On("tag-data", data =>
            {
                //UpdateStatus(data.GetValue<string>());
                // UpdateStatus(data.ToString());
                JObject obj = JObject.Parse(data.GetValue<string>());

                //objStr = obj.ToString() + "\n";// +

                //"company_id :" + obj["company_id"].ToString() + "\n"+
                //"device_id:" + obj["device_id"].ToString() + "\n"+
                //"socket_id :" + obj["socket_id"].ToString() + "\n"+
                //"tag_id :" + obj["tag_id"].ToString() + "\n"+
                //"tag_read :" + obj["tag_read"].ToString()+"\n"+
                //"tag_write :" + obj["tag_write"].ToString() + "\n" +
                //"total_token_tl :" + obj["total_token_tl"].ToString() + "\n" +
                //"send_data_counter :" + obj["send_data_counter"].ToString() + "\n" ;

                UpdateStatusDgv(obj);
                //UpdateStatus(objStr);
            });

            _socket.On("time_stamp", data =>
            {
                JObject obj = JObject.Parse(data.GetValue<string>());
                UpdateStatusDgv(obj);
            });

                //Console.WriteLine("Press any key to continue");
                //Console.ReadLine();

                await _socket.ConnectAsync();

        }
        private void UpdateStatusDgv(JObject obj)
        {
            if (this.dataGridView1.InvokeRequired)
            {
                UpdateTextBoxMethodDgv del = new UpdateTextBoxMethodDgv(UpdateStatusDgv);

                this.Invoke(del, new object[] { obj } );
            }
            else
            {
                //this.textBox2.Text = text;
                if (obj != null)
                {
                    if (obj["operation"].ToString() == "PING")
                    {
                        dataGridView1.Rows.Add(lineCounter++.ToString(), obj["device_id"].ToString(),"","",
                                       "", "", obj["send_data_counter"].ToString(), obj["operation"].ToString());
                    }
                    else
                    {
                        dataGridView1.Rows.Add(lineCounter++.ToString(), obj["device_id"].ToString(), obj["tag_id"].ToString(), obj["tag_read"].ToString(),
                                        obj["tag_write"].ToString(), obj["total_token_tl"].ToString(), obj["send_data_counter"].ToString(),
                                        obj["operation"].ToString());
                    }
                    
                }
                    


            }
        }
        private void UpdateStatusSocket(string text)
        {
            //if (this.toolStripStatusLabel.InvokeRequired)
            //{
            //    UpdateTextBoxMethodSocket del = new UpdateTextBoxMethodSocket(UpdateStatusSocket);

            //    this.Invoke(del, new object[] { text });
            //}
            //else
            {
                toolStripStatusLabel_socket.Text = text;
            }
        }

        private void Socket_OnReconnecting(object sender, int e)
        {
            // Console.WriteLine($"{DateTime.Now} Reconnecting: attempt = {e}");
            UpdateStatusSocket($"{DateTime.Now} Reconnecting: attempt = {e}");


        }

        private void Socket_OnDisconnected(object sender, string e)
        {
            UpdateStatusSocket("Disconnect: " + e);

        }

        private async void Socket_OnConnected(object sender, EventArgs e)
        {
            //Console.WriteLine("Socket_OnConnected");

            var socket = sender as SocketIO;
            //Console.WriteLine("Socket.Id:" + socket.Id);
            UpdateStatusSocket("Socket On Connected : " + socket.Id);

            //while (true)
            //{
            //    await Task.Delay(1000);
            await socket.EmitAsync("hi", DateTime.Now.ToString());
            //await socket.EmitAsync("welcome");
            //}
            //byte[] bytes = Encoding.UTF8.GetBytes("ClientCallsServerCallback_1Params_0");
            //await socket.EmitAsync("client calls the server's callback 1", bytes);
            //await socket.EmitAsync("1 params", Encoding.UTF8.GetBytes("hello world"));
        }

        private void Socket_OnPing(object sender, EventArgs e)
        {
            UpdateStatusSocket("Ping ...");
        }

        private void Socket_OnPong(object sender, TimeSpan e)
        {
            UpdateStatusSocket("Pong: " + e.TotalMilliseconds);
        }
    }
}
