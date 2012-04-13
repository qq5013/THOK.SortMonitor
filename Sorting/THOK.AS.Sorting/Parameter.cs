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
        [CategoryAttribute("ϵͳ����"), DescriptionAttribute("���ּ��ߴ���"), Chinese("�ּ��ߴ���")]
        public string LineCode
        {
            get { return lineCode; }
            set { lineCode = value; }
        }

        private string serverName;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ����������"), Chinese("����������")]
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        private string dbName;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ�����"), Chinese("���ݿ���")]
        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        private string dbUser;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ������û���"), Chinese("�û���")]
        public string DBUser
        {
            get { return dbUser; }
            set { dbUser = value; }
        }
        private string password;

        [CategoryAttribute("�������ݿ����Ӳ���"), DescriptionAttribute("���ݿ���������"), Chinese("����")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string remoteServerName;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ����������"), Chinese("����������")]
        public string RemoteServerName
        {
            get { return remoteServerName; }
            set { remoteServerName = value; }
        }

        private string remoteDBName;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ�����"), Chinese("���ݿ���")]
        public string RemoteDBName
        {
            get { return remoteDBName; }
            set { remoteDBName = value; }
        }

        private string remoteDBUser;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ������û���"), Chinese("�û���")]
        public string RemoteDBUser
        {
            get { return remoteDBUser; }
            set { remoteDBUser = value; }
        }
        private string remotePassword;

        [CategoryAttribute("���������ݿ����Ӳ���"), DescriptionAttribute("���ݿ���������"), Chinese("����")]
        public string RemotePassword
        {
            get { return remotePassword; }
            set { remotePassword = value; }
        }

        //����ϵͳ���ݿ����Ӳ���

        private string stockServerName;

        [CategoryAttribute("����ϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ����������"), Chinese("����������")]
        public string StockServerName
        {
            get { return stockServerName; }
            set { stockServerName = value; }
        }

        private string stockDBName;

        [CategoryAttribute("����ϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ�����"), Chinese("���ݿ���")]
        public string StockDBName
        {
            get { return stockDBName; }
            set { stockDBName = value; }
        }
        private string stockDBUser;

        [CategoryAttribute("����ϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ������û���"), Chinese("�û���")]
        public string StockDBUser
        {
            get { return stockDBUser; }
            set { stockDBUser = value; }
        }
        private string stockPwassword;

        [CategoryAttribute("����ϵͳ���ݿ����Ӳ���"), DescriptionAttribute("���ݿ���������"), Chinese("����")]
        public string StockPwassword
        {
            get { return stockPwassword; }
            set { stockPwassword = value; }
        }

        private string exportPort1;

        [CategoryAttribute("�����ն�ͨ�Ų���"), DescriptionAttribute("1�ų����ն˼����˿�"), Chinese("1�ż����˿�")]
        public string ExportPort1
        {
            get { return exportPort1; }
            set { exportPort1 = value; }
        }

        private string exportIP1;

        [CategoryAttribute("�����ն�ͨ�Ų���"), DescriptionAttribute("1�ų����ն�IP��ַ"), Chinese("1��IP��ַ")]
        public string ExportIP1
        {
            get { return exportIP1; }
            set { exportIP1 = value; }
        }

        private string exportPort2;

        [CategoryAttribute("�����ն�ͨ�Ų���"), DescriptionAttribute("2�ų����ն˼����˿�"), Chinese("2�ż����˿�")]
        public string ExportPort2
        {
            get { return exportPort2; }
            set { exportPort2 = value; }
        }

        private string exportIP2;

        [CategoryAttribute("�����ն�ͨ�Ų���"), DescriptionAttribute("2�ų����ն�IP��ַ"), Chinese("2��IP��ַ")]
        public string ExportIP2
        {
            get { return exportIP2; }
            set { exportIP2 = value; }
        }

        private string supplyIP;
        [CategoryAttribute("����ϵͳͨ�Ų���"), DescriptionAttribute("����ϵͳIP��ַ"), Chinese("IP��ַ")]
        public string SupplyIP
        {
            get { return supplyIP; }
            set { supplyIP = value; }
        }

        private string supplyPort;
        [CategoryAttribute("����ϵͳͨ�Ų���"), DescriptionAttribute("����ϵͳ�����˿�"), Chinese("�����˿�")]
        public string SupplyPort
        {
            get { return supplyPort; }
            set { supplyPort = value; }
        }

        private string sortLedIP;
        [CategoryAttribute("�ּ𳵼����ϵͳͨ�Ų���"), DescriptionAttribute("�ּ𳵼����ϵͳIP��ַ"), Chinese("IP��ַ")]
        public string SortLedIP
        {
            get { return sortLedIP; }
            set { sortLedIP = value; }
        }

        private string sortLedPort;
        [CategoryAttribute("�ּ𳵼����ϵͳͨ�Ų���"), DescriptionAttribute("�ּ𳵼����ϵͳ�����˿�"), Chinese("�����˿�")]
        public string SortLedPort
        {
            get { return sortLedPort; }
            set { sortLedPort = value; }
        }

        private string portName;

        [CategoryAttribute("��״��ͨ�Ų���"), DescriptionAttribute("��״�����ں�"), Chinese("���ں�")]
        public string PortName
        {
            get { return portName; }
            set { portName = value; }
        }

        private string baudRate;

        [CategoryAttribute("��״��ͨ�Ų���"), DescriptionAttribute("��״��������"), Chinese("������")]
        public string BaudRate
        {
            get { return baudRate; }
            set { baudRate = value; }
        }

        private string parity;

        [CategoryAttribute("��״��ͨ�Ų���"), DescriptionAttribute("��״������λ"), Chinese("����λ")]
        public string Parity
        {
            get { return parity; }
            set { parity = value; }
        }

        private string dataBits;

        [CategoryAttribute("��״��ͨ�Ų���"), DescriptionAttribute("��״������λ"), Chinese("����λ")]
        public string DataBits
        {
            get { return dataBits; }
            set { dataBits = value; }
        }

        private string stopBits;

        [CategoryAttribute("��״��ͨ�Ų���"), DescriptionAttribute("��״��ֹͣλ"), Chinese("ֹͣλ")]
        public string StopBits
        {
            get { return stopBits; }
            set { stopBits = value; }
        }
    }
}
