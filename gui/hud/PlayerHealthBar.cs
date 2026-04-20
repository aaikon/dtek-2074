using Game.component;
using Godot;
using Godot.Collections;

public partial class PlayerHealthBar : ProgressBar
{
  public static PlayerHealthBar Instance;

  private Array<HealthComponent> array = new();

  public override void _Ready()
  {
    Instance = this;
  }

  public void Add(HealthComponent healthComponent)
  {
    array.Add(healthComponent);
    UpdateHealthBar();
  }

  public void OnHealthChanged(float health, float maxHealth)
  {
    UpdateHealthBar();
  }

  private void UpdateHealthBar()
  {
    var health = 0f;
    var maxHealth = 0f;

    foreach (var healthComponent in array)
    {
      health += healthComponent.Health;
      maxHealth += healthComponent.MaxHealth;
    }
    MaxValue = maxHealth;
    Value = health;
  }
}
