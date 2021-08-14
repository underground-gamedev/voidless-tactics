using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TacticBattle : Node
{
    private TacticMap tacticMap;
    private List<AbstractController> controllers;
    private Random rand = new Random();

    public override void _Ready()
    { 
        var hud = GetNode<TacticHUD>("TacticHUD");
        UserInterfaceService.SetHUD(hud);

        tacticMap = GetNode<TacticMap>("Map");

        var manaMapGen = GetNode<ManaMapGenerator>("Map/Generators/ManaMapGenerator");
        manaMapGen?.Generate(tacticMap);

        tacticMap.Sync();

        var camera = GetNode<DraggingCamera>("Camera2D");

        var turnManager = GetNode<TurnManager>("TurnManager");

        Task.Run(BattleInit)
            .GetAwaiter()
            .OnCompleted(() => turnManager.TurnLoop(controllers));
    }

    public async Task BattleInit()
    {
        controllers = this.GetChilds<AbstractController>("Players");
        foreach (var controller in controllers.AsEnumerable().Reverse())
        {
            if (controller is HumanController)
            {
                tacticMap.VisualLayer.Connect(nameof(VisualLayer.OnDragStart), controller, nameof(HumanController.OnDragStart));
                tacticMap.VisualLayer.Connect(nameof(VisualLayer.OnDragEnd), controller, nameof(HumanController.OnDragEnd));
                tacticMap.VisualLayer.Connect(nameof(VisualLayer.OnCellClick), controller, nameof(HumanController.OnCellClick));
                tacticMap.VisualLayer.Connect(nameof(VisualLayer.OnCellHover), controller, nameof(HumanController.OnCellHover));
                UserInterfaceService.GetHUD<TacticHUD>().Connect(nameof(TacticHUD.ActionSelected), controller, nameof(HumanController.OnActionSelected));
            }
            else if (controller is AIController)
            {
                // controller.Connect(nameof(AIController.OnEndTurn), this, nameof(EndTurn));
            }
            else
            {
                GD.PrintErr("[SceneRoot] Incorrect Controller Type");
            }

            await controller.Init(tacticMap, GetSpawnArea());
        }
    }


    private List<MapCell> GetSpawnArea()
    {
        var allFreeArea = tacticMap.FindAllFloor().Where(cell => cell.MapObject == null).ToList();
        var basePosition = allFreeArea[rand.Next(0, allFreeArea.Count)];
        const int spawnRange = 4;
        return tacticMap.PathfindLayer
                    .GetAllAvailablePathDest(basePosition, spawnRange)
                    .Select(pos => tacticMap.CellBy(pos.Item1, pos.Item2))
                    .Where(cell => cell.MapObject == null)
                    .ToList();
    }
}
