using Godot;
using Game.component;

namespace Game.mutation
{
  [GlobalClass]
  public partial class Mutation : Node
  {
    private MutationComponent mutationComponent;

    public virtual void Apply(MutationComponent mutationComponent)
    {
      this.mutationComponent = mutationComponent;
    }
  }
}
