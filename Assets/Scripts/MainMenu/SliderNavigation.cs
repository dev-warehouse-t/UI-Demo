using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UIDemo
{
    public class SliderNavigation : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        private Slider slider;
        private Navigation originalNavigation;
        private bool isSelected;
        private bool isHoldingSubmit;
        void Awake()
        {
            slider = GetComponent<Slider>();
            originalNavigation = slider.navigation;
        }
        void Update()
        {
            if (!isSelected) return;

            bool currentlyHolding = Gamepad.current != null
                ? Gamepad.current.buttonSouth.isPressed
                : Keyboard.current != null && Keyboard.current.enterKey.isPressed;

            if (currentlyHolding && !isHoldingSubmit)
            {
                DisableNavigation();
                isHoldingSubmit = true;
            }
            else if (!currentlyHolding && isHoldingSubmit)
            {
                RestoreNavigation();
                isHoldingSubmit = false;
            }
        }
        public void OnSelect(BaseEventData eventData)
        {
            isSelected = true;
            originalNavigation = slider.navigation;
        }
        public void OnDeselect(BaseEventData eventData)
        {
            isSelected = false;
            isHoldingSubmit = false;
            RestoreNavigation();
        }
        private void DisableNavigation()
        {
            Navigation nav = slider.navigation;
            nav.mode = Navigation.Mode.None;
            slider.navigation = nav;
        }
        private void RestoreNavigation()
        {
            slider.navigation = originalNavigation;
        }
    }
}