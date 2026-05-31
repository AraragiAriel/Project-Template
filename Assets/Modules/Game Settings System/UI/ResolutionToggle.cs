using UnityEngine;

[RequireComponent(typeof(ToggleButton))]
public class ResolutionToggle : MonoBehaviour
{
    [SerializeField] private GameObject resolutionSelector;

    private ToggleButton _toggleButton;
    private ToggleButton toggleButton => _toggleButton ??= GetComponent<ToggleButton>();

    private bool setupDone = false;

    void OnEnable()
    {
        toggleButton.OnToggle += Toggle;
    }

    void OnDisable()
    {
        toggleButton.OnToggle -= Toggle;
    }

    void Start()
    {
        toggleButton.toggle = Res.data.gameSettingsData.nativeResolution;
        SetSelector();

        setupDone = true;
    }

    private void Toggle(bool toggle)
    {
        if(!setupDone)
            return;
            
        Res.data.gameSettingsData.nativeResolution = toggle;
        SetSelector();
    }

    private void SetSelector() => resolutionSelector.SetActive(!Res.data.gameSettingsData.nativeResolution);
}
