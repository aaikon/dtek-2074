using Godot;

namespace Game.component
{
  [GlobalClass]
  public partial class HealthComponent : Node
  {
    [Export]
    private float maxHealth;

    private float health;

    public override void _Ready()
    {
      health = maxHealth;
    }

    public void Damage(float damage)
    {
      health -= damage;
    }
  }
}