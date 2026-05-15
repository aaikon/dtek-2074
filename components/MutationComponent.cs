using System.Collections.Generic;
using Godot;
using Game.mutation;
using Godot.Collections;

namespace Game.component
{
  [GlobalClass]
  public partial class MutationComponent : Node
  {
    [Export]
    public VelocityComponent VelocityComponent;
    [Export]
    public AttackComponent AttackComponent;
    [Export]
    public Sprite2D HatSprite;
    [Export]
    public Sprite2D FaceSprite;
    [Export]
    public Sprite2D RightArmSprite;
    [Export]
    public Sprite2D LeftArmSprite;
    [Export]
    public Sprite2D RightLegSprite;
    [Export]
    public Sprite2D LeftLegSprite;
    [Export]
    public Marker2D Back;

    public Array<Mutation> Mutations = new();

    public override void _Ready()
    {
      foreach (Node child in GetChildren())
      {
        HandleChild(child);
      }

      ChildEnteredTree += OnChildEnteredTree;
    }

    private void OnChildEnteredTree(Node node)
    {
      HandleChild(node);
    }

    private void HandleChild(Node node)
    {
      if (node is Mutation mutation)
      {
        Mutations.Add(mutation);
        mutation.Apply(this);
      }
      else
      {
        GD.PushError($"The mutation component only accepts Mutation-type children. Removing {node.Name}");
        node.QueueFree();
      }
    }
  }
}
