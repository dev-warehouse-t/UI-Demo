using UnityEngine;
using UnityEngine.UI;

namespace UIDemo
{
    [System.Serializable]
    public class TabData
    {
        public string name;
        public GameObject tabContent;
        public GameObject defaultSelected;
        [Header("Scroll Navigation")]
        public ScrollRect scrollRect;
        public RectTransform content;
    }
}