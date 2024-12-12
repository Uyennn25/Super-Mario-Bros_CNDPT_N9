using System;
using _ProjectTemplate.Scripts.Managers;
using GameToolSample.Scripts.Layers_Tags;
using UnityEngine;

namespace _ProjectTemplate.Scripts.Others
{
    public class Princess : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(TagName.Player))
            {
                GameController.Instance.Victory();
            }
        }
    }
}