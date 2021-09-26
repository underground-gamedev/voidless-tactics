using System.Collections;
using Battle.Map.Interfaces;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Battle
{
    public class BattleSetup : SerializedMonoBehaviour
    {
        [OdinSerialize]
        private IMapSetup mapSetup;
        
        private void Start()
        {
            StartCoroutine(Setup());
        }

        private IEnumerator Setup()
        {
            yield return new WaitForEndOfFrame();

            var map = mapSetup.Setup();
            
        }
    }
}