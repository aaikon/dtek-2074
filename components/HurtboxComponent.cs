using Godot;

namespace Game.component
{
  [GlobalClass]
  public partial class HurtboxComponent : Area2D
  {
    [Export]
    private HealthComponent healthComponent;

    private void DealDamage(float damage)
    {
      healthComponent.Damage(damage);
    }

    private void OnAreaEntered(Area2D otherArea)
    {
      if (otherArea is HitboxComponent hitboxComponent)
      {
        DealDamage(hitboxComponent.Damage);
      }
    }
  }
}