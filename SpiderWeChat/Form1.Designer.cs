namespace SpiderWeChat
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_CloseConnect = new System.Windows.Forms.Button();
            this.txt_Msg = new System.Windows.Forms.TextBox();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(552, 25);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 49);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "开始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_CloseConnect
            // 
            this.btn_CloseConnect.Location = new System.Drawing.Point(680, 28);
            this.btn_CloseConnect.Name = "btn_CloseConnect";
            this.btn_CloseConnect.Size = new System.Drawing.Size(75, 42);
            this.btn_CloseConnect.TabIndex = 1;
            this.btn_CloseConnect.Text = "停止";
            this.btn_CloseConnect.UseVisualStyleBackColor = true;
            this.btn_CloseConnect.Click += new System.EventHandler(this.btn_CloseConnect_Click_1);
            // 
            // txt_Msg
            // 
            this.txt_Msg.Location = new System.Drawing.Point(43, 118);
            this.txt_Msg.Multiline = true;
            this.txt_Msg.Name = "txt_Msg";
            this.txt_Msg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_Msg.Size = new System.Drawing.Size(730, 530);
            this.txt_Msg.TabIndex = 2;
            // 
            // txt_IP
            // 
            this.txt_IP.Location = new System.Drawing.Point(104, 40);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(401, 21);
            this.txt_IP.TabIndex = 3;
            this.txt_IP.Text = "wss://ws03.wxb.com/web/8f2b8ab60b6e10bc6397223bcea6b602?t=1590041530&uid=1857413&" +
    "suid=0";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Location = new System.Drawing.Point(41, 43);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(53, 12);
            this.lbl1.TabIndex = 4;
            this.lbl1.Text = "服务器：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 694);
            this.Controls.Add(this.lbl1);
            this.Controls.Add(this.txt_IP);
            this.Controls.Add(this.txt_Msg);
            this.Controls.Add(this.btn_CloseConnect);
            this.Controls.Add(this.btn_Start);
            this.Name = "Form1";
            this.Text = "微信";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_CloseConnect;
        private System.Windows.Forms.TextBox txt_Msg;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.Label lbl1;
    }
}

