using Interactable;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Entities.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private Camera mainCam;
        [SerializeField] private LayerMask interactLayerMask;
        [SerializeField] private float distanceToInteract = 2f;
        
        private void Start()
        {
            GameManager.GameControls.PlayerControls.Interact.performed += HandleInteract;
        }

        private void HandleInteract(InputAction.CallbackContext ctx)
        {
            var ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (!Physics.Raycast(ray, out var hit, distanceToInteract, interactLayerMask)) return;
            if (hit.collider.isTrigger && hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            var cameraTtTransform = mainCam.transform;
            Gizmos.DrawLine(cameraTtTransform.position, cameraTtTransform.position + cameraTtTransform.forward * distanceToInteract);
        }
    }
}