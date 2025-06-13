using UnityEngine;
using TMPro;
using DG.Tweening;

public class MenuButtonEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    private Tween glowTween;
    private void Start()
    {
        if (buttonText != null)
        {
            glowTween = buttonText.transform
                .DOScale(1.03f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);

            buttonText.DOColor(Color.gray, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    }
    private void OnDestroy()
    {
        glowTween?.Kill();
        DOTween.Kill(buttonText);
    }
}