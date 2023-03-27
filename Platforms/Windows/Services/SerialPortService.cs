using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MeterApp.Platforms.Windows;
using MeterApp.Platforms.Windows.Models;

namespace MeterApp.Platforms.Windows.Services;

public class SerialPortService
{
    #region 事件定义
    //串口初始化完成事件
    public event ComInitEventHandler ComInitFinish;

    //从串口解析数据
    public event ComWeighEventHandler ComWeighParser;

    //接收串口数据
    public event comDataRecivingHandler comRecivingParser;

    //串口接收数据时间
    public event comRecivingDateHandler comRecivinTimeParser;

    #endregion

    private SerialPort com;
    private String portName;//串口名称COM1
    public static int baudrate;   //波特率
    public int baudrates;
    private int dataBits;   //数据位
    private int parity;     //校验位
    private int stopBits;   //停止位        

    private float into = 1;
    private float outto = 0;

    public String vendor { get; set; }//设备厂商
    public String specifications { get; set; }//设备型号

    private IProtocolService _protocolService { get; set; }

    #region 构造函数
    /// <summary>
    /// 
    /// </summary>
    /// <param name="portName">串口名称</param>
    /// <param name="baudrate">波特率</param>
    /// <param name="dataBits">数据位</param>
    /// <param name="parity">校验位</param>
    /// <param name="stopBits">停止位</param>
    public SerialPortService(ConfigureService configureService, IProtocolService protocolService)
    {
        SerialPortSetting setting = configureService.SerialPortSetting;
        com = new SerialPort();
        this.portName = setting.PortName;
        SerialPortService.baudrate = setting.Baudrate;
        this.baudrates = setting.Baudrate;
        this.dataBits = (int)setting.DataBits;
        this.parity = (int)setting.Parity;
        this.stopBits = (int)setting.StopBits;
        _protocolService = protocolService;

        //数据接收事件实现
        this.com.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.com_DataReceived);
    }
    #endregion

    #region 设置方法

    #endregion

    public void initCom()
    {
        try
        {
            //1200、7位、无校验
            com.PortName = portName;
            //
            //com.BaudRate = baudrate;
            com.BaudRate = baudrates;
            //数据位
            com.DataBits = dataBits;
            //校验位
            if (parity == 0) com.Parity = Parity.None;
            if (parity == 1) com.Parity = Parity.Odd;
            if (parity == 2) com.Parity = Parity.Even;
            if (parity == 3) com.Parity = Parity.Mark;
            if (parity == 4) com.Parity = Parity.Space;
            //停止位
            if (stopBits == 1) com.StopBits = StopBits.One;
            if (stopBits == 3) com.StopBits = StopBits.OnePointFive;
            if (stopBits == 2) com.StopBits = StopBits.Two;


            com.Open();

            if (ComInitFinish != null)
                ComInitFinish(new ComInitEventArgs(portName, true, "端口打开成功"));
        }
        catch (Exception ex)
        {
            if (ComInitFinish != null)
                ComInitFinish(new ComInitEventArgs(portName, false, ex.Message));
        }

    }
    public void openSerialPort()
    {
        com.Open();
    }
    public void closeSerialPort()
    {
        com.Close();
    }
    public bool portStust()
    {
        if (com.IsOpen)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
     * 串口接收数据
     * */
    private void com_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        //int n = com.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致  
        //byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据  
        //com.Read(buf, 0, n);//读取缓冲数据 
        DateTime recivingDatetime = DateTime.Now;
        if (comRecivinTimeParser is not null)
        {
            comRecivinTimeParser(new comRecivingDateArgs(recivingDatetime));
        }

        //天平秤在用户按打印按钮时候才往串口写数据
        //if (specifications == DeviceInfo.scales_model3 || specifications == DeviceInfo.scales_model4)
        if (false)
        {
            Thread.Sleep(1000);
        }
        else if (baudrate == 9600)
        {
            Thread.Sleep(20);
            //9600波特率
        }
        else if (baudrate == 1200)
        {
            Thread.Sleep(145);
            //1200波特率
        }

        int len = 0;

        try
        {
            len = com.BytesToRead;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        //Console.WriteLine("长度："+len);

        if (len is not 0)
        {
            byte[] buff = new byte[len];
            com.Read(buff, 0, len);


            into = into + 1;
            if (outto < 1000)
            {
                if (len == _protocolService.Length) outto++;
                float i = (outto / into) * 100;
                Console.WriteLine("命中率：" + i);
                Console.WriteLine(outto);
                Console.WriteLine("长度：" + len);
                if (comRecivingParser is not null)
                    comRecivingParser(new comDataRecivingArgs(BitConverter.ToString(buff, 0)));

            }
            else
            {
                outto = 1;
            }

            //if (len == 18) Console.WriteLine("" + BitConverter.ToString(buff, 0) + "");
            Console.WriteLine("" + BitConverter.ToString(buff, 0) + "");
            if (len == _protocolService.Length)
            {
                try
                {
                    SerialPortResult? result = _protocolService.Parser(buff);
                    if (ComWeighParser is not null)
                    {
                        if (result is not null)
                        {
                            ComWeighParser(new ComWeighEventArgs(result));
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    SerialPortResult result = new SerialPortResult() { Port = portName, Weight = -1, Unit = "kg"};
                    ComWeighParser(new ComWeighEventArgs(result));
                }
            }

        }
        return;
        //if (n == 18 && specifications == DeviceInfo.scales_model1) { parserXK3130(buf); return; }
        //if (n == 18 && specifications == DeviceInfo.scales_model2) { parserXK3130(buf); return; }
        //int n = com.BytesToRead;//先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致  
        //ComWeighParser(new ComWeighEventArgs(this.portName, "800", "T"));
        //Console.WriteLine("===========================接受数据的长度为:"+ len.ToString()+"============================");

    }


    /**
   * 托利多XK3130金鸟
   * */
    private void parserXK3130(byte[] buf)
    {
        if (!(buf[0] == 0x02 && buf[16] == 0x0D)) return;//数据协议格式不对，返回
        char[] w = new char[6];
        Array.Copy(buf, 4, w, 0, 6);
        string wh = new string(w).Trim();


        //计算小数位数
        byte b2 = buf[1];
        int digit = 0;

        if ((0x03 & b2) == 3)
        {
            digit = 1;
        }
        else if ((0x04 & b2) == 0x04)
        {
            digit = 2;
        }
        else if ((0x05 & b2) == 0x05)
        {
            digit = 3;
        }
        else if ((0x06 & b2) == 0x06) { digit = 4; }
        else if ((0x07 & b2) == 0x07) { digit = 5; }

        if (digit > 0)
            wh = wh.Insert(wh.Length - digit, ".");

        //单位
        byte b3 = buf[2];
        string b30 = (0x01 & b3) == 1 ? b30 = "净重" : b30 = "毛重";
        string b31 = (0x02 & b3) == 0x02 ? b31 = "-" : b31 = "+";
        string b32 = (0x04 & b3) == 0x04 ? b32 = "范围之外" : b32 = "未超";
        int b33 = (0x08 & b3) == 0x08 ? b33 = 1 : b33 = 0;//1动态，0稳定
        string b34 = (0x10 & b3) == 0x10 ? b34 = "kg" : b34 = "lb";

        if (b31.Equals("-"))
        {
            wh = "-" + wh;
        }

        if (ComWeighParser != null)
        {
            try
            {
                string wht = int.Parse(wh).ToString();
                ComWeighParser(new ComWeighEventArgs(this.portName, wht, b34));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                ComWeighParser(new ComWeighEventArgs(this.portName, "-1", b34));
            }
        }

    }


    /**
   * 托利多MS32001L/02
   * */
    private void parserMS32001L02_18(byte[] buf)
    {
        if (!(buf[0] == 0x53 && buf[16] == 0x0D)) return;//数据协议格式不对，返回
        char[] w = new char[12];
        Array.Copy(buf, 3, w, 0, 12);
        string wh = new string(w).Trim();
        char[] u = new char[1];
        Array.Copy(buf, 15, w, 0, 1);
        string unit = new string(u).Trim();
        string b34 = unit;
        if (ComWeighParser != null)
        {
            try
            {
                string wht = double.Parse(wh).ToString();
                ComWeighParser(new ComWeighEventArgs(this.portName, wht, b34));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                ComWeighParser(new ComWeighEventArgs(this.portName, "-1", b34));
            }
        }
    }


    private void parserMS32001L02_26(byte[] buf)
    {
        if (!(buf[24] == 0x0D)) return;//数据协议格式不对，返回
        char[] w = new char[17];
        Array.Copy(buf, 3, w, 0, 17);
        string wh = new string(w).Trim();
        char[] u = new char[4];
        Array.Copy(buf, 20, u, 0, 4);
        string unit = new string(u).Trim();
        string b34 = unit;
        if (ComWeighParser != null)
        {
            try
            {
                string wht = double.Parse(wh).ToString();
                ComWeighParser(new ComWeighEventArgs(this.portName, wh, b34));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                ComWeighParser(new ComWeighEventArgs(this.portName, "-1", b34));
            }
        }
    }

    private void parserME5002TE(byte[] buf)
    {
        int byteLength = 0;
        for (int i = 0; i < buf.Length; i++)
        {
            if (buf[i] == 0x1B)
            {
                byteLength = i;
                break;
            }
        }
        char[] w = new char[byteLength];
        Array.Copy(buf, 0, w, 0, byteLength);
        string wh = new string(w).Trim();

        if (ComWeighParser != null)
        {
            try
            {
                string wht = double.Parse(wh).ToString();
                ComWeighParser(new ComWeighEventArgs(this.portName, wh, "g"));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                ComWeighParser(new ComWeighEventArgs(this.portName, "-1", "g"));
            }
        }
    }

    private void parser0X7E(byte[] buf)
    {
        if (!(buf[0] == 0X02 && buf[18] == 0X0D)) return;//格式不符，丢弃
        char[] w = new char[9];
        Array.Copy(buf, 6, w, 0, 9);
        string wh = new string(w).Trim();
        if (ComWeighParser != null)
        {
            try
            {
                string wht = double.Parse(wh).ToString();
                ComWeighParser(new ComWeighEventArgs(this.portName, wh, "kg"));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                ComWeighParser(new ComWeighEventArgs(this.portName, "-1", "kg"));
            }
        }
    }

}

