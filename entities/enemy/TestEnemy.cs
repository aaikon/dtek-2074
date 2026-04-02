using Godot;
using System;

namespace Game.unit
{
  public partial class TestEnemy : CharacterBody2D
  {
    public override void _Ready()
    {
      AddToGroup("enemies");
    }
  }
}
