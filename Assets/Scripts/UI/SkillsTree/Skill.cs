using Entities.Player;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.SkillsTree
{
    public class Skill : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool isOpened;
        [SerializeField] private Skill skillToOpenThis;
        [SerializeField] private Image openedSkillLine;
        [Space]
        public string title;
        public string description;

        private PlayerData _playerData;
        
        private void Start()
        {
            if (!isOpened) return;
            if (openedSkillLine) openedSkillLine.color = Color.yellow;
            _playerData = GameManager.PlayerData;
        }

        public void OpenSkill()
        {
            if (isOpened || 
                (skillToOpenThis is not null && !skillToOpenThis.isOpened) ||
                !_playerData.UseSkillPoints(1)) return;
            isOpened = true;
            openedSkillLine.color = Color.yellow;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OpenSkill();
        }
    }
}
