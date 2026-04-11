using Godot;

namespace Game.component
{
    [GlobalClass]
    public partial class AttackComponent : Node
    {
        [Export]
        private float range = 30f;
        [Export]
        private float damage = 1f;
        [Export]
        private float AttackInterval = 1f;
        
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
            return from.DistanceTo(to) <= range;
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
            target?.DealDamage(damage);
        }
    }
}