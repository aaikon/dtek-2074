using System.Collections.Generic;
using Godot;
using Game.mutation;

namespace Game.component
{
  [GlobalClass]
  public partial class MutationComponent : Node
  {
    [Export]
    public VelocityComponent VelocityComponent;

    private List<Mutation> mutations = new();

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
        mutations.Add(mutation);
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
