using _ProjectTemplate.Scripts.Others;
using GameTool.UI.Scripts.CanvasPopup;
using UnityEngine;

namespace _ProjectTemplate.Scripts.UI
{
    public class TutorialPopup : SingletonUI<TutorialPopup>
    {
        [SerializeField] private ButtonUI _backBtn;

        private void Start()
        {
            _backBtn.onClick.AddListener(BackListener);
        }

        private void BackListener()
        {
            Pop();
        }
    }
}