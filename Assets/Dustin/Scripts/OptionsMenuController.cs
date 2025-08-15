using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public static class NavigationContext
{
    public static string ReturnSceneName = "MainMenu_Scene";
}

public class OptionsMenuController : MonoBehaviour
{
    [Header("Display Settings")]
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown windowModeDropdown;

    [Header("Volume Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Volume Labels")]
    public TMP_Text musicValueText;
    public TMP_Text sfxValueText;

    Resolution[] resolutions;

    private void Start()
    {
        if (resolutionDropdown != null) SetupResolutionDropdown();
        if (windowModeDropdown != null) SetupWindowModeDropdown();

        float music = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 0.75f);

        if (musicSlider != null)
        {
            musicSlider.value = music;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            UpdateMusicText(music);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfx;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
            UpdateSFXText(sfx);
        }
    }

    public void BackToMain()
    {
        var target = string.IsNullOrEmpty(NavigationContext.ReturnSceneName)
            ? "MainMenu_Scene"
            : NavigationContext.ReturnSceneName;

        SceneManager.LoadScene(target);
    }

    private void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
        UpdateMusicText(value);
    }

    private void SetSFXVolume(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFXVolume", value);
        UpdateSFXText(value);
    }

    private void UpdateMusicText(float value)
    {
        if (musicValueText != null)
            musicValueText.text = FormatVolume(value);
    }

    private void UpdateSFXText(float value)
    {
        if (sfxValueText != null)
            sfxValueText.text = FormatVolume(value);
    }

    private string FormatVolume(float val)
    {
        return val <= 0.0001f ? "0%" : Mathf.RoundToInt(val * 100f) + "%";
    }

    private void SetupResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResIndex = 0;
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            var res = resolutions[i];
            string option = res.width + " x " + res.height;
            options.Add(option);

            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
                currentResIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.SetValueWithoutNotify(currentResIndex); // <- key change
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
    }

    private void SetupWindowModeDropdown()
    {
        windowModeDropdown.ClearOptions();
        windowModeDropdown.AddOptions(new System.Collections.Generic.List<string> {
            "Borderless", "Windowed", "Fullscreen"
        });

        // Match current mode without notifying
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.FullScreenWindow: windowModeDropdown.SetValueWithoutNotify(0); break;
            case FullScreenMode.Windowed: windowModeDropdown.SetValueWithoutNotify(1); break;
            case FullScreenMode.ExclusiveFullScreen: windowModeDropdown.SetValueWithoutNotify(2); break;
            default: windowModeDropdown.SetValueWithoutNotify(0); break;
        }

        windowModeDropdown.RefreshShownValue();
        windowModeDropdown.onValueChanged.AddListener(SetWindowMode);
    }

    private void SetWindowMode(int index)
    {
        switch (index)
        {
            case 0: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
            case 1: Screen.fullScreenMode = FullScreenMode.Windowed; break;
            case 2: Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
        }
    }

    public void ApplySettings()
    {
        if (musicSlider != null) PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        if (sfxSlider != null) PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.Save();
        Debug.Log("[OptionsMenu_Controller] Settings applied.");
    }
}
