using _ProjectTemplate.Scripts.Datas;
using _ProjectTemplate.Scripts.Others;
using GameToolSample.GameDataScripts.Scripts;
using GameToolSample.Scripts.LoadScene;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectTemplate.Scripts.UI
{
    public class SelectLevelItem : MonoBehaviour
    {
        [SerializeField] private ButtonUI _buttonUI;
        [SerializeField] private Image _image;
        [SerializeField] private Image _lockImg;

        private InfoLevel _info;

        public void Init(InfoLevel info)
        {
            _info = info;
            _image.sprite = _info.LevelImage;
            bool isLocked = info.Level > GameData.Instance.LevelUnlocked;
            _lockImg.gameObject.SetActive(isLocked);
            _buttonUI.interactable = !isLocked;
            _buttonUI.onClick.AddListener(ButtonClickEvent);
        }

        private void ButtonClickEvent()
        {
            Debug.LogError("Load Scene Level: "+ _info.Level);
            SceneLoadManager.Instance.LoadSceneLevel(_info.Level);
        }
    }
}