using MeterApp.Platforms.Windows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterApp.Platforms.Windows;

/// <summary>
/// Com串口初始化委托
/// </summary>
public delegate void ComInitEventHandler(ComInitEventArgs e);

/// <summary>
/// Com串口数据解析
/// </summary>
public delegate void ComWeighEventHandler(ComWeighEventArgs e);

/// <summary>
/// 获取串口数据
/// </summary>
/// <param name="e"></param>
public delegate void comDataRecivingHandler(comDataRecivingArgs e);

/// <summary>
/// 获取串口接收数据时间
/// </summary>
/// <param name="e"></param>
public delegate void comRecivingDateHandler(comRecivingDateArgs e);

public delegate void DelegateNetStatuChange(string status, string message);

/// <summary>
/// 二维码信息读取
/// </summary>
/// <param name="e"></param>
public delegate void ComBarEventHandler(comBarDataArgs e);

/// <summary>
/// 串口初始化事件数据
/// </summary>
public class ComInitEventArgs : System.EventArgs
{
    #region 事件数据

    /// <summary>
    /// 设备端口
    /// </summary>
    private string portName;

    /// <summary>
    /// 是否成功
    /// </summary>
    private Boolean success;
    // <summary>
    /// 对外传递的信息
    /// </summary>
    private String message;

    public ComInitEventArgs(string portName, Boolean success, String message)
    {
        this.portName = portName;
        this.success = success;
        this.message = message;
    }

    public string PortName
    {
        get { return this.portName; }
    }

    public Boolean Success
    {
        get { return this.success; }
    }

    public string Message
    {
        get { return this.message; }
    }


    #endregion
}

/// <summary>
///    解析完称的数据后，往外抛送数据
/// </summary>
public class ComWeighEventArgs : System.EventArgs
{
    private String weight;//重量
    private String unit;//单位
    private String portName;//设备号

    public ComWeighEventArgs(String portName, String weight, String unit)
    {
        this.portName = portName;
        this.weight = weight;
        this.unit = unit;
    }

    public ComWeighEventArgs(SerialPortResult result)
    {
        this.portName = result.Port;
        this.weight = result.Weight.ToString();
        this.unit = result.Unit;
    }

    public string Weight
    {
        get { return this.weight; }
    }

    public String Unit
    {
        get { return this.unit; }
    }

    public string PortName
    {
        get { return this.portName; }
    }

}

public class comDataRecivingArgs : System.EventArgs
{
    private string dataArray;//捕获数据

    public comDataRecivingArgs(string dataArray)
    {
        this.dataArray = dataArray;
    }

    public string DataArray
    {
        get { return this.dataArray; }
    }
}

public class comRecivingDateArgs : System.EventArgs
{
    private DateTime recivingTime;

    public comRecivingDateArgs(DateTime recivingTime)
    {
        this.recivingTime = recivingTime;
    }

    public DateTime RecivingTime
    {
        get { return this.recivingTime; }
    }
}

/*
 * 二维码扫描设备事件
 */
public class comBarDataArgs : System.EventArgs
{
    private String data;

    public comBarDataArgs(String data)
    {
        this.data = data;
    }

    public String Data { get { return this.data; } }
}

