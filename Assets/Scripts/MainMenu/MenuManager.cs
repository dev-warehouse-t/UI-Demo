using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace UIDemo
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private MenuData[] menus;
        private MenuData currentMenu;
        private void Start()
        {
            if (menus.Length > 0)
                ShowMenu(menus[0], instant: true);
        }
        public void OpenMenuByIndex(int index)
        {
            if (index >= 0 && index < menus.Length)
                SwitchMenu(menus[index]);
        }
        public void OpenMainMenu() => SwitchMenu(GetMenuByName("MainMenu"));
        public void OpenSettings() => SwitchMenu(GetMenuByName("Settings"));
        public void OpenCredits() => SwitchMenu(GetMenuByName("Credits"));
        public void OpenLoadAndSave() => SwitchMenu(GetMenuByName("Load/Save"));
        public void Back() => OpenMainMenu();

        private void SwitchMenu(MenuData newMenu)
        {
            if (currentMenu == newMenu) return;

            if (currentMenu != null)
            {
                currentMenu.menuGroup.DOFade(0f, 0.2f);
                currentMenu.menuGroup.interactable = false;
                currentMenu.menuGroup.blocksRaycasts = false;
            }
            newMenu.menuGroup.gameObject.SetActive(true);
            newMenu.menuGroup.alpha = 0f;
            newMenu.menuGroup.DOFade(1f, 0.3f);
            newMenu.menuGroup.interactable = true;
            newMenu.menuGroup.blocksRaycasts = true;

            currentMenu = newMenu;
            SetFocus(newMenu.defaultSelected);
        }
        private void ShowMenu(MenuData menu, bool instant = false)
        {
            menu.menuGroup.gameObject.SetActive(true);
            menu.menuGroup.alpha = instant ? 1f : 0f;
            menu.menuGroup.interactable = true;
            menu.menuGroup.blocksRaycasts = true;

            if (!instant)
                menu.menuGroup.DOFade(1f, 0.3f);

            currentMenu = menu;
            SetFocus(menu.defaultSelected);
        }
        private void SetFocus(GameObject target)
        {
            if (target == null) return;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(target);
        }
        private MenuData GetMenuByName(string name)
        {
            foreach (var menu in menus)
            {
                if (menu.name == name)
                    return menu;
            }
            return null;
        }
    }
}