using Game.controller;
using Godot;

namespace Game.component
{
  [GlobalClass]
  public partial class SelectableComponent : Area2D
  {
    [Export]
    public Node? Target;
    [Export]
    private ShaderMaterial? outlineMaterial;

    private bool isHovered;
    private bool isSelected;

    public override void _Ready()
    {
      AddToGroup("selectable");

      MouseEntered += OnMouseEntered;
      MouseExited += OnMouseExited;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
      if (!@event.IsActionPressed("select")) return;

      if (SelectionController.Instance.Hovered != this) return;

      SelectionController.Instance.SetSelection(this);
    }

    private void OnMouseEntered()
    {
      Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
      SelectionController.Instance.SetHover(this);
    }

    private void OnMouseExited()
    {
      Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
      SelectionController.Instance.ClearHover(this);
    }

    public void SetHovered(bool value)
    {
      isHovered = value;
      UpdateOutline();
    }

    public void SetSelected(bool value)
    {
      isSelected = value;
      UpdateOutline();
    }

    private void UpdateOutline()
    {
      outlineMaterial?.SetShaderParameter("enabled", isHovered || isSelected);
    }
  }
}