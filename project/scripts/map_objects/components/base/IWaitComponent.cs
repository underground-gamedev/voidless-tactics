using System.Threading.Tasks;

public interface IWaitComponent
{
    bool WaitAvailable();
    Task Wait();
}