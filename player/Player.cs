using System;
using Godot;

namespace Avventura.player;

public partial class Player : CharacterBody2D
{
    private const int MaxHealth = 10;
    private const float Speed = 500;
    private const float JumpVelocity = -900.0F;

    private readonly int _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsInt32();
    private int _health;

    public int Health
    {
        get => _health;
        private set
        {
            EmitSignal(SignalName.HealthChanged, _health, value);
            _health = value;
        }
    }

    [Signal]
    public delegate void HealthChangedEventHandler(int oldValue, int newValue);

    public override void _Ready()
    {
        Console.WriteLine("HealthSet");
        Health = MaxHealth;
    }

    public void TakeDamage(int amount, Vector2 sourcePos)
    {
        Health -= amount;
        Velocity = Velocity.MoveToward(sourcePos, -2);
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