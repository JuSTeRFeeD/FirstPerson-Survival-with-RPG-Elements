namespace Entities.Player
{
    public class PlayerData
    {
        public string PlayerName;
        public EntityStats Stats;
        
        public Leveling Leveling;
        public int SkillPoints = 0;

        public PlayerData()
        {
            Leveling = new Leveling(
                new int[] { 10, 20, 30, 40, 50, 60 },
                1);


        Leveling.LevelUpEvent += HandleLevelUp;
        }

        private void HandleLevelUp()
        {
            SkillPoints++;
        }
        
        public bool UseSkillPoints(int amount)
        {
            if (SkillPoints < amount) return false;
            SkillPoints -= amount;
            return true;
        }
    }
}