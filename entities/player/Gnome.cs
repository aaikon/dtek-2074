using Game.component;
using Godot;

namespace Game.unit
{
  public partial class Gnome : CharacterBody2D
  {

    [Export]
    private VelocityComponent velocityComponent;
    [Export]
    private PathfindComponent pathfindComponent;
    [Export]
    public AnimatedSprite2D Sprite;

    private Vector2? targetPosition;

    private StateMachine stateMachine = new();

    public override void _Ready()
    {
      AddToGroup("player");
      stateMachine.SetState(StateIdle);
    }

    public override void _Process(double delta)
    {
      stateMachine.Update();
    }

    private void StateIdle()
    {
      Sprite.Play("idle");

      if (targetPosition != null)
      {

        stateMachine.SetState(StateMove);
        return;
      }
    }

    private void StateMove()
    {
      var velocity = velocityComponent.Velocity;
      if (Mathf.Abs(velocity.X) > Mathf.Abs(velocity.Y))
      {
        Sprite.Play("move_horizontal");
        Sprite.Scale = new Vector2(Mathf.Sign(velocity.X), 1);
      }
      else
      {
        if (velocity.Y > 0)
        {
          Sprite.Play("move_down");
        }
        else
        {
          Sprite.Play("move_up");
        }
      }

      if (targetPosition == null)
      {
        stateMachine.SetState(StateIdle);
        return;
      }

      pathfindComponent.FollowPath();
      velocityComponent.Move(this);

      if (pathfindComponent.NavigationAgent2D.IsNavigationFinished())
      {
        targetPosition = null;
        stateMachine.SetState(StateIdle);
        return;
      }
    }

    private void StateAttack()
    {
      // Attack
    }

    public void MoveTo(Vector2 target)
    {
      this.targetPosition = target;
      pathfindComponent.SetTargetPosition(target);
    }
  }
}
