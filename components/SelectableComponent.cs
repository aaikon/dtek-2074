using Godot;

namespace Game.component
{
  [GlobalClass]
  public partial class SelectableComponent : Area2D
  {
    [Export]
    public EntityDataComponent? EntityDataComponent;
    [Export]
    private ShaderMaterial? material;

    public static SelectableComponent? Hovered { get; private set; }
    public static SelectableComponent? Selected { get; private set; }

    public override void _Ready()
    {
      AddToGroup("selectable");
      MouseEntered += OnMouseEntered;
      MouseExited += OnMouseExited;
    }

    public override void _Input(InputEvent @event)
    {
      if (@event.IsActionPressed("select"))
      {
        Selected = Hovered ?? null;
      }
    }

    private void OnMouseEntered()
    {
      Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);

      if (Hovered != this)
      {
        Hovered?.SetHovered(false);
        Hovered = this;
        SetHovered(true);
      }
    }

    private void OnMouseExited()
    {
      Input.SetDefaultCursorShape(Input.CursorShape.Arrow);

      if (Hovered == this)
      {
        SetHovered(false);
        Hovered = null;
      }
    }

    private void SetHovered(bool value)
    {
      material?.SetShaderParameter("enabled", value);
    }
  }
}