using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillButton : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            fillImage.fillAmount = 0;
        }

        public void SetInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }

        public void SetFillAmount(float amount)
        {
            fillImage.fillAmount = Mathf.Clamp(amount ,0f, 1f); 
        }
    }
}
