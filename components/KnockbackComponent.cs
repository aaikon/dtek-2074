using Godot;
using System;

namespace Game.component
{
    [GlobalClass]
    public partial class KnockbackComponent : Node
    {
        [Export]
        private VelocityComponent velocityComponent;
        [Export]
        private float knockbackCoefficient = 1f;

        public void Knockback(Vector2 knockback)
        {
            velocityComponent.Velocity += knockback * knockbackCoefficient;
        }
    }
}
