using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace
namespace GameTool
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class LoadSceneManager : SingletonMonoBehaviour<LoadSceneManager>
    {
        public const string nameSceneSpl = "SPL";
        public const string nameSceneHome = "HomeScene";
        public const string nameSceneGame = "GameScene";
        public const string nameSceneLobby = "LobbyScene";
        public const string nameSceneLevel = "Level ";

        protected override void Awake()
        {
            base.Awake();
            this.PostEvent(eEventType.SceneLoaded);
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadAsyncScene(sceneName));
        }

        public void LoadSceneSpl()
        {
            StartCoroutine(LoadAsyncScene(nameSceneSpl));
        }

        public void LoadSceneHome()
        {
            StartCoroutine(LoadAsyncScene(nameSceneHome));
        }

        public void LoadSceneGame()
        {
            StartCoroutine(LoadAsyncScene(nameSceneGame));
        }

        public void LoadSceneLobby()
        {
            StartCoroutine(LoadAsyncScene(nameSceneLobby));
        }


        public void LoadSceneLevel(int level)
        {
            GameData.Instance.Data.CurrentLevel = level;
            StartCoroutine(LoadAsyncScene(nameSceneLevel + level));
        }

        public void ReLoadCurrentLevel()
        {
            var level = GameData.Instance.Data.CurrentLevel;
            StartCoroutine(LoadAsyncScene(nameSceneLevel + level));
        }

        public void LoadCurrentScene()
        {
            StartCoroutine(LoadAsyncScene(SceneManager.GetActiveScene().name));
        }

        protected IEnumerator LoadAsyncScene(string nameScene)
        {
            yield return TransistionFX.Instance.OnLoadScene();
            PoolingManager.Instance.DisableAllObject();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nameScene);
            this.PostEvent(eEventType.SceneLoaded);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            TransistionFX.Instance.EndLoadScene();
        }
    }
}