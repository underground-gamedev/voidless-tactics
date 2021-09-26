using System.Collections;
using UnityEngine;

/*
namespace Battle
{
    [RequireComponent(typeof(Character))]
    public class WaitComponent : MonoBehaviour, IWaitComponent, IRoundWatcher
    {
        private bool waitUsed;
        private Character character;

        public void Start()
        {
            character = GetComponent<Character>();
        }

        public IEnumerator Wait()
        {
            waitUsed = true;
            yield break;
        }

        public bool WaitAvailable()
        {
            return !waitUsed;
        }

        public void OnRoundStart()
        {
            waitUsed = false;
        }

        public void OnRoundEnd()
        {
        }
    }
}
*/