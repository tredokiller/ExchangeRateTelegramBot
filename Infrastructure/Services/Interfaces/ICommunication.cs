namespace Infrastructure.Services;

public interface ICommunication
{
    public void WriteLine(string s);

    public void Write(string s);

    public string ReadLine();

}