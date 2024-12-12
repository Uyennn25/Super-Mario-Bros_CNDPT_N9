using TMPro;
using UnityEngine;

namespace GameToolSample.Scripts.Toast
{
    public class ToastDisplayItemController : MonoBehaviour
    {
        [Header("COMPONENT")]
        [SerializeField] Animator animator;
        [SerializeField] RectTransform rectTransform;
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] TextMeshProUGUI toastText;

        public void ShowToast(string[] mess)
        {
            SetToastText(mess);

            animator.SetTrigger("open");
        }

        public void SetToastText(string[] mess)
        {
            if(mess.Length > 0)
            {
                if (mess.Length == 1)
                {
                    toastText.text = mess[0];
                }
                else
                {
                    toastText.text = mess[0];
                }
            }
            else
            {
                toastText.text = "";
            }
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
