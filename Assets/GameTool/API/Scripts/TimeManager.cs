using GameTool.Assistants.DesignPattern;
using GameToolSample.Scripts.Enum;
using UnityEngine;

namespace GameTool.API.Scripts
{
    public class TimeManager : SingletonMonoBehaviour<TimeManager>
    {
        public static bool isPause = false;
        [Header("SLOMOTION")]
        float slowdownFactor = 0.2f;
        float slowdownLength = 2f;

        float currentTimeScale = 1f;
        float startFixedDeltaTime = 0.02f;
        bool isSlomotion = false;

        void Start()
        {
            this.RegisterListener(EventID.PauseGame, PauseGameEventRegisterListener);
            this.RegisterListener(EventID.ContinueGame, ContinueGameEventRegisterListener);
            this.RegisterListener(EventID.SlowMotion, SlomotionEventRegisterListener);
            this.RegisterListener(EventID.ResetTime, ResetTimeEventRegisterListener);

            startFixedDeltaTime = Time.fixedDeltaTime;
        }

        private void Update()
        {
            if (!isPause)
            {
                if (isSlomotion)
                {
                    currentTimeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
                    currentTimeScale = Mathf.Clamp(currentTimeScale, 0f, 1f);
                    SetTimeScale(currentTimeScale);
                    if (currentTimeScale >= 1f)
                    {
                        DoNormal();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            this.RemoveListener(EventID.PauseGame, PauseGameEventRegisterListener);
            this.RemoveListener(EventID.ContinueGame, ContinueGameEventRegisterListener);
            this.RemoveListener(EventID.SlowMotion, SlomotionEventRegisterListener);
            this.RemoveListener(EventID.ResetTime, ResetTimeEventRegisterListener);
        }

        void PauseGameEventRegisterListener(Component component, object[] obj = null)
        {
            SetPause(true);
        }

        void ContinueGameEventRegisterListener(Component component, object[] obj = null)
        {
            SetPause(false);
        }

        void SlomotionEventRegisterListener(Component component, object[] obj = null)
        {
            if (obj != null && obj.Length > 0)
            {
                DoSlomotion((int)obj[0]);
            }
            else
            {
                DoSlomotion();
            }
        }

        void ResetTimeEventRegisterListener(Component component, object[] obj = null)
        {
            DoNormal();
        }

        #region Slomotion
        public void SetPause(bool value)
        {
            isPause = value;

            if (isPause)
            {
                SetTimeScale(0f);
            }
            else
            {
                SetTimeScale(1f);
            }
        }

        public void DoSlomotion()
        {
            isSlomotion = true;
            currentTimeScale = slowdownFactor;
            SetTimeScale(slowdownFactor);
            Time.fixedDeltaTime = slowdownFactor * 0.02f;
        }

        public void DoSlomotion(float slowdown)
        {
            isSlomotion = true;
            currentTimeScale = slowdown;
            SetTimeScale(slowdown);
            Time.fixedDeltaTime = slowdown * 0.02f;
        }

        public void SetTimeScale(float value)
        {
            Time.timeScale = value;
            Debug.Log("Timescale : " + Time.timeScale);
        }

        public void DoNormal()
        {
            isSlomotion = false;
            currentTimeScale = 1f;
            SetPause(false);
            SetFixedDeltaTime(startFixedDeltaTime);
        }

        public void SetFixedDeltaTime(float value)
        {
            Time.fixedDeltaTime = value;
        }
        #endregion
    }
}
