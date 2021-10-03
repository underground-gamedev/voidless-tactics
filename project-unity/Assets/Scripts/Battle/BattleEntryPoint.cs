using System.Collections;
using UnityEngine;

namespace Battle
{
    public class BattleEntryPoint: MonoBehaviour
    {
        [SerializeField]
        private BattleSetup setup;

        private BattleState state;
        private void Start()
        {
            StartCoroutine(RunGame());
        }

        private IEnumerator RunGame()
        {
            yield return new WaitForEndOfFrame();
            state = setup.Setup();
            state.EventQueue.Handle(new StartUtilityEvent());
        }
    }
}