using Game.unit;
using Godot;
using System;

public partial class Shop : Control
{
  [Export]
  private CanvasLayer menu;
  [Export]
  private RichMenuButton buyGnome;
  [Export]
  private RichMenuButton exit;
  [Export]
  private PackedScene gnomeScene;

  private const int GNOME_PRICE = 3;

  public override void _Ready()
  {
    buyGnome.ButtonDown += OnGnomeBuy;
    exit.ButtonDown += () => menu.Visible = false;
  }

  public override void _GuiInput(InputEvent @event)
  {
    if (@event is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.ButtonIndex == MouseButton.Left && mouseButtonEvent.Pressed)
    {
      menu.Visible = true;
    }
  }

  private void OnGnomeBuy()
  {
    if (GamestateController.Instance.Gold >= GNOME_PRICE)
    {
      GamestateController.Instance.UseGold(GNOME_PRICE);
      Gnome gnome = (Gnome)gnomeScene.Instantiate();
      gnome.GlobalPosition = GlobalPosition;
      GetParent().AddChild(gnome);
    }
  }
}
