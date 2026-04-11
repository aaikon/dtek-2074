using Godot;

namespace Game.component
{
	[GlobalClass]
	public partial class HealthBarComponent : Node2D
	{
		[Export]
		private HealthComponent healthComponent;
		[Export]
		private ProgressBar HealthBar;

		public override void _Ready()
		{
			HealthBar.Value = healthComponent.Health;
			HealthBar.MaxValue = healthComponent.MaxHealth;
			healthComponent.HealthChanged += OnHealthChanged;
		}

		private void OnHealthChanged(float health, float maxHealth)
		{
			HealthBar.Value = health;
			HealthBar.MaxValue = maxHealth;
		}
	}
}
