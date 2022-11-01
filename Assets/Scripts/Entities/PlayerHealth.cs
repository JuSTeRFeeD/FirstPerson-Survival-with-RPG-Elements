using Managers;

namespace Entities
{
    public class PlayerHealth : Health
    {
        private EntityStats _stats;
        private void Start()
        {
            _stats = GameManager.Instance.PlayerData.Stats;
            _stats.StatsChangeEvent += HandleStatsChange;
            HandleStatsChange();
        }

        private void HandleStatsChange()
        {
            health = _stats.health;
        }
    }
}
