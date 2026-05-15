using Game.component;
using Game.mutation;
using Godot;
using System;

public partial class MutationPickup : Node2D
{
  [Export]
  public PackedScene Mutation;
  [Export]
  private Sprite2D sprite;
  [Export]
  private Texture2D icon;


  public override void _Ready()
  {
    var iconSprite = GetNode<Sprite2D>("Icon");
    iconSprite.Texture = icon;
  }

  public void Disable()
  {
    sprite.Visible = false;
    GetNode<SelectableComponent>("SelectableComponent").QueueFree();
  }
}
