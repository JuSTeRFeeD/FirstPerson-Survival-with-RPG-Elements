namespace Utils
{
    public class Cooldown
    {
        public float cooldownTime { get; private set; }
        public float curCooldown { get; private set; }
        public bool IsOnCooldown => curCooldown > 0;

        public delegate void OnCooldownResets();
        public OnCooldownResets CooldownResetsEvent;

        public void Start()
        {
            curCooldown = cooldownTime;
        }
        
        public void Update(float deltaTime)
        {
            if (curCooldown <= 0) return;
            curCooldown -= deltaTime;

            if (curCooldown <= 0) CooldownResetsEvent?.Invoke();
        }

        public Cooldown(float cooldown)
        {
            if (cooldown < 0) cooldown = 0;
            cooldownTime = cooldown;
            curCooldown = 0;
        }
        
        public void SetCooldownTime(float seconds)
        {
            if (seconds < 0) cooldownTime = 0;
            else cooldownTime = seconds;
        }
    }
}