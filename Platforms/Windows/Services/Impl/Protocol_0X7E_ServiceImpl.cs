namespace MeterApp.Platforms.Windows.Services.Impl;

class Protocol_0X7E_ServiceImpl : IProtocolService
{
    public int Length => 20;

    public SerialPortResult? Parser(byte[] source)
    {
        if (!(source[0] == 0X02 && source[18] == 0X0D)) return null;//格式不符，丢弃
        char[] w = new char[9];
        Array.Copy(source, 6, w, 0, 9);
        string wh = new string(w).Trim();
        double wht = double.Parse(wh);
        return new SerialPortResult() { Unit = "kg", Weight = wht };
    }
}
