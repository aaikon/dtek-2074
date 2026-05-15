using Godot;

namespace Game.component
{
    [GlobalClass]
    public partial class AttackComponent : Node
    {
        [Signal]
        public delegate void OnAttackTimeoutEventHandler();
        
        [Export]
        private Node2D owner;
        [Export]
        public float Range = 30f;
        [Export]
        public float Damage = 1f;
        [Export]
        public float AttackInterval = 1f;
        
        private bool isAttacking = false;

        private HurtboxComponent? target;

        private Timer timer = new();

        public override void _Ready()
        {
            timer.Autostart = false;
            timer.Timeout += OnTimerTimeout;
            AddChild(timer);
        }

        public void SetTarget(HurtboxComponent? target)
        {
            this.target = target;
        }

        public bool IsInRange(Vector2 to, Vector2 from)
        {
            return from.DistanceTo(to) <= Range;
        }

        public void Start()
        {
            if (isAttacking) return;
            timer.Start(AttackInterval);
            isAttacking = true;
        }

        public void Stop()
        {
            if (!isAttacking) return;
            timer.Stop();
            isAttacking = false;
        }

        private void OnTimerTimeout()
        {
            target?.Attack(new Attack(Damage, (target.GlobalPosition - owner.GlobalPosition).Normalized() * 100));
            EmitSignal(SignalName.OnAttackTimeout);
        }
    }
}