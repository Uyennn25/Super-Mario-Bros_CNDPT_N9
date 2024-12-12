using GameTool.Assistants.DesignPattern;
using UnityEngine;

namespace GameToolSample.API.Scripts
{
    public class API : SingletonMonoBehaviour<API>
    {
        private void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}