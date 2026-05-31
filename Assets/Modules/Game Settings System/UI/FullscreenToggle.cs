using UnityEngine;

[RequireComponent(typeof(ToggleButton))]
public class FullscreenToggle : MonoBehaviour
{
    private bool setDone = false;

    private ToggleButton _toggle;
    private ToggleButton toggle => _toggle ??= GetComponent<ToggleButton>();

    private void OnEnable()
    {
        toggle.OnToggle += ToggleChange;
    }

    private void OnDisable()
    {
        toggle.OnToggle -= ToggleChange;
    }

    private void Start()
    {
        toggle.toggle = Res.data.gameSettingsData.fullscreen;
        setDone = true;
    }

    private void ToggleChange(bool toggle)
    {
        if(!setDone)
            return;

        Res.data.gameSettingsData.fullscreen = toggle;
        GameSettingsManager.Apply();
    }
}
