using Managers;
using TMPro;
using UnityEngine;

namespace UI.SkillsTree
{
    public class SkillsTreeUI : MonoBehaviour
    {
        [Header("Tooltip")]
        [SerializeField] private GameObject skillsTooltip;
        [SerializeField] private TextMeshProUGUI tooltipTitle;
        [SerializeField] private TextMeshProUGUI tooltipDescription;
        [Space]
        [SerializeField] private TextMeshProUGUI skillPointsCounter;

        private readonly Vector3 _tooltipOffset = new Vector3(300, 0, 0);

        private GameManager _gameManager;
        
        private void Start()
        {
            _gameManager = GameManager.Instance;

            skillPointsCounter.text = "0";
            _gameManager.PlayerData.SkillPointsChangedEvent += HandleSkillPointsChanged;
        }

        private void OnDisable()
        {
            HideTooltip();
        }

        private void HandleSkillPointsChanged()
        {
            skillPointsCounter.text = _gameManager.PlayerData.SkillPoints.ToString();
        }

        public void ShowTooltip(SkillUI skill)
        {
            skillsTooltip.SetActive(true);
            skillsTooltip.transform.localPosition = skill.transform.localPosition + _tooltipOffset; 
            tooltipTitle.text = skill.title;
            tooltipDescription.text = skill.description;
        }

        public void HideTooltip()
        {
            skillsTooltip.SetActive(false);
        }
    }
}
