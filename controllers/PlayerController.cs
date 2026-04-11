using Godot;
using Game.unit;
using System.Collections.Generic;
using System.Linq;
using Game.component;
using Godot.Collections;

namespace Game.controller
{
  [GlobalClass]
  public partial class PlayerController : Node2D
  {
    private Array<SelectableComponent> selected = new();

    public override void _Ready()
    {
      SelectionController.Instance.SelectionChanged += selected => this.selected = selected;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
      if (!@event.IsActionPressed("move")) return;

      var hovered = SelectionController.Instance.Hovered;

      if (hovered?.Target?.IsInGroup("enemies") == true)
      {
        IssueAttackOrder(hovered.Target as CharacterBody2D);
      }
      else
      {
        IssueMoveOrder(GetGlobalMousePosition());
      }
    }

    private void IssueAttackOrder(CharacterBody2D? target)
    {
      /*
      if (selected.Count() == 0)
      {
         List<Gnome> playerUnits = [.. GetTree().GetNodesInGroup("player").Cast<Gnome>()];
         foreach (var unit in playerUnits)
          unit.AttackTarget(target);
         return;
      }
      */
      foreach (var selectable in selected)
      {
        if (selectable.Target is Gnome gnome)
          gnome.AttackTarget(target);
      }
    }

    private void IssueMoveOrder(Vector2 position)
    {
      /*
      if (selected.Count() == 0)
      {
         List<Gnome> playerUnits = [.. GetTree().GetNodesInGroup("player").Cast<Gnome>()];
         foreach (var unit in playerUnits)
          unit.MoveTo(position);
         return;
      }
      */
      foreach (var selectable in selected)
      {
        if (selectable.Target is Gnome gnome)
          gnome.MoveTo(position);
      }
    }
  }
}