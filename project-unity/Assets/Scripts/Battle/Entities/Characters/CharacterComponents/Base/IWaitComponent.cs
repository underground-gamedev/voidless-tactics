using System.Collections;

namespace Battle
{
    public interface IWaitComponent
    {
        bool WaitAvailable();
        IEnumerator Wait();
    }
}