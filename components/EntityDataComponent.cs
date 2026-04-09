using Godot;
using System;

namespace Game.component
{
    [GlobalClass]
    public partial class EntityDataComponent : Node
    {
        [Export]
        public EntityData Data;
    }
}