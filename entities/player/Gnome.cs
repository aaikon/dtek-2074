using System;
using Game.component;
using Godot;

namespace Game.unit
{
  public partial class Gnome : CharacterBody2D
  {
    [Export]
    public AnimatedSprite2D Sprite;

    [Export]
    private HealthComponent healthComponent;
    [Export]
    private VelocityComponent velocityComponent;
    [Export]
    private PathfindComponent pathfindComponent;
    [Export]
    private AttackComponent attackComponent;

    private Vector2? targetPosition;
    private CharacterBody2D targetEntity;

    private StateMachine stateMachine = new();

    public override void _Ready()
    {
      AddToGroup("player");
      stateMachine.SetState(StateIdle);

      PlayerHealthBar.Instance.Add(healthComponent);
      healthComponent.HealthChanged += PlayerHealthBar.Instance.OnHealthChanged;
    }

    public override void _Process(double delta)
    {
      UpdateSprite();
      stateMachine.Update();
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
      if (targetEntity != null) stateMachine.SetState(StateAttack);
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
        attackComponent.Stop();
        stateMachine.SetState(StateIdle);
        return;
      }

      if (attackComponent.IsInRange(targetEntity.GlobalPosition, GlobalPosition))
      {
        var targetHurtbox = targetEntity.GetNodeOrNull<HurtboxComponent>("HurtboxComponent");
        attackComponent.SetTarget(targetHurtbox);
        attackComponent.Start();
        return;
      }
      else
      {
        attackComponent.Stop();
      }

      pathfindComponent.SetTargetPosition(targetEntity.GlobalPosition);
      pathfindComponent.FollowPath();
      velocityComponent.Move(this);
    }

    public void MoveTo(Vector2 target)
    {
      attackComponent.Stop();
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
