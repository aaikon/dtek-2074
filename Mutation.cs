using Godot;

namespace Game.mutation
{
  [GlobalClass]
  public abstract partial class Mutation : Node
  {
    [Export]
    private string name;
  }
}
