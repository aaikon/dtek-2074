using Godot;

namespace Game.component
{
  [GlobalClass]
  public partial class HealthComponent : Node
  {
    [Signal] 
    public delegate void HealthChangedEventHandler(float health, float maxHealth);

    [Export]
    public float MaxHealth { get; private set; }

    public float Health { get; private set; }

    public override void _Ready()
    {
      Health = MaxHealth;
    }

    public void Damage(float damage)
    {
      SetHealth(Health - damage);
    }

    private void SetMaxHealth(float health)
    {
      MaxHealth = health;
      EmitSignal(SignalName.HealthChanged, Health, MaxHealth);
    }

    private void SetHealth(float health)
    {
      Health = health;
      EmitSignal(SignalName.HealthChanged, Health, MaxHealth);
    }
  }
}