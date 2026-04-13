using Godot;

namespace Game.component
{
  [GlobalClass]
  public partial class HurtboxComponent : Area2D
  {
    [Export]
    private HealthComponent healthComponent;
    [Export]
    private KnockbackComponent knockbackComponent;

    public void Attack(Attack attack)
    {
      healthComponent.Damage(attack.damage);
      knockbackComponent.Knockback(attack.knockback);
    }

    private void OnAreaEntered(Area2D otherArea)
    {
      if (otherArea is HitboxComponent hitboxComponent)
      {
        Attack(hitboxComponent.Attack);
      }
    }
  }
}