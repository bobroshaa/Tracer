using System.Text;

namespace Tracer.Serialization;

public class OutputSerialization : IOutputSerialization
{
    public void Output(string text, Stream stream)
    {
        byte[] buffer = Encoding.Default.GetBytes(text); 
        stream.Write(buffer, 0, buffer.Length);
    }
}