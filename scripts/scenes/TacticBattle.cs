using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class TacticBattle : Node
{
    private TacticMap tacticMap;
    private List<AbstractController> controllers;

    AbstractController activeController;

    ManaMover manaMover;
    private Random rand = new Random();

    public override void _Ready()
    { 
        var hud = GetNode<TacticHUD>("TacticHUD");
        UserInterfaceService.SetHUD(hud);

        tacticMap = GetNode<TacticMap>("Map");

        var solidMapGen = GetNode<SolidMapGenerator>("Map/Generators/SolidMapGenerator");
        solidMapGen?.Generate(tacticMap);

        var manaMapGen = GetNode<ManaMapGenerator>("Map/Generators/ManaMapGenerator");
        manaMapGen?.Generate(tacticMap);

        tacticMap.Sync();

        var camera = GetNode<DraggingCamera>("Camera2D");
        camera.Connect(nameof(DraggingCamera.OnCameraMove), hud, nameof(TacticHUD.OnCameraDrag));
        camera.Connect(nameof(DraggingCamera.OnCameraZoom), hud, nameof(TacticHUD.OnCameraZoom));

        manaMover = new ManaMover(tacticMap);

        hud.Connect(nameof(TacticHUD.EndTurnPressed), this, nameof(HumanEndTurn));

        Task.Run(BattleInit);
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
                controller.Connect(nameof(AIController.OnEndTurn), this, nameof(EndTurn));
            }
            else
            {
                GD.PrintErr("[SceneRoot] Incorrect Controller Type");
            }

            await controller.Init(tacticMap, GetSpawnArea());
        }

        await UserInterfaceService.GetHUD<TacticHUD>().ShowTurnLabel("Battle Start");
        activeController = controllers.First();
        activeController.OnTurnStart();
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

    private async void HumanEndTurn()
    {
        if (activeController is HumanController)
        {
            await EndTurn();
        }
    }

    private async Task EndTurn()
    {
        var activeId = controllers.IndexOf(activeController);
        var nextId = (activeId + 1) % controllers.Count;
        var nextController = controllers[nextId];
        activeController.OnTurnEnd();
        activeController = nextController;

        /// mana move place
        
		
		manaMover.ApplyChangesMap(manaMover.GetChangesMap());
		tacticMap.ManaLayer.OnSync(tacticMap);

        var turnText = $"{activeController.Name} Turn";
        await UserInterfaceService.GetHUD<TacticHUD>().ShowTurnLabel(turnText);

        nextController.OnTurnStart();
    }
}
