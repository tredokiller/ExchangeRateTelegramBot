namespace Infrastructure.Services.Interfaces;

public interface ICommunication
{
    public void WriteLine(string s);

    public void Write(string s);

    public string ReadLine();

}