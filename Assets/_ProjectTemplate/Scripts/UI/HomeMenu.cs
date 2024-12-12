using _ProjectTemplate.Scripts.Others;
using GameTool.Audio.Scripts;
using GameTool.UI.Scripts.CanvasPopup;
using GameToolSample.Audio;
using GameToolSample.Scripts.LoadScene;
using GameToolSample.UIManager;
using UnityEngine;

namespace _ProjectTemplate.Scripts.UI
{
    public class HomeMenu : SingletonUI<HomeMenu>
    {
        [SerializeField] private ButtonUI _newGameBtn;
        [SerializeField] private ButtonUI _levelBtn;
        [SerializeField] private ButtonUI _tutorialBtn;

        private void OnEnable()
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayMusic(eMusicName.BG_Home);
        }

        protected override void Awake()
        {
            base.Awake();
            _newGameBtn.onClick.AddListener(NewGameListener);
            _levelBtn.onClick.AddListener(LevelListener);
            _tutorialBtn.onClick.AddListener(TutorialListener);
        }

        private void TutorialListener()
        {
            CanvasManager.Instance.Push(eUIName.TutorialPopup);
        }

        private void NewGameListener()
        {
            SceneLoadManager.Instance.LoadSceneLevel(1);
        }

        private void LevelListener()
        {
            CanvasManager.Instance.Push(eUIName.SelectLevelPopup);
        }
    }
}