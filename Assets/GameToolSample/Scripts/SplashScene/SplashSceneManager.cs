using System.Collections;
using GameTool.Assistants.DesignPattern;
using GameToolSample.GameDataScripts.Scripts;
using GameToolSample.Scripts.LoadScene;
using UnityEngine;

namespace GameToolSample.Scripts.SplashScene
{
    public class SplashSceneManager : SingletonMonoBehaviour<SplashSceneManager>
    {
        [SerializeField] private float maxTimeWaitLoadSceneStart = 1f;

        private void Start()
        {
            StartCoroutine(LoadSceneStart());
        }

        private IEnumerator LoadSceneStart()
        {
            float currentTimeWaitLoadSceneStart = 0f;
            while (!GameData.allDataLoaded && currentTimeWaitLoadSceneStart < maxTimeWaitLoadSceneStart)
            {
                currentTimeWaitLoadSceneStart += Time.unscaledDeltaTime;
                yield return null;
            }

            SceneLoadManager.Instance.LoadSceneStart();
        }
    }
}