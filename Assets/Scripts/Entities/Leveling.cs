namespace Entities
{
    public class Leveling
    {
        public int Level { get; private set; } = 1;

        public int TargetExperience = 10;
        public int CurrentExperience { get; private set; } = 0;

        private readonly int[] _levelExperiences;

        public delegate void OnLevelUp();
        public OnLevelUp LevelUpEvent;
        
        public delegate void OnExperienceChanged(int expAmount);
        public OnExperienceChanged ExperienceChangedEvent;

        public float ExperienceProgress => TargetExperience == 0 ? 0 : (float)CurrentExperience / TargetExperience; 

        public Leveling(int[] levelExperiences, int curLevel)
        {
            _levelExperiences = levelExperiences;
            TargetExperience = levelExperiences[curLevel];
            Level = curLevel;
        }
        public Leveling() { }

        public void AddExperience(int exp)
        {
            if (exp <= 0) return;
            CurrentExperience += exp;
            while (CurrentExperience >= TargetExperience)
            {
                CurrentExperience -= TargetExperience;
                Level++;
                if (_levelExperiences.Length > Level) TargetExperience = _levelExperiences[Level];
                else TargetExperience = (int)(TargetExperience * 0.15f);
                LevelUpEvent?.Invoke();
            }
            ExperienceChangedEvent?.Invoke(exp);
        }
    }
}
