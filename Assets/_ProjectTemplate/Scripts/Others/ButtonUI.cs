using System;
using DG.Tweening;
using GameTool.Audio.Scripts;
using GameToolSample.Audio;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _ProjectTemplate.Scripts.Others
{
    public class ButtonUI : Button
    {
#if UNITY_EDITOR
        [Header("BUTTON ELEMENT")] public bool _getImage = true;
        public bool _getText = true;
#endif

        [Space] [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _txtButton;

        [Header("BUTTON EFFECT")] [SerializeField]
        bool useAudio = true;

        [SerializeField] bool useScaleEffect = true;

        private Vector3 startScale = Vector3.one;
        private Vector3 endScale = Vector3.one * 0.95f;

        public Image Image => _image;

        public TextMeshProUGUI TxtButton => _txtButton;


        public Action _pointerDownEvent;

        #region API

        public void SetStringText(string text)
        {
            if (TxtButton)
            {
                TxtButton.text = text;
            }
        }

        public void SetTextColor(Color color)
        {
            if (TxtButton)
            {
                TxtButton.color = color;
            }
        }

        public void SetImageSprite(Sprite _sprite)
        {
            if (Image)
            {
                Image.sprite = _sprite;
            }
        }

        public void AddPointerDownEvent(Action action = null)
        {
            _pointerDownEvent += action;
        }

        #endregion

        #region Init && Editor

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (Application.isPlaying) return;

            transition = Transition.None;

            if (_getImage)
            {
                if (!_image)
                {
                    _image = GetComponent<Image>();
                }

                _getImage = false;
            }


            if (_getText)
            {
                if (!_txtButton)
                {
                    _txtButton = GetComponentInChildren<TextMeshProUGUI>();
                }

                _getText = false;
            }

            EditorUtility.SetDirty(this);
        }
#endif

        #endregion

        #region Effect

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _pointerDownEvent?.Invoke();

            if (useAudio)
            {
                if (AudioManager.IsInstanceValid())
                {
                    AudioManager.Instance.Shot(eSoundName.ButtonClick);
                }
            }

            if (useScaleEffect)
            {
                transform.DOScale(endScale, 0.05f).SetEase(Ease.Linear).SetUpdate(true);
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (useScaleEffect)
            {
                transform.DOScale(startScale, 0.05f).SetEase(Ease.Linear).SetUpdate(true);
            }
        }

        #endregion
    }
}