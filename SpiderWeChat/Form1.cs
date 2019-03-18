using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;

namespace SpiderWeChat
{
    public partial class Form1 : Form
    {
        //计时器  
        private System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer();
        //自动重置事件类    
        //主要用到其两个方法 WaitOne() 和 Set() , 前者阻塞当前线程，后者通知阻塞线程继续往下执行  
        AutoResetEvent autoEvent = new AutoResetEvent(false);
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            tm.Interval = 1;
            tm.Tick += new EventHandler(tm_Tick);
            btn_CloseConnect.Enabled = false;
        }
        //计时器 事件  
        void tm_Tick(object sender, EventArgs e)
        {
            if (txt_Msg.Lines.Count() > 100)
            {
                txt_Msg.Text = "";
                txt_Msg.AppendText("清空缓存" + Environment.NewLine);
            }
            autoEvent.Set(); //通知阻塞的线程继续执行  
        }

        WebSocket websocket;
 

        #region 向外传递数据事件
        public event Action<string> MessageReceived;
        #endregion
        /// <summary>
        /// 检查重连线程
        /// </summary>
        Thread _thread;
        bool _isRunning = true;
        /// <summary>
        /// WebSocket连接地址
        /// </summary>
        public string ServerPath { get; set; }


        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Start.Enabled = false;
                btn_CloseConnect.Enabled = true;
               double num = ((DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds) / 1000;
                String ServerPath = txt_IP.Text;
                if (string.IsNullOrEmpty(ServerPath)) {
                    ServerPath = "wss://ws02.wxb.com/web/8d71945f103e041f9b6f139e39b8b9f6?t=" + Convert.ToInt32(num) + "&uid=1857413&suid=0";
                }
                websocket = new WebSocket4Net.WebSocket(ServerPath);
                websocket.Opened += WebSocket_Opened;
                websocket.Closed += WebSocket_Closed;
                websocket.Error += websocket_Error;
                websocket.MessageReceived += WebSocket_MessageReceived;
                Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show("连接服务端出错:" + ex.ToString());
            }

        }

        #region "web socket "
        /// <summary>
        /// 连接方法
        /// <returns></returns>
        public bool Start()
        {
            bool result = true;
            try
            {
                websocket.Open();

                this._isRunning = true;
                this._thread = new Thread(new ThreadStart(CheckConnection));
                this._thread.Start();
            }
            catch (Exception ex)
            {
                 txt_Msg.AppendText(ex.ToString() + Environment.NewLine);
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 消息收到事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
             txt_Msg.AppendText( e.Message + Environment.NewLine);
            string content = GetStr(e.Message, "text", "}");
            if (!string.IsNullOrEmpty(content)) {
               txt_Msg.AppendText(content + Environment.NewLine);
                string regular = ".*1[2|3|4|5|6|7|8][0-9]{9}.*";
                Regex mobi = new Regex(regular);
                Match m = mobi.Match(content);
                if (m.Success) {
                    content = content.Replace("\"", "").Replace(":", "").Replace("\\n", "");
                    txt_Msg.AppendText(content + Environment.NewLine);

                    string Path = "down\\船源数据.txt";
                    if (!File.Exists(Path))
                    {
                        using (new FileStream(Path, FileMode.Create, FileAccess.Write)) { };
                    }
                    using (StreamWriter sw = new StreamWriter(Path, true, Encoding.Default))
                    {
                        sw.Write("<content>" + content + "</content>\r\n");
                    }
                      
                }
            }
         
       
            //MessageReceived?.Invoke(e.Message);
        }
        /// <summary>
        /// Socket关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_Closed(object sender, EventArgs e)
        {
             txt_Msg.AppendText("websocket_Closed" + Environment.NewLine);
        }


        /// <summary>
        /// Socket报错事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void websocket_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            txt_Msg.AppendText("websocket_Error:");
        }
        /// <summary>
        /// Socket打开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebSocket_Opened(object sender, EventArgs e)
        {
             txt_Msg.AppendText(" websocket_Opened");
        }
        /// <summary>
        /// 检查重连线程
        /// </summary>
        private void CheckConnection()
        {
            do
            {
                try
                {
                    if (websocket.State != WebSocket4Net.WebSocketState.Open && websocket.State != WebSocket4Net.WebSocketState.Connecting)
                    {
                         txt_Msg.AppendText(" Reconnect websocket WebSocketState:" + websocket.State);
                        websocket.Close();
                        websocket.Open();
                        Console.WriteLine("正在重连");
                    }
                }
                catch (Exception ex)
                {
                     txt_Msg.AppendText(ex.ToString());
                }
                System.Threading.Thread.Sleep(5000);
            } while (this._isRunning);
        }
        #endregion



        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="Message"></param>
        public void SendMessage(string Message)
        {
            Task.Factory.StartNew(() =>
            {
                if (websocket != null && websocket.State == WebSocket4Net.WebSocketState.Open)
                {
                    websocket.Send(Message);
                }
            });
        }

        public void Dispose()
        {
            this._isRunning = false;
            try
            {
                _thread.Abort();
            }
            catch
            {

            }
            websocket.Close();
            websocket.Dispose();
            websocket = null;
        }


    

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CloseConnect_Click_1(object sender, EventArgs e)
        {
            btn_Start.Enabled = true;
            btn_CloseConnect.Enabled = false;
            Dispose();


        }

        public string GetStr(string pContent, string regBegKey, string regEndKey)
        {
            string regstr = "";
            string regular = "(?<={0})(.|\n)*?(?={1})";
            regular = string.Format(regular, regBegKey, regEndKey);
            Regex regex = new Regex(regular, RegexOptions.IgnoreCase);
            Match m = regex.Match(pContent);
            if (m.Length > 0)
            {
                regstr = m.Value.Trim();
            }
            return regstr;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }


}
