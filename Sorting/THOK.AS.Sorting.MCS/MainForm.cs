using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.MCP;
using System.Diagnostics;


namespace THOK.AS.Sorting.MCS
{
    public partial class MainForm : Form
    {
        private Rectangle tabArea;
        private RectangleF tabTextArea;
        private Context context = null;
        
        public MainForm()
        {            
            InitializeComponent();       
        }

        private void CreateDirectory(string directoryName)
        {
            if (!System.IO.Directory.Exists(directoryName))
                System.IO.Directory.CreateDirectory(directoryName);
        }

        private void WriteLoggerFile(string text)
        {
            try
            {
                string path = "";
                CreateDirectory("日志");
                path = "日志";
                path = path + @"/" + DateTime.Now.ToString().Substring(0, 4).Trim();
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToString().Substring(0, 7).Trim();
                path = path.TrimEnd(new char[] { '-'});
                CreateDirectory(path);
                path = path + @"/" + DateTime.Now.ToShortDateString() + ".txt";
                System.IO.File.AppendAllText(path, string.Format("{0} {1}", DateTime.Now, text + "\r\n"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        void Logger_OnLog(THOK.MCP.LogEventArgs args)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new LogEventHandler(Logger_OnLog), args);
            }
            else
            {
                lock (lbLog)
                {
                    string msg = string.Format("[{0}] {1} {2}", args.LogLevel, DateTime.Now, args.Message);
                    lbLog.Items.Insert(0, msg);
                    WriteLoggerFile(msg);
                }
            }
        }

        /// <summary>
        /// 自绘TabControl控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabLeft_DrawItem(object sender, DrawItemEventArgs e)
        {
            tabArea = tabLeft.GetTabRect(e.Index);
            tabTextArea = tabArea;
            Graphics g = e.Graphics;
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Font font = this.tabLeft.Font;
            SolidBrush brush = new SolidBrush(Color.Black);
            g.DrawString(((TabControl)(sender)).TabPages[e.Index].Text, font, brush, tabTextArea, sf);  
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tabLeft.DrawMode = TabDrawMode.OwnerDrawFixed;

            try
            {               
                Logger.OnLog += new LogEventHandler(Logger_OnLog);
                if (Init())
                {
                    context = new Context();
                    context.RegisterProcessControl(sortingStatus);
                    context.RegisterProcessControl(monitorView);
                    context.RegisterProcessControl(buttonArea);

                    ContextInitialize initialize = new ContextInitialize();
                    initialize.InitializeContext(context);
                }

            }
            catch (Exception ex)
            {
                Logger.Error("初始化处理失败请检查配置，原因：" + ex.Message);
            }     
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (context != null)
            {
                context.Release();
            }            
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseReason abc = e.CloseReason;
            if (abc == CloseReason.UserClosing)
                e.Cancel = true;
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            lblTitle.Left = (pnlTitle.Width - lblTitle.Width) / 2;
        }

        #region  程序运行控制只允许一个进程运行。

        string appName = "THOK.AS.Sorting.MCS";

        private bool Init()
        {
            if (System.Diagnostics.Process.GetProcessesByName(appName).Length > 1)
            {
                if (MessageBox.Show("程序已启动，将自动退出本程序！", appName , MessageBoxButtons.OK).ToString() == "OK")
                {
                    Application.Exit();
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}