using Game.component;
using Game.unit;
using Godot;
using Godot.Collections;

public partial class MutationController : Node
{
  public static MutationController Instance { get; private set; } = null!;

  public Array<PackedScene> mutations = [];


  public override void _Ready()
  {
    Instance = this;
  }

  public void AddMutation(PackedScene mutation)
  {
    var units = GetTree().GetNodesInGroup("player");

    foreach (var unit in units)
    {
      if (unit is Gnome gnome)
      {
        var mutationComponent = gnome.GetNodeOrNull<MutationComponent>("MutationComponent");
        if (mutationComponent == null) continue;
        mutationComponent.AddChild(mutation.Instantiate());
      }
    }

    mutations.Add(mutation);
  }
}
