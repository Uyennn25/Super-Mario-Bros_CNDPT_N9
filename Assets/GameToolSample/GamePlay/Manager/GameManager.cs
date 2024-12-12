#if !Minify
using GameTool.Assistants.DesignPattern;
using GameTool.UI.Scripts.CanvasPopup;
using GameToolSample.GameDataScripts.Scripts;
using GameToolSample.Scripts.Enum;
using GameToolSample.Scripts.LoadScene;
using GameToolSample.UIManager;
using UnityEngine;
using static GameToolSample.Scripts.Enum.AnalyticID;

namespace GameToolSample.GamePlay.Manager
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [Header("COMPONENT")] public GamePlayState GameplayStatus = GamePlayState.none;
        public GameModeName GameMode = GameModeName.none;

        private int _currentLevel;

        protected virtual void Start()
        {
            _currentLevel = GameData.Instance.CurrentLevel;
        }

        public bool IsGamePlayStatus(GamePlayState gamePlayState)
        {
            return GameplayStatus == gamePlayState;
        }

        #region STATUS INGAME

        public virtual void PlayGame()
        {
            GameplayStatus = GamePlayState.playing;
        }

        public virtual void Victory()
        {
            GameplayStatus = GamePlayState.victory;
            ShowVictoryPopup();
        }

        public virtual void Lose()
        {
            GameplayStatus = GamePlayState.lose;
            ShowLosePopup();
        }

        protected virtual void ShowVictoryPopup()
        {
            
        }

        public virtual void PauseGame()
        {
            GameplayStatus = GamePlayState.pause;
            ShowPausePopup();
        }
        
        public virtual void ContinueGame()
        {
            GameplayStatus = GamePlayState.playing;
        }

        protected virtual void ShowPausePopup()
        {
        }

        protected virtual void ShowLosePopup()
        {
        }

        public virtual void CheckRevive()
        {
            if (GameplayStatus == GamePlayState.revive) return;
            GameplayStatus = GamePlayState.revive;
            Revive();
        }

        public virtual void Revive()
        {
            GameplayStatus = GamePlayState.playing;
            this.PostEvent(EventID.PlayerRevive);
        }

        protected virtual void ShowRevivePopup()
        {
            CanvasManager.Instance.Push(eUIName.RevivePopup);
        }

        public virtual void SkipLevel()
        {
            GameData.Instance.CurrentLevel++;
        }

        public virtual void ReplayLevel()
        {
            GameplayStatus = GamePlayState.replay;
            SceneLoadManager.Instance.LoadCurrentScene();
        }

        #endregion
    }
}

#endif