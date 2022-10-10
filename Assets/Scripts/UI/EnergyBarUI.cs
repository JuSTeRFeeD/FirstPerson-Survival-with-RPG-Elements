using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class EnergyBarUI : MonoBehaviour
    {
        [SerializeField] private Energy energy;
        private Image _bar;

        private void Start()
        {
            _bar = GetComponent<Image>();
        }

        private void Update()
        {
            _bar.fillAmount = energy.CurrentValue / energy.MaxValue;
        }
    }
}
