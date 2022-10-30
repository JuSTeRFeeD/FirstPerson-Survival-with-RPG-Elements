namespace Entities.Player
{
    public class PlayerData
    {
        public string PlayerName;
        public EntityStats Stats;
        
        public Leveling Leveling;
        public int SkillPoints = 0;
        
        public delegate void OnSkillPointsChanged(); 
        public OnSkillPointsChanged SkillPointsChangedEvent; 

        public PlayerData()
        {
            Stats = new EntityStats(
                0,
                0,
                0,
                10,
                10,
                100,
                1,
                5
                );
            Leveling = new Leveling(
                new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170 },
                1);

            Leveling.LevelUpEvent += HandleLevelUp;
        }

        private void HandleLevelUp()
        {
            SkillPoints++;
            SkillPointsChangedEvent?.Invoke();
        }
        
        public bool UseSkillPoints(int amount)
        {
            if (SkillPoints < amount) return false;
            SkillPoints -= amount;
            SkillPointsChangedEvent?.Invoke();
            return true;
        }
    }
}