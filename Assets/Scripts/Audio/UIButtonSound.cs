using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIAudioSound : MonoBehaviour
{
    public enum SoundType
    {
        Click,
        Submit,
        Slider
    }
    public SoundType soundType = SoundType.Click;
    private float lastPlayTime = 0f;
    private float minInterval = 0.05f;
    private void Awake()
    {
        var button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(PlaySound);

        var toggle = GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(OnToggleValueChanged);

        var slider = GetComponent<Slider>();
        if (slider != null)
            slider.onValueChanged.AddListener(OnSliderValueChanged);

        var dropdown = GetComponent<TMP_Dropdown>();
        if (dropdown != null)
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }
    private void PlaySound()
    {
        if (UIAudioManager.Instance == null) return;

        switch (soundType)
        {
            case SoundType.Click:
                UIAudioManager.Instance.PlayClick();
                break;
            case SoundType.Submit:
                UIAudioManager.Instance.PlaySubmit();
                break;
            case SoundType.Slider:
                UIAudioManager.Instance.PlaySlider();
                break;
        }
    }
    private void OnToggleValueChanged(bool value)
    {
        if (soundType == SoundType.Submit)
            PlaySound();
    }
    private void OnSliderValueChanged(float value)
    {
        if (Time.time - lastPlayTime > minInterval)
        {
            if (soundType == SoundType.Slider)
                PlaySound();
            lastPlayTime = Time.time;
        }
    }
    private void OnDropdownValueChanged(int value)
    {
        if (soundType == SoundType.Submit)
            PlaySound();
    }
}