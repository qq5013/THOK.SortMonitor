using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using THOK.ParamUtil;

namespace THOK.AS.Sorting
{
    public class Parameter: BaseObject
    {
        private string lineCode;
        [CategoryAttribute("系统参数"), DescriptionAttribute("本分拣线代码"), Chinese("分拣线代码")]
        public string LineCode
        {
            get { return lineCode; }
            set { lineCode = value; }
        }

        private string serverName;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库服务器名称"), Chinese("服务器名称")]
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        private string dbName;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库名称"), Chinese("数据库名")]
        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        private string dbUser;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库连接用户名"), Chinese("用户名")]
        public string DBUser
        {
            get { return dbUser; }
            set { dbUser = value; }
        }
        private string password;

        [CategoryAttribute("本机数据库连接参数"), DescriptionAttribute("数据库连接密码"), Chinese("密码")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string remoteServerName;

        [CategoryAttribute("服务器数据库连接参数"), DescriptionAttribute("数据库服务器名称"), Chinese("服务器名称")]
        public string RemoteServerName
        {
            get { return remoteServerName; }
            set { remoteServerName = value; }
        }

        private string remoteDBName;

        [CategoryAttribute("服务器数据库连接参数"), DescriptionAttribute("数据库名称"), Chinese("数据库名")]
        public string RemoteDBName
        {
            get { return remoteDBName; }
            set { remoteDBName = value; }
        }

        private string remoteDBUser;

        [CategoryAttribute("服务器数据库连接参数"), DescriptionAttribute("数据库连接用户名"), Chinese("用户名")]
        public string RemoteDBUser
        {
            get { return remoteDBUser; }
            set { remoteDBUser = value; }
        }
        private string remotePassword;

        [CategoryAttribute("服务器数据库连接参数"), DescriptionAttribute("数据库连接密码"), Chinese("密码")]
        public string RemotePassword
        {
            get { return remotePassword; }
            set { remotePassword = value; }
        }

        //补货系统数据库连接参数

        private string stockServerName;

        [CategoryAttribute("补货系统数据库连接参数"), DescriptionAttribute("数据库服务器名称"), Chinese("服务器名称")]
        public string StockServerName
        {
            get { return stockServerName; }
            set { stockServerName = value; }
        }

        private string stockDBName;

        [CategoryAttribute("补货系统数据库连接参数"), DescriptionAttribute("数据库名称"), Chinese("数据库名")]
        public string StockDBName
        {
            get { return stockDBName; }
            set { stockDBName = value; }
        }
        private string stockDBUser;

        [CategoryAttribute("补货系统数据库连接参数"), DescriptionAttribute("数据库连接用户名"), Chinese("用户名")]
        public string StockDBUser
        {
            get { return stockDBUser; }
            set { stockDBUser = value; }
        }
        private string stockPwassword;

        [CategoryAttribute("补货系统数据库连接参数"), DescriptionAttribute("数据库连接密码"), Chinese("密码")]
        public string StockPwassword
        {
            get { return stockPwassword; }
            set { stockPwassword = value; }
        }

        private string exportPort;

        [CategoryAttribute("出口终端通信参数"), DescriptionAttribute("出口终端监听端口"), Chinese("监听端口")]
        public string ExportPort
        {
            get { return exportPort; }
            set { exportPort = value; }
        }

        private string exportIP;

        [CategoryAttribute("出口终端通信参数"), DescriptionAttribute("出口终端IP地址"), Chinese("IP地址")]
        public string ExportIP
        {
            get { return exportIP; }
            set { exportIP = value; }
        }

        private string supplyIP;
        [CategoryAttribute("补货系统通信参数"), DescriptionAttribute("补货系统IP地址"), Chinese("IP地址")]
        public string SupplyIP
        {
            get { return supplyIP; }
            set { supplyIP = value; }
        }

        private string supplyPort;
        [CategoryAttribute("补货系统通信参数"), DescriptionAttribute("补货系统监听端口"), Chinese("监听端口")]
        public string SupplyPort
        {
            get { return supplyPort; }
            set { supplyPort = value; }
        }

        private string sortLedIP;
        [CategoryAttribute("分拣车间大屏系统通信参数"), DescriptionAttribute("分拣车间大屏系统IP地址"), Chinese("IP地址")]
        public string SortLedIP
        {
            get { return sortLedIP; }
            set { sortLedIP = value; }
        }

        private string sortLedPort;
        [CategoryAttribute("分拣车间大屏系统通信参数"), DescriptionAttribute("分拣车间大屏系统监听端口"), Chinese("监听端口")]
        public string SortLedPort
        {
            get { return sortLedPort; }
            set { sortLedPort = value; }
        }

        private string portName;

        [CategoryAttribute("包状机通信参数"), DescriptionAttribute("包状机串口号"), Chinese("串口号")]
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }

        private string baudRate;

        [CategoryAttribute("包状机通信参数"), DescriptionAttribute("包状机波特率"), Chinese("波特率")]
        public string BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

        private string parity;

        [CategoryAttribute("包状机通信参数"), DescriptionAttribute("包状机较验位"), Chinese("较验位")]
        public string Parity
        {
            get { return parity; }
            set { parity = value; }
        }

        private string dataBits;

        [CategoryAttribute("包状机通信参数"), DescriptionAttribute("包状机数据位"), Chinese("数据位")]
        public string DataBits
        {
            get { return dataBits; }
            set { dataBits = value; }
        }

        private string stopBits;

        [CategoryAttribute("包状机通信参数"), DescriptionAttribute("包状机停止位"), Chinese("停止位")]
        public string StopBits
        {
            get { return stopBits; }
            set { stopBits = value; }
        }
    }
}
