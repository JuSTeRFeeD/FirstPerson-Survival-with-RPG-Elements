using System;
using Entities;
using Managers;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class PlayerStatsUI : MonoBehaviour
    {
        private EntityStats _playerStats;
        [SerializeField] private TextMeshProUGUI stats;

        private void Start()
        {
            _playerStats = GameManager.Instance.PlayerData.Stats;
            HandleStatsChange();
            _playerStats.StatsChangeEvent += HandleStatsChange;
        }

        private void OnDestroy()
        {
            _playerStats.StatsChangeEvent -= HandleStatsChange;
        }

        private void HandleStatsChange()
        {
            stats.text = _playerStats.GetFullStats();
        }
    }
}
