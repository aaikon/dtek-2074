using System;
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
    private CharacterBody2D targetEntity;

    private StateMachine stateMachine = new();

    public override void _Ready()
    {
      AddToGroup("player");
      stateMachine.SetState(StateIdle);
    }

    public override void _Process(double delta)
    {
      stateMachine.Update();
      UpdateSprite();
    }

    private void UpdateSprite()
    {
      var velocity = velocityComponent.Velocity;

      if (velocity == Vector2.Zero) { Sprite.Play("idle"); return; }

      if (Mathf.Abs(velocity.X) > Mathf.Abs(velocity.Y))
      {
        Sprite.Play("move_horizontal");
        Sprite.Scale = new Vector2(Mathf.Sign(velocity.X), 1);
      }
      else
      {
        Sprite.Play(velocity.Y > 0 ? "move_down" : "move_up");
      }
    }

    private void StateIdle()
    {
      if (targetPosition != null) stateMachine.SetState(StateMove);
      if (targetEntity != null)   stateMachine.SetState(StateAttack);
    }

    private void StateMove()
    {
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
      }
    }

    private void StateAttack()
    {
      if (targetEntity == null)
      {
        stateMachine.SetState(StateIdle);
        return;
      }

        pathfindComponent.SetTargetPosition(targetEntity.GlobalPosition);
        pathfindComponent.FollowPath();
        velocityComponent.Move(this);
    }

    public void MoveTo(Vector2 target)
    {
      targetEntity = null;
      targetPosition = target;
      pathfindComponent.SetTargetPosition(target);
      stateMachine.SetState(StateMove); 
    }

    public void AttackTarget(CharacterBody2D target)
    {
      targetPosition = null;
      targetEntity = target;
      stateMachine.SetState(StateAttack);
    }
  }
}
