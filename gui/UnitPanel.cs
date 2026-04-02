using Game.component;
using Godot;

public partial class UnitPanel : Control
{
  [Export]
  private TextureRect iconRect;

  public override void _Ready()
  {
    Visible = false;
  }

  public override void _Process(double delta)
  {
    if (SelectableComponent.Selected != null)
    {
      iconRect.Texture = SelectableComponent.Selected.EntityDataComponent?.EntityData.Icon;
      Visible = true;
      return;
    }
    Visible = false;
  }
}
