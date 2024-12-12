using System;
using System.Collections.Generic;
using DatdevUlts.DateTimeScripts;
using GameTool.Assistants.DictionarySerialize;
using UnityEngine;

namespace GameToolSample.GameDataScripts.Scripts
{
    [Serializable]
    public class DataField
    {
        [Header("GAME CHECK")] public bool FirstOpen = true;

        public bool FirstPlay;
        public bool Rated;
        public bool RemoveAds;

        [Header("SETTING")] public bool MuteAll;

        public bool Music = true;
        public bool SoundFX = true;
        public bool Vibrate = true;

        public float MasterVolume = 1f;
        public float MusicVolume = 1f;
        public float SoundFXVolume = 1f;

        [Header("RESOURCES")] public int Coin;

        public int Diamond;

        [Header("GAMEPLAY")] public int VictoryCount;

        public int LoseCount;

        [Header("LEVEL")] public int CurrentLevel = 1;

        public int LevelUnlocked = 1;
        public List<int> ListLevelUnlockID = new List<int> { 0 };
    }
}