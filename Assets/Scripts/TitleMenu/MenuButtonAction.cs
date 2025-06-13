using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace UIDemo
{
    public class MenuButtonAction : MonoBehaviour
    {
        [SerializeField] private const string sceneToLoad = "MainMenu";

        void Update()
        {
            if (Keyboard.current.anyKey.wasPressedThisFrame ||
                Mouse.current.leftButton.wasPressedThisFrame ||
                Mouse.current.rightButton.wasPressedThisFrame ||
                Gamepad.current != null && Gamepad.current.allControls.Any(c => c is ButtonControl b && b.wasPressedThisFrame))
            {
                OnClick();
            }
        }
        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            PerformAction();
        }
        public void OnClick() => PerformAction();
        private void PerformAction()
        {
            if (ScreenFader.Instance != null)
                ScreenFader.Instance.FadeToScene(sceneToLoad);
        }
    }
}