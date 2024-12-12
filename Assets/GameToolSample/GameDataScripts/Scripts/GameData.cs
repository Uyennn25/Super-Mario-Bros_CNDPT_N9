using System;
using System.Collections.Generic;
using GameTool.Assistants.DesignPattern;
using GameTool.GameDataScripts;
using GameToolSample.GameConfigScripts;
using GameToolSample.Scripts.UI.ResourcesItems;
using UnityEngine;

namespace GameToolSample.GameDataScripts.Scripts
{
    public class GameData : SingletonMonoBehaviour<GameData>
    {
        [Header("DATA INFO")] public static bool allDataLoaded;
        public DataField Data;
        public GameModeData GameModeData;


        protected override void Awake()
        {
            base.Awake();
            SaveGameData.LoadAllData();
        }

        public void OnAllDataLoaded()
        {
            SetDataFake();

            allDataLoaded = true;

            Debug.Log("All Data Loaded!!!");
        }

        #region FAKE

        public int CoinFake;
        public int DiamondFake;

        public void SetDataFake()
        {
            CoinFake = Coin;
            DiamondFake = Diamond;
        }

        #endregion

        #region MORE DATA

        public bool FirstOpen
        {
            get => Data.FirstOpen;
            set
            {
                Data.FirstOpen = value;
                SaveGameData.SaveData(eData.FirstOpen, Data.FirstOpen);
            }
        }

        public bool FirstPlay
        {
            get => Data.FirstPlay;
            set
            {
                Data.FirstPlay = value;
                SaveGameData.SaveData(eData.FirstPlay, Data.FirstPlay);
            }
        }

        public bool Rated
        {
            get => Data.Rated;
            set
            {
                Data.Rated = value;
                SaveGameData.SaveData(eData.Rated, Data.Rated);
            }
        }

        public bool RemoveAds
        {
            get => Data.RemoveAds;
            set
            {
                Data.RemoveAds = value;
                SaveGameData.SaveData(eData.RemoveAds, Data.RemoveAds);
            }
        }

        #endregion MORE DATA

        #region SETTING

        public bool MuteAll
        {
            get => Data.MuteAll;
            set
            {
                Data.MuteAll = value;
                SaveGameData.SaveData(eData.MuteAll, Data.MuteAll);
            }
        }

        public bool Vibrate
        {
            get => Data.Vibrate;
            set
            {
                Data.Vibrate = value;
                SaveGameData.SaveData(eData.Vibrate, Data.Vibrate);
            }
        }

        public bool Music
        {
            get => Data.Music;
            set
            {
                Data.Music = value;
                SaveGameData.SaveData(eData.Music, Data.Music);
            }
        }

        public bool SoundFX
        {
            get => Data.SoundFX;
            set
            {
                Data.SoundFX = value;
                SaveGameData.SaveData(eData.SoundFX, Data.SoundFX);
            }
        }

        public float MasterVolume
        {
            get => Data.MasterVolume;
            set
            {
                Data.MasterVolume = value;
                SaveGameData.SaveData(eData.MasterVolume, Data.MasterVolume);
            }
        }

        public float MusicVolume
        {
            get => Data.MusicVolume;
            set
            {
                Data.MusicVolume = value;
                SaveGameData.SaveData(eData.MusicVolume, Data.MusicVolume);
            }
        }

        public float SoundFXVolume
        {
            get => Data.SoundFXVolume;
            set
            {
                Data.SoundFXVolume = value;
                SaveGameData.SaveData(eData.SoundFXVolume, Data.SoundFXVolume);
            }
        }

        #endregion SETTING

        #region GAMEPLAY

        public int VictoryCount
        {
            get => Data.VictoryCount;
            set
            {
                Data.VictoryCount = value;
                SaveGameData.SaveData(eData.VictoryCount, Data.VictoryCount);
            }
        }

