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

    public override void _Ready()
    {
      NavigationAgent2D.MaxSpeed = velocityComponent.CalculatedMaxSpeed;
      NavigationAgent2D.VelocityComputed += OnVelocityComputed;
    }

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

      var direction = NavigationAgent2D.GetNextPathPosition() - GlobalPosition;
      velocityComponent.AccelerateInDirection(direction);
      NavigationAgent2D.Velocity = velocityComponent.Velocity;
    }

    private void OnVelocityComputed(Vector2 velocity)
    {
      var newDirection = velocity.Normalized();
      var currentDirection = velocityComponent.Velocity.Normalized();
      var halfway = newDirection.Lerp(currentDirection, 1f - Mathf.Exp(-velocityComponent.Acceleration * velocityComponent.AccelerationMultiplier * (float)GetPhysicsProcessDeltaTime()));
      velocityComponent.Velocity = halfway * velocityComponent.Velocity.Length();
    }
  }
}