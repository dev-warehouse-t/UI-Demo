using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace UIDemo
{
    public class ScreenFader : MonoBehaviour
    {
        public static ScreenFader Instance { get; private set; }
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 0.5f;
        private bool isFadingInScene = false;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvasGroup);
            canvasGroup.alpha = 1;
        }
        private void Start() => FadeIn();
        public void FadeToScene(string sceneName)
        {
            if (isFadingInScene) return;
            isFadingInScene = true;

            canvasGroup.DOFade(1, fadeDuration).OnComplete(() =>
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(sceneName);
            });
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            canvasGroup.alpha = 1;
            FadeIn();
            isFadingInScene = false;
        }
        private void FadeIn() => canvasGroup.DOFade(0, fadeDuration);
    }
}