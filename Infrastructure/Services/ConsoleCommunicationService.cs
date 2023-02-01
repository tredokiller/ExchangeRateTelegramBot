using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services;

public class ConsoleCommunicationService : ICommunication
{
    public void WriteLine(string s)
    {
        Console.WriteLine(s);
    }

    public void Write(string s)
    {
        Console.Write(s);
    }

    public string ReadLine()
    {
        return Console.ReadLine()!;
    }
}