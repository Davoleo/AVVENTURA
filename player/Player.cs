using System;
using Godot;

namespace Avventura.player;

public partial class Player : CharacterBody2D
{
    private const int MaxHealth = 10;
    private const float Speed = 400;
    private const float JumpVelocity = -600.0F;

    private readonly int _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsInt32();

    private int Health { get; set; }

    public override void _Ready()
    {
        Health = MaxHealth;
    }

    public override void _PhysicsProcess(double delta)
    {

        var velocity = Velocity;

        //Add Gravity
        if (!IsOnFloor())
        {
            velocity.Y += _gravity * (float) delta;
        }

        //Handle Jump
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JumpVelocity;
        }

        //input direction
        var direction = Input.GetAxis("move_left", "move_right");
        if (Math.Abs(direction) > 0)
        {
            velocity.X = direction * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(velocity.X, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}