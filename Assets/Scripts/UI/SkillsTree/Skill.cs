using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.SkillsTree
{
    public class Skill : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SkillsTree skillsTree;
        [Space]
        [SerializeField] private bool isOpened;
        [SerializeField] private Skill skillToOpenThis;
        [SerializeField] private Image openedSkillLine;
        [SerializeField] private Image skillIcon;
        [Space]
        public string title;
        [TextArea]
        public string description;
        
        private void Start()
        {
            if (!isOpened)
            {
                skillIcon.color = Color.gray;
                return;
            }
            if (openedSkillLine) openedSkillLine.color = Color.yellow;
            skillIcon.color = Color.white;
        }

        public void OpenSkill()
        {
            if (isOpened) return;
            if (skillToOpenThis != null && !skillToOpenThis.isOpened) return;
            if (!GameManager.Instance.PlayerData.UseSkillPoints(1)) return;
            isOpened = true;
            if (openedSkillLine) openedSkillLine.color = Color.yellow;
            skillIcon.color = Color.white;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OpenSkill();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            skillsTree.ShowTooltip(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            skillsTree.HideTooltip();
        }
    }
}
