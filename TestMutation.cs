using Godot;
using Game.component;

namespace Game.mutation
{
  [GlobalClass]
  public partial class TestMutation : Mutation
  {

    public override void Apply(MutationComponent mutationComponent)
    {
      mutationComponent?.VelocityComponent?.AddSpeedPercentModifier(Name, 10f);
    }
  }
}