        public int LoseCount
        {
            get => Data.LoseCount;
            set
            {
                Data.LoseCount = value;
                SaveGameData.SaveData(eData.LoseCount, Data.LoseCount);
            }
        }

        #endregion GAMEPLAY

        #region LEVEL

        public int CurrentLevel
        {
            get => Data.CurrentLevel;
            set
            {
                Data.CurrentLevel = value;

                if (Data.CurrentLevel >= GameConfig.Instance.TotalLevel)
                {
                    Data.CurrentLevel = GameConfig.Instance.TotalLevel;
                }

                if (value > LevelUnlocked)
                {
                    LevelUnlocked = value;
                }

                SaveGameData.SaveData(eData.CurrentLevel, Data.CurrentLevel);
            }
        }

        public int LevelUnlocked
        {
            get => Data.LevelUnlocked;
            set
            {
                if (value > GameConfig.Instance.TotalLevel)
                {
                    value = GameConfig.Instance.TotalLevel;
                }

                Data.LevelUnlocked = value;

                SaveGameData.SaveData(eData.LevelUnlocked, Data.LevelUnlocked);
            }
        }

        public List<int> ListLevelUnlockID
        {
            get => Data.ListLevelUnlockID;
        }

        public void UnlockLevel(int id)
        {
            if (!Data.ListLevelUnlockID.Contains(id))
            {
                Data.ListLevelUnlockID.Add(id);
                SaveGameData.SaveData(eData.ListLevelUnlockID, Data.ListLevelUnlockID);
            }
        }

        public void LockLevel(int id)
        {
            if (Data.ListLevelUnlockID.Contains(id))
            {
                Data.ListLevelUnlockID.Remove(id);
                SaveGameData.SaveData(eData.ListLevelUnlockID, Data.ListLevelUnlockID);
            }
        }

        public bool CheckLevelUnlock(int id)
        {
            if (Data.ListLevelUnlockID.Contains(id))
            {
                return true;
            }

            return false;
        }

        #endregion LEVEL

        #region CURRENCY

        /// <summary>
        /// Không sử dụng biến này để cộng trừ Coin mà dùng hàm AddCurrency ở bên trên
        /// </summary>
        public int Coin
        {
            get => Data.Coin;
            private set
            {
                Data.Coin = value;
                SaveGameData.SaveData(eData.Coin, Data.Coin);
            }
        }

        /// <summary>
        /// Không sử dụng biến này để cộng trừ Diamond mà dùng hàm AddCurrency ở bên trên
        /// </summary>
        public int Diamond
        {
            get => Data.Diamond;
            private set
            {
                Data.Diamond = value;
                SaveGameData.SaveData(eData.Diamond, Data.Diamond);
            }
        }

        #endregion CURRENCY

        public void CollectItem(CurrencyInfo currencyInfo, bool setFake = true)
        {
            var amount = currencyInfo.value;

            if (currencyInfo.itemResourceType == ItemResourceType.Coin)
            {
                Coin += amount;
                if (setFake)
                {
                    CoinFake += amount;
                }
            }

            if (currencyInfo.itemResourceType == ItemResourceType.Diamond)
            {
                Diamond += amount;
                if (setFake)
                {
                    DiamondFake += amount;
                }
            }
        }

        public void SpendItem(CurrencyInfo currencyInfo, bool setFake = true)
        {
            var amount = -currencyInfo.value;

            if (currencyInfo.itemResourceType == ItemResourceType.Coin)
            {
                Coin += amount;
                if (setFake)
                {
                    CoinFake += amount;
                }
            }

            if (currencyInfo.itemResourceType == ItemResourceType.Diamond)
            {
                Diamond += amount;
                if (setFake)
                {
                    DiamondFake += amount;
                }
            }
        }
// AUTO GENERATE
    }

    [Serializable]
    public class GameModeData
    {
        public GameMode GameMode;
        public int Level;
    }

    public enum GameMode
    {
        None,
        Normal,
    }
}