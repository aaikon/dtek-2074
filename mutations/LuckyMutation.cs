using Godot;
using Game.component;

namespace Game.mutation
{
  [GlobalClass]
  public partial class LuckyMutation : Mutation
  {

    public override void Apply(MutationComponent mutationComponent)
    {
      mutationComponent.HatSprite.Texture = GD.Load<Texture2D>("res://assets/gnome/mutations/sLuckyHat.png");

      // Change the ammount of gold dropped by enemies (luck stat?)
    }
  }
}
