using Godot;
using Game.character;
using System.Collections.Generic;
using System.Linq;

namespace Game.controller
{
  [GlobalClass]
  public partial class PlayerMovementController : Node2D
  {
    private List<Unit> units = new();

    public override void _Ready()
    {
      units = [.. GetTree().GetNodesInGroup("units").Cast<Unit>()];
    }

    public override void _Input(InputEvent @event)
    {
      if (@event.IsActionPressed("move"))
      {
        var target = GetGlobalMousePosition();

        foreach (var unit in units)
        {
          unit.MoveTo(target);
        }
      }
    }
  }
}