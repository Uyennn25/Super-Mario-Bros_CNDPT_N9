using _ProjectTemplate.Scripts.Managers;
using _ProjectTemplate.Scripts.Others;
using GameTool.UI.Scripts.CanvasPopup;
using GameToolSample.Scripts.LoadScene;
using UnityEngine;

namespace _ProjectTemplate.Scripts.UI
{
    public class SettingPopup : SingletonUI<SettingPopup>
    {
        [SerializeField] private ButtonUI _resumeBtn;
        [SerializeField] private ButtonUI _homeBtn;
        [SerializeField] private ButtonUI _closeBtn;
        
        private void Start()
        {
            _resumeBtn.onClick.AddListener(ResumeListener);
            _homeBtn.onClick.AddListener(HomeListener);
            _closeBtn.onClick.AddListener(ResumeListener);
        }

        private void ResumeListener()
        {
            GameController.Instance.ContinueGame();
            Pop();
        }

        /// Restart Level
        private void HomeListener()
        {
            SceneLoadManager.Instance.LoadSceneHome();
        }
    }
}