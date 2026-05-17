using Godot;
using System;

public partial class MovableCamera : Camera2D
{
  [Export] public float Speed = 150f;

  public override void _Process(double delta)
  {
    Vector2 direction = Vector2.Zero;

    if (Input.IsKeyPressed(Key.W)) direction.Y -= 1;
    if (Input.IsKeyPressed(Key.S)) direction.Y += 1;
    if (Input.IsKeyPressed(Key.A)) direction.X -= 1;
    if (Input.IsKeyPressed(Key.D)) direction.X += 1;

    Position += direction.Normalized() * Speed * (float)delta;
  }
}
