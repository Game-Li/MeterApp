namespace MeterApp.Platforms.Windows.Services;

public interface IProtocolService
{
    /// <summary>
    /// 报文长度
    /// </summary>
    public int Length {  get; }

    /// <summary>
    /// 协议解析
    /// </summary>
    /// <param name="source">源数据</param>
    /// <returns>解析后的串口数据(若解析失败，值为null)</returns>
    public SerialPortResult? Parser(byte[] source);
}
