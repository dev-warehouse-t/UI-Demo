using UnityEngine;
using UnityEngine.InputSystem;

namespace UIDemo
{
    public class MenuNavigationHandler : MonoBehaviour
    {
        [SerializeField] private MenuManager menuManager;
        public void OnBack(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            if (menuManager != null)
                menuManager.Back();
        }
    }
}
