using _ProjectTemplate.Scripts.GamePlay.Player;
using _ProjectTemplate.Scripts.UI;
using GameTool.Assistants.DesignPattern;
using GameTool.Audio.Scripts;
using GameTool.UI.Scripts.CanvasPopup;
using GameToolSample.Audio;
using GameToolSample.GameConfigScripts;
using GameToolSample.GameDataScripts.Scripts;
using GameToolSample.GamePlay.Manager;
using GameToolSample.Scripts.Enum;
using GameToolSample.Scripts.LoadScene;
using GameToolSample.Scripts.UI.ResourcesItems;
using GameToolSample.UIManager;
using UnityEngine;

namespace _ProjectTemplate.Scripts.Managers
{
    public class GameController : GameManager
    {
        public new static GameController Instance => (GameController)GameManager.Instance;

        #region Variable

        [Header("PLAYER")] [SerializeField] private PlayerController _playerController;
        [SerializeField] private int countLives = 3;

        [Header("POSITION SPAWN PLAYER")] [SerializeField]
        private Transform _startPosition;

        private int currentLevel;

        #endregion

        #region Properties

        public PlayerController PlayerController => _playerController;

        public Transform StartPosition => _startPosition;

        public int CountLives => countLives;

        public bool IsPlayingGame => GameplayStatus == AnalyticID.GamePlayState.playing;

        #endregion

        #region Init

        protected override void Awake()
        {
            base.Awake();
            Init();

            currentLevel = GameData.Instance.GameModeData.Level;
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayMusic(eMusicName.GameMusic);
            AudioManager.Instance.SetMusicVolume(0.6f);
        }

        protected override void Start()
        {
            base.Start();
            PlayGame();
        }

        private void Init()
        {
            PlayerController.PlayerHealth.SetLives(CountLives);
            PlayerController.Init();
        }

        public override void PlayGame()
        {
            base.PlayGame();
            Debug.LogError("PlayGame");
        }

        #endregion

        #region Game Status

        public override void Lose()
        {
            base.Lose();
            CanvasManager.Instance.Push(eUIName.LosePopup);
            Debug.LogError("Lose");
        }

        public override void Victory()
        {
            base.Victory();
            CanvasManager.Instance.Push(eUIName.WinPopup);
            Debug.LogError("Victory");
        }

        public override void Revive()
        {
            base.Revive();
            PlayerController.Init();
            Debug.LogError("Revive");
        }


        public override void ContinueGame()
        {
            base.ContinueGame();
            Debug.LogError("ContinueGame");
        }

        public override void PauseGame()
        {
            base.PauseGame();
            Debug.LogError("PauseGame");
        }

        public override void ReplayLevel()
        {
            base.ReplayLevel();

            Debug.LogError("ReplayLevel");
        }

        public void NextLevel()
        {
            AudioManager.Instance.Shot(eSoundName.PassLevel);
            GameData.Instance.CurrentLevel = currentLevel + 1;
            if (GameData.Instance.CurrentLevel > GameConfig.Instance.TotalLevel)
            {
                Victory();
                return;
            }

            SceneLoadManager.Instance.LoadSceneLevel(GameData.Instance.CurrentLevel);
        }

        #endregion

        #region API

        public void AddCoin()
        {
            var value = 100;
            AudioManager.Instance.Shot(eSoundName.CollectCoin);
            GameData.Instance.CollectItem(new CurrencyInfo(ItemResourceType.Coin, value));
            this.PostEvent(EventID.UpdateData);
        }

        public void AddLife()
        {
            PlayerController.AddLife();
            MenuGameplay.Instance.UpdateLives(PlayerController.PlayerHealth.Lives);
        }

        #endregion
    }
}