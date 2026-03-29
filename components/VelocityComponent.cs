using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.component
{
  [GlobalClass]
  public partial class VelocityComponent : Node
  {
    [Export]
    private float maxSpeed = 100;
    [Export]
    private float acceleration = 10;

    public Vector2 Velocity { get; set; }
    public Vector2? VelocityOverride { get; set; }
    public float SpeedMultiplier { get; set; } = 1.0f;
    public float SpeedPercentModifier => speedPercentModifiers.Values.Sum();
    public float AccelerationMultiplier { get; set; } = 1.0f;

    public float Acceleration => acceleration;
    public float SpeedPercent => Math.Min(Velocity.Length() / (CalculatedMaxSpeed > 0f ? CalculatedMaxSpeed : 1f), 1f);
    public float CalculatedMaxSpeed => maxSpeed * (1f + SpeedPercentModifier) * SpeedMultiplier;

    private Dictionary<string, float> speedPercentModifiers = new();

    public void AccelerateToVelocity(Vector2 velocity)
    {
      Velocity = Velocity.Lerp(velocity, 1f - Mathf.Exp(-acceleration * AccelerationMultiplier));
    }

    public void AccelerateInDirection(Vector2 direction)
    {
      AccelerateToVelocity(direction.Normalized() * CalculatedMaxSpeed);
    }

    public void Decelerate()
    {
      AccelerateToVelocity(Vector2.Zero);
    }

    public void Move(CharacterBody2D characterBody)
    {
      characterBody.Velocity = VelocityOverride ?? Velocity;
      characterBody.MoveAndSlide();
    }

    public void AddSpeedPercentModifier(string name, float modifier)
    {
      speedPercentModifiers[name] = modifier;
    }

    public void RemoveSpeedPercentModifier(string name)
    {
      speedPercentModifiers.Remove(name);
    }
  }
}
