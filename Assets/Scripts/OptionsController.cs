using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider volumeSlider;

    public Text volumeText;
    public Text playersText;

    // Use this for initialization
    void Start()
    {
        volumeSlider.value = Preferences.MasterVolume;
        SetVolumeText();
        SetPlayersText();
    }

    public void VolumeChanged()
    {
        Preferences.MasterVolume = volumeSlider.value;
        SetVolumeText();
    }

    public void PlayersChanged()
    {
        switch (Preferences.PlayersNumber)
        {
            case Preferences.Players.One:
                Preferences.PlayersNumber = Preferences.Players.Two;
                break;
            case Preferences.Players.Two:
                Preferences.PlayersNumber = Preferences.Players.One;
                break;
            default:
                Debug.LogError($"Number of players {Preferences.PlayersNumber} is not implemented.");
                break;
        }
        SetPlayersText();
    }

    public void ResetToDefaults()
    {
        Preferences.ResetUserPrefsToDefaults();
        volumeSlider.value = Preferences.MasterVolume;
    }

    private void SetVolumeText() => volumeText.text = Mathf.RoundToInt(volumeSlider.value * 100).ToString();

    private void SetPlayersText() => playersText.text = ((int)Preferences.PlayersNumber).ToString();
}
