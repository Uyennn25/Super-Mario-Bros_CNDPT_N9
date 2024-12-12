using System.Collections.Generic;
using _ProjectTemplate.Scripts.Managers;
using _ProjectTemplate.Scripts.Others;
using GameTool.UI.Scripts.CanvasPopup;
using GameToolSample.Scripts.LoadScene;
using GameToolSample.UIManager;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectTemplate.Scripts.UI
{
    public class MenuGameplay : SingletonUI<MenuGameplay>
    {
        [SerializeField] private ButtonUI _settingBtn;
        [SerializeField] private ButtonUI _homeBtn;

        [SerializeField] private List<Image> _listLivesImage;

        [SerializeField] private Color _normal;
        [SerializeField] private Color _gray;
        

        private void Start()
        {
            _settingBtn.onClick.AddListener(SettingListener);
            _homeBtn.onClick.AddListener(HomeListener);
        }

        private void OnEnable()
        {
            var playerHealthLives = GameController.Instance.PlayerController.PlayerHealth.Lives;
            UpdateLives(playerHealthLives);
        }

        public void UpdateLives(int lives)
        {
            for (int i = 0; i < _listLivesImage.Count; i++)
            {
                var item = _listLivesImage[i];
                bool isOn = i < lives;
                item.color = isOn? _normal: _gray;
            }
        }

        private void DisableAllObj()
        {
            for (int i = 0; i < _listLivesImage.Count; i++)
            {
                var item = _listLivesImage[i];
                item.gameObject.SetActive(false);
            }
        }

        private void SettingListener()
        {
            CanvasManager.Instance.Push(eUIName.SettingPopup);
        }

        /// Restart Level
        private void HomeListener()
        {
            SceneLoadManager.Instance.LoadSceneHome();
        }
    }
}