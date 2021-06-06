using Godot;
using System;
using System.Collections.Generic;

public class TacticBattle : Node
{
	private TacticMap tacticMap;
	private List<TeamController> controllers;

	TeamController activeController;

	public override void _Ready()
	{
		tacticMap = GetNode<TacticMap>("Map");
		var solidMapGen = GetNode<SolidMapGenerator>("SolidMapGenerator");
		solidMapGen.Generate(tacticMap);
		tacticMap.Sync();

		controllers = this.GetChilds<TeamController>("Players");
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
		}

		var button = GetNode<Button>("UI/ActionMenu/EndTurnButton");
		button.Connect("pressed", this, nameof(EndTurn));
	}

	private void EndTurn()
	{
		var activeId = controllers.IndexOf(activeController);
		var nextId = (activeId + 1) % controllers.Count;
		var nextController = controllers[nextId];
		activeController.OnTurnEnd();
		nextController.OnTurnStart();
		activeController = nextController;
	}

	private void UpdateCharacterDescription(Character activeCharacter)
	{
		var moveLabel = GetNode<Label>("UI/Labels/MoveLabel");
		moveLabel.Text = $"Move: {activeCharacter.MovePoints}";
	}
}
