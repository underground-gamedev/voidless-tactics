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

    public override void _Ready()
    { 
        var hud = GetNode<TacticHUD>("HUD");
        UserInterfaceService.SetHUD(hud);

        tacticMap = GetNode<TacticMap>("Map");
        var solidMapGen = GetNode<SolidMapGenerator>("SolidMapGenerator");
        solidMapGen.Generate(tacticMap);
        tacticMap.Sync();

        controllers = this.GetChilds<AbstractController>("Players");
        foreach (var controller in controllers)
        {
            controller.BindMap(tacticMap);
            controller.Init();

            if (controller is HumanController)
            {
                tacticMap.VisualLayer.Connect(nameof(VisualLayer.OnCellClick), controller, nameof(HumanController.OnCellClick));
                tacticMap.VisualLayer.Connect(nameof(VisualLayer.OnCellHover), controller, nameof(HumanController.OnCellHover));
                UserInterfaceService.GetHUD<TacticHUD>().Connect(nameof(TacticHUD.ActionSelected), controller, nameof(HumanController.OnActionSelected));
            }
            else if (controller is AIController)
            {
                controller.Connect(nameof(AIController.OnEndTurn), this, nameof(EndTurn));
            }
        }
        hud.Connect(nameof(TacticHUD.EndTurnPressed), this, nameof(HumanEndTurn));

        activeController = controllers.First();
        activeController.OnTurnStart();
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

        var turnText = $"{activeController.Name} Turn";
        await UserInterfaceService.GetHUD<TacticHUD>().ShowTurnLabel(turnText);

        nextController.OnTurnStart();
    }
}
