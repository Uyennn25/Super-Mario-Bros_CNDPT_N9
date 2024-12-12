using System;
using GameTool.Assistants.DesignPattern;
using UnityEngine;

namespace GameTool.TransitionFX.Scripts
{
    public class LoadingTransitionFX : SingletonMonoBehaviour<LoadingTransitionFX>
    {
        [SerializeField] private Transform _content;
        
        private Action callBackEndAnimInEvent;
        private Action callBackEndAnimOutEvent;

        public void ActiveLoading(Action _callBack = null)
        {
            _content.gameObject.SetActive(true);
            callBackEndAnimInEvent = _callBack;
            EndLoadingInAnimEvent();
        }

        public void DisableLoading(Action _callBack = null)
        {
            _content.gameObject.SetActive(false);
            callBackEndAnimOutEvent = _callBack;
        }

        void EndLoadingInAnimEvent()
        {
            callBackEndAnimInEvent?.Invoke();
            callBackEndAnimInEvent = null;
            EndLoadingOutAnimEvent();
        }

        void EndLoadingOutAnimEvent()
        {
            callBackEndAnimOutEvent?.Invoke();
            callBackEndAnimOutEvent = null;
        }
    }
}