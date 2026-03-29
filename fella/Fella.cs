using Game.component;
using Godot;

namespace Game.character
{
  public partial class Fella : CharacterBody2D
  {
    [Export]
    private VelocityComponent velocityComponent;
    [Export]
    private PathfindComponent pathfindComponent;

    public override void _Process(double delta)
    {
      pathfindComponent.SetTargetPosition(GetGlobalMousePosition());
      pathfindComponent.FollowPath();
      velocityComponent.Move(this);
    }
  }
}
