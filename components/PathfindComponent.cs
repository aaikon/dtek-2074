using Godot;

namespace Game.component
{
  [GlobalClass]
  public partial class PathfindComponent : Node2D
  {
    [Export]
    private VelocityComponent velocityComponent;
    [Export]
    public NavigationAgent2D NavigationAgent2D;

    public void SetTargetPosition(Vector2 position)
    {
      NavigationAgent2D.TargetPosition = position;
    }

    public void FollowPath()
    {
      if (NavigationAgent2D.IsNavigationFinished())
      {
        velocityComponent.Decelerate();
        return;
      }

      var direction = (NavigationAgent2D.GetNextPathPosition() - GlobalPosition).Normalized();
      velocityComponent.AccelerateInDirection(direction);
      NavigationAgent2D.Velocity = velocityComponent.Velocity;
    }
  } 
}