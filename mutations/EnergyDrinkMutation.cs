using Godot;
using Game.mutation;
using Game.component;

public partial class EnergyDrinkMutation : Mutation
{
  private const float SPEED_MOD = 2f;
  private const float ATTACK_INTERVAL_DEC = 0.5f;

  public override void Apply(MutationComponent mutationComponent)
  {
    mutationComponent.VelocityComponent.AddSpeedPercentModifier("energy_drink", SPEED_MOD);
    mutationComponent.AttackComponent.AttackInterval -= ATTACK_INTERVAL_DEC;
    mutationComponent.FaceSprite.Texture = GD.Load<Texture2D>("res://assets/gnome/mutations/sGnomeFaceEnergyDrink.png");
  }
}
