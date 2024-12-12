using _ProjectTemplate.Scripts.Others;
using GameTool.Audio.Scripts;
using GameTool.UI.Scripts.CanvasPopup;
using GameToolSample.Audio;
using GameToolSample.Scripts.LoadScene;
using UnityEngine;

namespace _ProjectTemplate.Scripts.UI
{
    public class LosePopup : SingletonUI<LosePopup>
    {
        [SerializeField] private ButtonUI _homeBtn;
        
        private void OnEnable()
        {
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.Shot(eSoundName.GameOver);
        }

        private void Start()
        {
            _homeBtn.onClick.AddListener(HomeListener);
        }
        
        /// Restart Level
        private void HomeListener()
        {
            Debug.LogError("VAR");
            SceneLoadManager.Instance.LoadSceneHome();
        }
    }
}