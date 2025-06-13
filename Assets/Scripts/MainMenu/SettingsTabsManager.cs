using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

namespace UIDemo
{
    public class SettingsTabsManager : MonoBehaviour
    {
        [SerializeField] private TabData[] tabs;
        [SerializeField] private TextMeshProUGUI tabTitleText;
        [SerializeField] private float scrollOffset = 20f;
        [SerializeField] private TabData currentTab;

        private void Start()
        {
            if (tabs.Length > 0)
                ShowTab(tabs[0]);
        }

        private void Update() => ScrollRectNavigation();

        public void ShowTabByIndex(int index)
        {
            if (index >= 0 && index < tabs.Length)
                ShowTab(tabs[index]);
        }

        private void ShowTab(TabData newTab)
        {
            if (currentTab == newTab) return;

            foreach (var tab in tabs)
            {
                tab.tabContent.SetActive(tab == newTab);
                if (tab == newTab && tab.scrollRect != null)
                {
                    Canvas.ForceUpdateCanvases();
                    tab.scrollRect.verticalNormalizedPosition = 1f;
                }
            }
            currentTab = newTab;
            SetFocus(newTab.defaultSelected);

            if (tabTitleText != null)
                tabTitleText.text = newTab.name;
        }
        private void SetFocus(GameObject target)
        {
            if (target == null) return;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(target);
        }

        private void ScrollRectNavigation()
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            if (selected == null || currentTab == null || currentTab.scrollRect == null) return;

            ScrollRect scrollRect = currentTab.scrollRect;
            RectTransform content = scrollRect.content;
            RectTransform viewport = scrollRect.viewport;
            RectTransform selectedRect = selected.GetComponent<RectTransform>();

            if (selectedRect == null || !selectedRect.IsChildOf(content)) return;

            Canvas.ForceUpdateCanvases();

            Vector3[] itemCorners = new Vector3[4];
            Vector3[] viewportCorners = new Vector3[4];
            selectedRect.GetWorldCorners(itemCorners);
            viewport.GetWorldCorners(viewportCorners);

            float itemTop = itemCorners[1].y;
            float itemBottom = itemCorners[0].y;
            float viewportTop = viewportCorners[1].y;
            float viewportBottom = viewportCorners[0].y;

            float offset = 0f;

            if (itemTop > viewportTop)
                offset = itemTop - viewportTop + scrollOffset;
            else if (itemBottom < viewportBottom)
                offset = itemBottom - viewportBottom - scrollOffset;
            else
                return;

            float contentHeight = content.rect.height;
            float viewportHeight = viewport.rect.height;
            float scrollDelta = offset / (contentHeight - viewportHeight);
            float targetPos = Mathf.Clamp01(scrollRect.verticalNormalizedPosition - scrollDelta);
            scrollRect.verticalNormalizedPosition = targetPos;
        }
    }
}
