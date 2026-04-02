using Godot;
using Game.unit;
using System.Collections.Generic;
using System.Linq;
using Game.component;

namespace Game.controller
{
  [GlobalClass]
  public partial class PlayerController : Node2D
  {
    public override void _Input(InputEvent @event)
    {
      List<Gnome> playerUnits = [.. GetTree().GetNodesInGroup("player").Cast<Gnome>()];
      List<SelectableComponent> selectables = [.. GetTree().GetNodesInGroup("selectable").Cast<SelectableComponent>()];

      if (@event.IsActionPressed("move"))
      {
        /*
        if (SelectableComponent.Hovered?.Target != null)
        {
          var targetUnit = SelectableComponent.Hovered.Target;

          if (targetUnit is TestEnemy testEnemy)
          {
            GD.Print("Hey, don't touch me!");
          }

          return;
        }
        */
        var targetPosition = GetGlobalMousePosition();

        foreach (var unit in playerUnits)
        {
          unit.MoveTo(targetPosition);
        }
      }
    }
  }
}