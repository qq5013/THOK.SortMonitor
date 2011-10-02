using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.MCP;

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
            try
            {
                Logger.OnLog += new LogEventHandler(Logger_OnLog);

                context = new Context();
                context.RegisterProcessControl(sortingStatus);

                ContextInitialize initialize = new ContextInitialize();
                initialize.InitializeContext(context);
                context.RegisterProcessControl(monitorView);
                context.RegisterProcessControl(buttonArea);
            }
            catch (Exception e)
            {
                Logger.Error("初始化处理失败请检查配置，原因：" + e.Message);
            }            
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
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            context.Release();
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
    }
}