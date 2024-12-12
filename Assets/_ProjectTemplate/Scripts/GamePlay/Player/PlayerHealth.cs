using _ProjectTemplate.Scripts.Managers;
using DatdevUlts.Ults;
using DG.Tweening;
using GameTool.Assistants.DesignPattern;
using GameToolSample.Scripts.Enum;
using UnityEngine;

namespace _ProjectTemplate.Scripts.GamePlay.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        protected int lives;
        protected bool isDie;

        public int Lives
        {
            get => lives;
            private set => lives = value;
        }

        public bool IsDie
        {
            get => isDie;
            private set => isDie = value;
        }

        public void SetLives(int countLive)
        {
            Lives = countLive;
        }

        public void Init()
        {
            IsDie = false;
        }

        public void Revive()
        {
            IsDie = false;
            GameController.Instance.Revive();
        }

        public void Die()
        {
            IsDie = true;
            Debug.LogError("Die");
            if (Lives > 1)
            {
                Lives--;
                DOVirtual.DelayedCall(1f, Revive);
            }
            else
            {
                GameController.Instance.Lose();
            }
        }

        public void AddLife(int value = 1)
        {
            Lives += value;
        }
    }
}