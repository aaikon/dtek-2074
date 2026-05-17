using Godot;
using System;
using Game.component;

namespace Game.unit
{
  public partial class Rat : CharacterBody2D
  {

    [Export]
    private VelocityComponent velocityComponent;
    [Export]
    private HealthComponent healthComponent;
    [Export]
    private AnimationPlayer animationPlayer;
    [Export]
    private Node2D visuals;

    private StateMachine stateMachine = new();

    public override void _Ready()
    {
      AddToGroup("enemies");

      stateMachine.SetState(StateNormal);

      healthComponent.DamageTaken += () => animationPlayer.Play("shake");
      healthComponent.OnHealthZero += () => {
        stateMachine.SetState(StateDeath);
        GamestateController.Instance.AddGold(3);
      };
    }


    public override void _Process(double delta)
    {
      stateMachine.Update();
    }

    private void StateNormal()
    {
      var playerUnits = GetTree().GetNodesInGroup("player");
      Gnome closestGnome = null;
      float closestDistance = float.MaxValue;

      foreach (var unit in playerUnits)
      {
        if (unit is Gnome gnome)
        {
          float distance = GlobalPosition.DistanceTo(gnome.GlobalPosition);
          if (distance < closestDistance)
          {
            closestDistance = distance;
            closestGnome = gnome;
          }
        }
      }

      if (closestGnome != null)
      {
        var direction = (closestGnome.GlobalPosition - GlobalPosition).Normalized();
        velocityComponent.AccelerateInDirection(direction);
        velocityComponent.Move(this);
      }

      if (velocityComponent.Velocity.X != 0)
        visuals.Scale = new Vector2(Math.Sign(velocityComponent.Velocity.X), 1);
    }

    private void StateDeath()
    {
      animationPlayer.Play("die");
      velocityComponent.VelocityOverride = Vector2.Zero;
      visuals.Modulate = new Color(0.7f, 0.7f, 0.7f);
    }
  }
}
