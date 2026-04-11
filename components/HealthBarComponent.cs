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
			healthComponent.HealthChanged += OnHealthChanged;
			OnHealthChanged(healthComponent.Health, healthComponent.MaxHealth);
		}

		private void OnHealthChanged(float health, float maxHealth)
		{
			HealthBar.Value = health;
			HealthBar.MaxValue = maxHealth;
		}
	}
}
