namespace GameTool
{
    public static class GameDataControl
    {
        public static void SaveAllData()
        {
            GameData.Instance.SaveData(eData.MuteAll, GameData.Instance.Data.MuteAll);
            GameData.Instance.SaveData(eData.PushAlarm, GameData.Instance.Data.PushAlarm);
            GameData.Instance.SaveData(eData.SoundFX, GameData.Instance.Data.SoundFX);
            GameData.Instance.SaveData(eData.MusicFX, GameData.Instance.Data.MusicFX);
            GameData.Instance.SaveData(eData.Vibration, GameData.Instance.Data.Vibration);
            GameData.Instance.SaveData(eData.CurrentLevel, GameData.Instance.Data.CurrentLevel);
        }

        public static void LoadAllData()
        {
            GameData.Instance.LoadData(eData.MuteAll, ref GameData.Instance.Data.MuteAll);
            GameData.Instance.LoadData(eData.PushAlarm, ref GameData.Instance.Data.PushAlarm);
            GameData.Instance.LoadData(eData.SoundFX, ref GameData.Instance.Data.SoundFX);
            GameData.Instance.LoadData(eData.MusicFX, ref GameData.Instance.Data.MusicFX);
            GameData.Instance.LoadData(eData.Vibration, ref GameData.Instance.Data.Vibration);
            GameData.Instance.LoadData(eData.CurrentLevel, ref GameData.Instance.Data.CurrentLevel);
        }
    }
}
