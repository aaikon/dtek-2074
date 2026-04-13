using Game.component;
using Godot;
using System;
using System.Diagnostics;

namespace Game.unit
{
  public partial class TestEnemy : CharacterBody2D
  {
    [Export]
    private VelocityComponent velocityComponent;
    
    public override void _Ready()
    {
      AddToGroup("enemies");
    }

    public override void _Process(double delta)
    {
      velocityComponent.Decelerate();
      velocityComponent.Move(this);
    }
  }
}
