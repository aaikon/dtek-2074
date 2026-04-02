using Godot;
using System;

namespace Game.component
{
  [GlobalClass]
  public partial class ShadowComponent : Node2D
  {
    [Export]
    private Vector2 size;

    public override void _Draw()
    {
      DrawEllipse(Vector2.Zero, size.X, size.Y, new Color(0, 0, 0, 0.1f));
    }
  }
}