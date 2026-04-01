using Game.component;
using Godot;

namespace Game.character
{
  public partial class Unit : CharacterBody2D
  {
    [Export]
    private VelocityComponent velocityComponent;
    [Export]
    private PathfindComponent pathfindComponent;
    [Export]
    private AnimatedSprite2D sprite;

    private Vector2? target;

    private StateMachine stateMachine = new();

    public override void _Ready()
    {
      AddToGroup("units");
      stateMachine.SetState(StateIdle);
    }

    public override void _Process(double delta)
    {
      stateMachine.Update();
    }

    private void StateIdle()
    {
      sprite.Play("idle");

      if (target != null)
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
        sprite.Play("move_horizontal");
        sprite.Scale = new Vector2(Mathf.Sign(velocity.X), 1);
      }
      else
      {
        if (velocity.Y > 0)
        {
          sprite.Play("move_down");
        }
        else
        {
          sprite.Play("move_up");
        }
      }

      if (target == null)
      {
        stateMachine.SetState(StateIdle);
        return;
      }

      pathfindComponent.FollowPath();
      velocityComponent.Move(this);

      if (pathfindComponent.NavigationAgent2D.IsNavigationFinished())
      {
        target = null;
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
      this.target = target;
      pathfindComponent.SetTargetPosition(target);
    }
  }
}
