using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class PredefinedTeamSetup : BaseTeamSetup
    {
        [OdinSerialize]
        private Dictionary<Character, Vector2Int> positions;

        public override IEnumerator SetupTeam()
        {
            var map = Team.Map;
            foreach (var character in Team.Characters)
            {
                var pos = positions[character];
                var cell = map.CellBy(pos.x, pos.y);
                character.MapObject.BindMap(Team.Map, cell);
            }

            yield break;
        }
    }
}