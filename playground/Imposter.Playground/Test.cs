namespace Imposter.Playground;

internal class T 
{
    void A(out int arg)
    {
        var a = new ArgsClass(arg);
    }
}

public class ArgsClass
{
    public ArgsClass(int outParam)
    {
        OutParam = outParam;
    }

    public int OutParam { get; set; }
}