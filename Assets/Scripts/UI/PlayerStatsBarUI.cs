using Entities.Player;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerStatsBarUI : MonoBehaviour
    {
        private PlayerData _playerData;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private Image experienceProgressBar;
        [Space]
        [SerializeField] private Energy energy;
        [SerializeField] private Image energyBar;

        private void Start()
        {
            _playerData = GameManager.Instance.PlayerData;
            var lvl = _playerData.Leveling;
            levelText.text = lvl.Level.ToString();
            experienceText.text = $"{lvl.CurrentExperience} / {lvl.TargetExperience}";
            experienceProgressBar.fillAmount = lvl.ExperienceProgress;
            
            energy.EnergyChangedEvent += HandleEnergyChanged;
            _playerData.Leveling.LevelUpEvent += HandleLevelChanged;
            _playerData.Leveling.ExperienceChangedEvent += HandleExperienceChanged;
        }

        private void OnDestroy()
        {
            energy.EnergyChangedEvent -= HandleEnergyChanged;
            _playerData.Leveling.LevelUpEvent -= HandleLevelChanged;
            _playerData.Leveling.ExperienceChangedEvent -= HandleExperienceChanged;
        }

        private void HandleEnergyChanged()
        {
            energyBar.fillAmount = energy.PercentOfEnergy;
        }

        private void HandleLevelChanged()
        {
            levelText.text = _playerData.Leveling.Level.ToString();
        }
        
        private void HandleExperienceChanged(int expAmount)
        {
            var lvl = _playerData.Leveling;
            experienceText.text = $"{lvl.CurrentExperience} / {lvl.TargetExperience}";
            experienceProgressBar.fillAmount = lvl.ExperienceProgress;
        }
    }
}