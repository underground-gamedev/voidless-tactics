using System.Linq;
using VoidLess.Core.Stats;
using VoidLess.Game.Entities.Characters.Components;
using VoidLess.Game.EventSystem.Base;
using VoidLess.Game.EventSystem.GlobalEvents;
using VoidLess.Game.EventSystem.GlobalEvents.GameEvents;
using VoidLess.Game.Map.Base;
using VoidLess.Game.Map.Layers.CharacterMapLayer;
using VoidLess.Game.Map.Layers.HighlightAreaLayer;
using VoidLess.Game.Map.Layers.PathfindLayer;
using VoidLess.Game.Map.Structs;

namespace VoidLess.Game.Systems
{
    public class MoveSystem : IEventHandler
    {
        public HandleStatus Handle(BattleState state, IGlobalEvent globalEvent)
        {
            return globalEvent switch
            {
                MoveToGameEvent moveTo => Handle(state, moveTo),
                SelectNextActiveGameEvent needAction => Handle(state, needAction),
                _ => HandleStatus.Skipped,
            };
        }

        private HandleStatus Handle(BattleState state, MoveToGameEvent moveToEvent)
        {
            var (ent, targetCell) = moveToEvent;
            var map = state.Map.Map;

            var pathfinder = map.GetLayer<IPathfindMapLayer>();
            var entityLayer = map.GetLayer<ICharacterMapLayer>();
            if (entityLayer.GetCharacter(targetCell) != null) return HandleStatus.Skipped;

            var currCell = entityLayer.GetPosition(ent);
            if (!currCell.HasValue) return HandleStatus.Skipped;

            var pathResult = pathfinder.Pathfind(currCell.Value, targetCell, validDest: IsValidEndpoint);
            if (!pathResult.IsSuccess) return HandleStatus.Skipped;

            var stats = ent.Stats();
            var speed = stats?.Get(StatType.Speed);
            if (speed == null) return HandleStatus.Skipped;

            if (pathResult.Cost > speed.ModifiedValue) return HandleStatus.Skipped;

            entityLayer.RelocateCharacter(ent, targetCell);
            state.EventQueue.Handle(new CharacterRelocatedGameEvent(map, ent, currCell.Value, targetCell));
            state.EventQueue.Handle(new EndTurnGameEvent(ent));
            
            return HandleStatus.Handled;
        }

        private HandleStatus Handle(BattleState state, SelectNextActiveGameEvent nextActive)
        {
            var ent = state.TimeLine.Active;
            if (ent == null) return HandleStatus.Skipped;
            
            var map = state.Map.Map;
            
            var highlightLayer = map.GetLayer<HighlightAreaLayer>();
            if (highlightLayer == null) return HandleStatus.Skipped;
            
            var pathfinder = map.GetLayer<IPathfindMapLayer>();
            var entityLayer = map.GetLayer<ICharacterMapLayer>();
            
            var currCell = entityLayer.GetPosition(ent);
            if (!currCell.HasValue) return HandleStatus.Skipped;

            var stats = ent.Stats();
            var speed = stats?.Get(StatType.Speed);
            if (speed == null) return HandleStatus.Skipped;

            var availableMoveArea = pathfinder.GetAreaByDistance(currCell.Value, speed.ModifiedValue, validDest: IsValidEndpoint);

            highlightLayer.HighlightArea(availableMoveArea.Select(cell => cell.Pos).ToArray());
            
            return HandleStatus.Handled;
        }
        
        private static bool IsValidEndpoint(ILayeredMap map, MapCell cell)
        {
            var characterLayer = map.GetLayer<ICharacterMapLayer>();
            var character = characterLayer.GetCharacter(cell);
            return character == null;
        }
    }
}