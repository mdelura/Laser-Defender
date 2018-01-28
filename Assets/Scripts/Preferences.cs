using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Preferences
{
    public enum Players
    {
        One = 1,
        Two = 2
    }

    public static event Action<PreferenceChangedEventArgs> PreferenceChanged;

    public static float MasterVolume
    {
        get { return PlayerPrefs.HasKey(Keys.MasterVolume) ? PlayerPrefs.GetFloat(Keys.MasterVolume) : Defaults.MasterVolume; }
        set
        {
            if (value >= 0f && value <= 1f)
            {
                PlayerPrefs.SetFloat(Keys.MasterVolume, value);
                OnPreferenceChanged(value);
            }
            else Debug.LogError($"Master volume value {value} to be set is out of range (0-1).");
        }
    }

    public static Players PlayersNumber
    {
        get { return PlayerPrefs.HasKey(Keys.PlayersNumber) ? (Players)PlayerPrefs.GetInt(Keys.PlayersNumber) : Defaults.PlayersNumber; }
        set
        {
            PlayerPrefs.SetInt(Keys.PlayersNumber, (int)value);
            OnPreferenceChanged(value);
        }
    }

    public static Controls P1Controls
    {
        get
        {
            return new Controls()
            {
                Up = P1Up,
                Down = P1Down,
                Left = P1Left,
                Right = P1Right,
                Fire = P1Fire
            };
        }
        set
        {
            P1Up = value.Up;
            P1Down = value.Down;
            P1Left = value.Left;
            P1Right = value.Right;
            P1Fire = value.Fire;
        }
    }
    public static Controls P2Controls
    {
        get
        {
            return new Controls()
            {
                Up = P2Up,
                Down = P2Down,
                Left = P2Left,
                Right = P2Right,
                Fire = P2Fire
            };
        }
        set
        {
            P2Up = value.Up;
            P2Down = value.Down;
            P2Left = value.Left;
            P2Right = value.Right;
            P2Fire = value.Fire;
        }
    }

    private static KeyCode P1Up
    {
        get { return PlayerPrefs.HasKey(Keys.P1Up) ? (KeyCode)PlayerPrefs.GetInt(Keys.P1Up) : Defaults.P1Up; }
        set
        {
            PlayerPrefs.SetInt(Keys.P1Up, (int)value);
            OnPreferenceChanged(value);
        }
    }
    private static KeyCode P1Down
    {
        get { return PlayerPrefs.HasKey(Keys.P1Down) ? (KeyCode)PlayerPrefs.GetInt(Keys.P1Down) : Defaults.P1Down; }
        set
        {
            PlayerPrefs.SetInt(Keys.P1Down, (int)value);
            OnPreferenceChanged(value);
        }
    }
    private static KeyCode P1Left
    {
        get { return PlayerPrefs.HasKey(Keys.P1Left) ? (KeyCode)PlayerPrefs.GetInt(Keys.P1Left) : Defaults.P1Left; }
        set
        {
            PlayerPrefs.SetInt(Keys.P1Left, (int)value);
            OnPreferenceChanged(value);
        }
    }
    private static KeyCode P1Right
    {
        get { return PlayerPrefs.HasKey(Keys.P1Right) ? (KeyCode)PlayerPrefs.GetInt(Keys.P1Right) : Defaults.P1Right; }
        set
        {
            PlayerPrefs.SetInt(Keys.P1Right, (int)value);
            OnPreferenceChanged(value);
        }
    }
    private static KeyCode P1Fire
    {
        get { return PlayerPrefs.HasKey(Keys.P1Fire) ? (KeyCode)PlayerPrefs.GetInt(Keys.P1Fire) : Defaults.P1Fire; }
        set
        {
            PlayerPrefs.SetInt(Keys.P1Fire, (int)value);
            OnPreferenceChanged(value);
        }
    }

    private static KeyCode P2Up
    {
        get { return PlayerPrefs.HasKey(Keys.P2Up) ? (KeyCode)PlayerPrefs.GetInt(Keys.P2Up) : Defaults.P2Up; }
        set
        {
            PlayerPrefs.SetInt(Keys.P2Up, (int)value);
            OnPreferenceChanged(value);
        }
    }
    private static KeyCode P2Down
    {
        get { return PlayerPrefs.HasKey(Keys.P2Down) ? (KeyCode)PlayerPrefs.GetInt(Keys.P2Down) : Defaults.P2Down; }
        set
        {
            PlayerPrefs.SetInt(Keys.P2Down, (int)value);
            OnPreferenceChanged(value);
        }
    }
    private static KeyCode P2Left
    {
        get { return PlayerPrefs.HasKey(Keys.P2Left) ? (KeyCode)PlayerPrefs.GetInt(Keys.P2Left) : Defaults.P2Left; }
        set
        {
            PlayerPrefs.SetInt(Keys.P2Left, (int)value);
            OnPreferenceChanged(value);
        }
    }
    private static KeyCode P2Right
    {
        get { return PlayerPrefs.HasKey(Keys.P2Right) ? (KeyCode)PlayerPrefs.GetInt(Keys.P2Right) : Defaults.P2Right; }
        set
        {
            PlayerPrefs.SetInt(Keys.P2Right, (int)value);
            OnPreferenceChanged(value);
        }
    }
    private static KeyCode P2Fire
    {
        get { return PlayerPrefs.HasKey(Keys.P2Fire) ? (KeyCode)PlayerPrefs.GetInt(Keys.P2Fire) : Defaults.P2Fire; }
        set
        {
            PlayerPrefs.SetInt(Keys.P2Fire, (int)value);
            OnPreferenceChanged(value);
        }
    }

    private static void OnPreferenceChanged(object value, [CallerMemberName] string preferenceName = null) => PreferenceChanged?.Invoke(new PreferenceChangedEventArgs(preferenceName, value));

    public static void ResetUserPrefsToDefaults()
    {
        MasterVolume = Defaults.MasterVolume;
        PlayersNumber = Defaults.PlayersNumber;
    }


    static class Keys
    {
        public const string MasterVolume = "master_volume";
        public const string PlayersNumber = "players_number";

        public const string P1Up = "P1_up";
        public const string P1Down = "P1_down";
        public const string P1Left = "P1_left";
        public const string P1Right = "P1_right";
        public const string P1Fire = "P1_fire";

        public const string P2Up = "P2_up";
        public const string P2Down = "P2_down";
        public const string P2Left = "P2_left";
        public const string P2Right = "P2_right";
        public const string P2Fire = "P2_fire";
    }

    static class Defaults
    {
        public const float MasterVolume = 0.5F;
        public const Players PlayersNumber = Players.One;

        public const KeyCode P1Up = KeyCode.UpArrow;
        public const KeyCode P1Down = KeyCode.DownArrow;
        public const KeyCode P1Left = KeyCode.LeftArrow;
        public const KeyCode P1Right = KeyCode.RightArrow;
        public const KeyCode P1Fire = KeyCode.Space;

        public const KeyCode P2Up = KeyCode.W;
        public const KeyCode P2Down = KeyCode.S;
        public const KeyCode P2Left = KeyCode.A;
        public const KeyCode P2Right = KeyCode.D;
        public const KeyCode P2Fire = KeyCode.LeftShift;
    }
}
