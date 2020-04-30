using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public float defaultVolume;
    public AudioMixer audioMixer;
    GameObject volumeSlider;
    Slider slider;
    Toggle fullscreenToggle;
    public bool fullscreenToggleBool = true;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string tempOption = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(tempOption);
        }
        resolutionDropdown.AddOptions(options);

        volumeSlider = GameObject.Find("VolumeSlider");
        if (volumeSlider != null)
        {
            slider = volumeSlider.GetComponent<Slider>();

            if (slider != null)
            {
                slider.normalizedValue = defaultVolume;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("volume", (volume - 80));
    }

    public void setQuality(int qIndex)
    {
        QualitySettings.SetQualityLevel(qIndex);
    }

    public void setFullscreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void setResolution(int rIndex)
    {
        Resolution tempResolution = resolutions[rIndex];
        Screen.SetResolution(tempResolution.width, tempResolution.height, Screen.fullScreen);
    }
}
