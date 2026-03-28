using Game.component;
using Godot;

namespace Game.character
{
  public partial class Fella : CharacterBody2D
  {
	[Export]
	private VelocityComponent velocityComponent;

	public override void _Process(double delta)
	{
	  velocityComponent.AccelerateInDirection(GlobalPosition.DirectionTo(GetGlobalMousePosition()));
	  velocityComponent.Move(this);
	}

  }
}
