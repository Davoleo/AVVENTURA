using System;
using Avventura.player;
using Godot;

namespace Avventura.game;

public partial class Game : Node2D
{
	private Label _hpLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_hpLabel = GetNode<Label>("HUD/HPLabel");
		var player = GetNode<Player>("TileMap/Player");
		_hpLabel.Text = $"HP: {player.Health}";
		player.HealthChanged += OnPlayerHealthChanged;
	}

	private void OnPlayerHealthChanged(int _, int newValue)
	{
		_hpLabel.Text = $"HP: {newValue}";
	}
}