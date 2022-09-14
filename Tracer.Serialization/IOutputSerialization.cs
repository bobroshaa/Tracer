namespace Tracer.Serialization;

public interface IOutputSerialization
{
    public void Output(string text, Stream stream);
}