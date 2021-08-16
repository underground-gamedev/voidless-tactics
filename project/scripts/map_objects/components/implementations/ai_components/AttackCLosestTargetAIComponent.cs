using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;

public class AttackCLosestTargetAIComponent : AIComponent
{
    Character Target;

    Character GetClosestTarget()
    {
        Character resultC = null;

        Vector2 SelfPosition;
        Vector2 TargetPosition;

        float MinD = 100 * 100;
        float D = 0;

        SelfPosition = intintToV2(character.Cell.Position);

        foreach(Character C in enemyCharacters)
        {
            TargetPosition = intintToV2(C.Cell.Position);

            D = (TargetPosition - SelfPosition).Length();

            if (D < MinD)
            {
                resultC = C;

                MinD = D;
            }
        }

        return resultC;
    }

    public override async Task MakeTurn()
    {
        Target = GetClosestTarget();

        List<MoveCell> MoveAreaMoveCells = moveComponent.MoveAvailable() ? moveComponent.GetMoveArea() : new List<MoveCell>();

        List<MapCell> AttackCircleMapCells = map.AllNeighboursFor(Target.Cell.Position.Item1, Target.Cell.Position.Item2);

        List<MapCell> MoveAreaMapCells = new List<MapCell>();

        foreach (MoveCell MC in MoveAreaMoveCells)
        {
            MoveAreaMapCells.Add(MC.MapCell);
        }

        MoveAreaMapCells.Add(character.Cell);

        List<MapCell> CellsAttackCanOccurFrom = SameMapCells(AttackCircleMapCells, MoveAreaMapCells);

        if (CellsAttackCanOccurFrom.Count > 0)
        {
            if (!CellsAttackCanOccurFrom.Contains(character.Cell))
            {
                await moveComponent.MoveTo(GetClosestCellFromList(CellsAttackCanOccurFrom));
            }

            await attackComponent.Attack(Target.GetTargetComponent());
        }
        else
        {
            if (moveComponent.MoveAvailable()) {

                MapCell FarthestCell = null;

                (int, int)[] iiPath = map.PathfindLayer.Pathfind(intintToV2(character.Cell.Position), intintToV2(Target.Cell.Position));

                for (int i = 1; i < iiPath.Length - 1; i++)
                {
                    if (MoveAreaMapCells.Contains(map.CellBy(iiPath[i].Item1, iiPath[i].Item2)))
                    {
                        FarthestCell = map.CellBy(iiPath[i].Item1, iiPath[i].Item2);
                    }
                    else
                    {
                        break;
                    }
                }

                if(FarthestCell != null)
                {
                    await moveComponent.MoveTo(FarthestCell);
                }
            }
        }
    }

    Vector2 intintToV2((int, int)ii)
    {
        Vector2 rV = Vector2.Zero;

        rV.x = ii.Item1;
        rV.y = ii.Item2;

        return rV;
    }

    List<MapCell> SameMapCells(List<MapCell> L1, List<MapCell> L2)
    {
        List<MapCell> result = new List<MapCell>();

        foreach (MapCell cfl1 in L1)
        {
            foreach (MapCell cfl2 in L2)
            {
                if (cfl1 == cfl2)
                {
                    result.Add(cfl1); 
                }
            }
        }

        return result;
    }

    MapCell GetClosestCellFromList(List<MapCell> L)
    {
        MapCell result = null;

        Vector2 SelfPosition;
        Vector2 CellPosition;

        float MinD = 100 * 100;
        float D = 0;

        SelfPosition = intintToV2(character.Cell.Position);

        foreach (MapCell MC in L)
        {
            CellPosition = intintToV2(MC.Position);

            D = (CellPosition - SelfPosition).Length();

            if (D < MinD)
            {
                result = MC;

                MinD = D;
            }
        }

        if (result == null) GD.Print("!!!!");

        return result;
    }

}