using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AimUI : MonoBehaviour
    {
        // TODO: Добавлять реализацию различных AIM спрайтов для разных типо оружия сюда
        [SerializeField] private Image aimHit;
        private WaitForSeconds _delay;

        private void Start()
        {
            _delay = new WaitForSeconds(0.1f);
            aimHit.enabled = false;
        }

        public void PlayAimHit()
        {
            StartCoroutine(nameof(AimHit));
        }

        private IEnumerator AimHit()
        {
            aimHit.enabled = true;
            yield return _delay;
            aimHit.enabled = false;
        }
        
    }
}
