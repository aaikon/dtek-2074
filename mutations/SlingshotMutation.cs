using Godot;
using Game.mutation;
using Game.component;

[GlobalClass]
public partial class SlingshotMutation : Mutation
{
  private const float RANGE = 100f;

  public override void Apply(MutationComponent mutationComponent)
  {
    mutationComponent.AttackComponent.Range += RANGE;
    Sprite2D slingshotSprite = new()
    {
      Texture = GD.Load<Texture2D>("res://assets/gnome/mutations/sSlingshot.png")
    };
    
    mutationComponent.Back.AddChild(slingshotSprite);
  }
}
