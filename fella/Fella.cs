using Game.component;
using Godot;

namespace Game.character
{
  public partial class Fella : CharacterBody2D
  {
	[Export]
	private VelocityComponent velocityComponent;
  }
}
