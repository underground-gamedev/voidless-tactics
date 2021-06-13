using Godot;
using System;
using System.Collections.Generic;
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
                UserInterfaceService.GetHUD<TacticHUD>().Connect(nameof(TacticHUD.ActionSelected), controller, nameof(HumanController.OnActionSelected));
                activeController = controller;
            }
            else if (controller is AIController)
            {
                controller.Connect(nameof(AIController.OnEndTurn), this, nameof(EndTurn));
            }
        }

        hud.Connect(nameof(TacticHUD.EndTurnPressed), this, nameof(HumanEndTurn));
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

        var turnText = activeController is HumanController ? "Player Turn" : "Enemy Turn";
        await UserInterfaceService.GetHUD<TacticHUD>().ShowTurnLabel(turnText);

        nextController.OnTurnStart();
    }
}
