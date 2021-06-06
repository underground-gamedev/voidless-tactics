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
				controller.Connect(nameof(HumanController.OnActiveCharacterChanged), this, nameof(UpdateCharacterDescription));
				activeController = controller;
			}
			else if (controller is AIController)
			{
				controller.Connect(nameof(AIController.OnEndTurn), this, nameof(EndTurn));
			}
		}

		var button = GetNode<Button>("UI/ActionMenu/EndTurnButton");
		button.Connect("pressed", this, nameof(HumanEndTurn));
	}

	private async void HumanEndTurn()
	{
		if (activeController is HumanController)
		{
			await EndTurn();
		}
	}

	private async Task ShowTurnLabel(AbstractController controller)
	{
		var text = controller is HumanController ? "Player Turn" : "Enemy Turn";
		var turnLine = GetNode<Control>("UI/TurnHighlight");
		var label = turnLine.GetNode<Label>("TurnLabel");
		label.Text = text;
		turnLine.Visible = true;
		await this.Wait(1);
		turnLine.Visible = false;
	}
	private async Task EndTurn()
	{
		var activeId = controllers.IndexOf(activeController);
		var nextId = (activeId + 1) % controllers.Count;
		var nextController = controllers[nextId];
		activeController.OnTurnEnd();
		activeController = nextController;
		await ShowTurnLabel(nextController);
		nextController.OnTurnStart();
	}

	private void UpdateCharacterDescription(Character activeCharacter)
	{
		var moveLabel = GetNode<Label>("UI/Labels/MoveLabel");
		moveLabel.Text = $"Move: {activeCharacter.MovePoints}";
	}
}
