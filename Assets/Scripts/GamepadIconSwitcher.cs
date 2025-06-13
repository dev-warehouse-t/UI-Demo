using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GamepadIconSwitcher : MonoBehaviour
{
    [Header("Target UI Images")]
    public Image selectButtonIcon;
    public Image cancelButtonIcon;

    [Header("Icon Sets")]
    public GamepadIconSet xboxIcons;
    public GamepadIconSet dualsenseIcons;
    public GamepadIconSet defaultIcons;

    private Gamepad currentGamepad;
    private void Awake() => UpdateIcons();
    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        UpdateIcons();
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void Update()
    {
        if (Gamepad.current != currentGamepad)
        {
            currentGamepad = Gamepad.current;
            UpdateIcons();
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad)
            UpdateIcons();
    }

    private void UpdateIcons()
    {
        currentGamepad = Gamepad.current;
        GamepadIconSet iconSet = defaultIcons;

        if (currentGamepad != null)
        {
            string layout = currentGamepad.layout.ToLower();
            string displayName = currentGamepad.displayName.ToLower();

            if (layout.Contains("dualshock") || layout.Contains("dualsense") || displayName.Contains("wireless controller"))
            {
                iconSet = dualsenseIcons;
            }
            else if (layout.Contains("xinput") || displayName.Contains("xbox"))
            {
                iconSet = xboxIcons;
            }
        }

        if (selectButtonIcon != null) selectButtonIcon.sprite = iconSet.selectIcon;
        if (cancelButtonIcon != null) cancelButtonIcon.sprite = iconSet.cancelIcon;
    }

    [System.Serializable]
    public class GamepadIconSet
    {
        public Sprite selectIcon;
        public Sprite cancelIcon;
    }
}
