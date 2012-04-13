using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using THOK.MCP.Config;
using THOK.Util;
using THOK.ParamUtil;

namespace THOK.AS.Sorting.View
{
    public partial class ParameterForm : THOK.AF.View.ToolbarForm
    {
        private Parameter parameter = new Parameter();
        private DBConfigUtil config = new DBConfigUtil("DefaultConnection", "SQLSERVER");
        private DBConfigUtil serverConfig = new DBConfigUtil("ServerConnection", "SQLSERVER");
        private DBConfigUtil stockConfig = new DBConfigUtil("StockServerConnection", "SQLSERVER");
        private THOK.MCP.Service.Package.Config.Configuration packageConfig = new THOK.MCP.Service.Package.Config.Configuration("PackPLC.xml");
        private Dictionary<string, string> attributes = null;

        public ParameterForm()
        {
            InitializeComponent();
            ReadParameter();
        }

        private void ReadParameter()
        {
            //本机数据库连接参数
            parameter.ServerName = config.Parameters["server"].ToString();
            parameter.DBName = config.Parameters["database"].ToString();
            parameter.DBUser = config.Parameters["uid"].ToString();
            parameter.Password = config.Parameters["password"].ToString();

            //服务器数据库连接参数
            parameter.RemoteServerName = serverConfig.Parameters["server"].ToString();
            parameter.RemoteDBName = serverConfig.Parameters["database"].ToString();
            parameter.RemoteDBUser = serverConfig.Parameters["uid"].ToString();
            parameter.RemotePassword = serverConfig.Parameters["password"].ToString();

            //补货系统数据库连接参数
            parameter.StockServerName = stockConfig.Parameters["server"].ToString();
            parameter.StockDBName = stockConfig.Parameters["database"].ToString();
            parameter.StockDBUser = stockConfig.Parameters["uid"].ToString();
            parameter.StockPwassword = stockConfig.Parameters["password"].ToString();

            //读取Context配置文件参数
            ConfigUtil configUtil = new ConfigUtil();
            attributes = configUtil.GetAttribute();
            parameter.ExportIP1 = attributes["ExportIP1"];
            parameter.ExportPort1 = attributes["ExportPort1"];
            parameter.ExportIP2 = attributes["ExportIP2"];
            parameter.ExportPort2 = attributes["ExportPort2"];
            parameter.LineCode = attributes["LineCode"];
            parameter.SupplyIP = attributes["SupplyIP"];
            parameter.SupplyPort = attributes["SupplyPort"];
            parameter.SortLedIP = attributes["SortLedIP"];
            parameter.SortLedPort = attributes["SortLedPort"];

            //读取包装机参数
            parameter.PortName = packageConfig.Port.ToString();
            parameter.BaudRate = packageConfig.BaudRate.ToString();
            parameter.DataBits = packageConfig.DataBits.ToString();
            parameter.Parity = packageConfig.Parity.ToString();
            parameter.StopBits = packageConfig.StopBits.ToString();   

            propertyGrid.SelectedObject = parameter;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //保存本机数据库连接参数
                config.Parameters["server"] = parameter.ServerName;
                config.Parameters["database"] = parameter.DBName;
                config.Parameters["uid"] = parameter.DBUser;
                config.Parameters["Password"] = config.Parameters["Password"].ToString() == parameter.Password ? parameter.Password : THOK.Util.Coding.Encoding(parameter.Password);
                config.Save();

                //保存服务器数据库连接参数
                serverConfig.Parameters["server"] = parameter.RemoteServerName;
                serverConfig.Parameters["database"] = parameter.RemoteDBName;
                serverConfig.Parameters["uid"] = parameter.RemoteDBUser;
                serverConfig.Parameters["Password"] = serverConfig.Parameters["Password"].ToString() == parameter.RemotePassword ? parameter.RemotePassword : THOK.Util.Coding.Encoding(parameter.RemotePassword);
                serverConfig.Save();

                //保存补货系统数据库连接参数
                stockConfig.Parameters["server"] = parameter.StockServerName;
                stockConfig.Parameters["database"] = parameter.StockDBName;
                stockConfig.Parameters["uid"] = parameter.StockDBUser;
                stockConfig.Parameters["Password"] = stockConfig.Parameters["Password"].ToString() == parameter.StockPwassword ? parameter.StockPwassword : THOK.Util.Coding.Encoding(parameter.StockPwassword);
                stockConfig.Save();

                //保存Context参数
                attributes["ExportIP1"] = parameter.ExportIP1;
                attributes["ExportPort1"] = parameter.ExportPort1;
                attributes["ExportIP2"] = parameter.ExportIP2;
                attributes["ExportPort2"] = parameter.ExportPort2;
                attributes["LineCode"] = parameter.LineCode;
                attributes["SupplyIP"] = parameter.SupplyIP;
                attributes["SupplyPort"] = parameter.SupplyPort;
                attributes["SortLedIP"] = parameter.SortLedIP;
                attributes["SortLedPort"] = parameter.SortLedPort;
                ConfigUtil configUtil = new ConfigUtil();
                configUtil.Save(attributes);

                //保存包状机参数
                packageConfig.Port = parameter.PortName;
                packageConfig.BaudRate = Convert.ToInt32(parameter.BaudRate);
                packageConfig.DataBits = Convert.ToInt32(parameter.DataBits) > 8 || Convert.ToInt32(parameter.DataBits) < 5 ? 8 : Convert.ToInt32(parameter.DataBits);
                packageConfig.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), parameter.Parity);
                packageConfig.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), parameter.StopBits);
                packageConfig.Save();



                MessageBox.Show("系统参数保存成功，请重新启动本系统。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exp)
            {
                MessageBox.Show("保存系统参数过程中出现异常，原因：" + exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
    }
}

