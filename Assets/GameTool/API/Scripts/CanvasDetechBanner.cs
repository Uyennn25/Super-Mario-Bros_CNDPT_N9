using GameTool.Assistants.DesignPattern;
using GameToolSample.Scripts.Enum;
#if !Minify
using GameToolSample.GameDataScripts.Scripts;
using UnityEditor;
using UnityEngine;
#endif

namespace GameTool.API.Scripts
{
    public class CanvasDetechBanner : MonoBehaviour
    {
#if !Minify
        [SerializeField] RectTransform rectTransform;
        [SerializeField] float top, bottom;
        [SerializeField] Vector2 anchorPosBanner;
        [SerializeField] Vector2 anchorPosNormal;
        [SerializeField] bool useForAnchor;
        [SerializeField] bool keepPosition;
        public static bool isShowBanner;

        private void OnEnable()
        {
            CheckBanner(isShowBanner);
            this.RegisterListener(EventID.BannerShowing, BannerShowingEventRegisterListener);
            if (keepPosition)
            {
                keepPosition = !GameData.Instance.RemoveAds;
            }
        }

        private void OnDisable()
        {
            this.RemoveListener(EventID.BannerShowing, BannerShowingEventRegisterListener);
        }

        private void BannerShowingEventRegisterListener(Component component, object[] obj = null)
        {
            CheckBanner(obj != null && (bool)obj[0]);
        }

        void CheckBanner(bool isShow)
        {
            if (isShow)
            {
                isShowBanner = true;
                if (useForAnchor)
                {
                    rectTransform.anchoredPosition = anchorPosBanner;
                }
                else
                {
                    rectTransform.offsetMin = new Vector2(0, bottom);
                    rectTransform.offsetMax = new Vector2(0, -top);
                    if (keepPosition)
                        CancelInvoke(nameof(BackToNormal));
                }
            }
            else
            {
                isShowBanner = false;
                if (useForAnchor)
                {
                    rectTransform.anchoredPosition = anchorPosNormal;
                }
                else
                {
                    if (keepPosition)
                        Invoke(nameof(BackToNormal), .5f);
                    else
                    {
                        BackToNormal();
                    }
                }
            }
        }

        void BackToNormal()
        {
            if (GameData.Instance.RemoveAds)
            {
                rectTransform.offsetMin = new Vector2(0, 0);
                rectTransform.offsetMax = new Vector2(0, 0);
            }
        }

#if UNITY_EDITOR
        // public KeyCode KeySafeArea = KeyCode.A;

        // void OnGUI()
        // {
        //     // EditorApplication.update += OnUpDateEditor;
        // }
        [SerializeField] bool _show;

        public void OnShowBanner()
        {
            if (!EditorApplication.isPlaying)
            {
                _show = !_show;
                CheckBannerEditor(_show);
            }
        }

        void CheckBannerEditor(bool isShow)
        {
            if (isShow)
            {
                // isShowBanner = true;
                if (useForAnchor)
                {
                    rectTransform.anchoredPosition = anchorPosBanner;
                }
                else
                {
                    rectTransform.offsetMin = new Vector2(0, bottom);
                    rectTransform.offsetMax = new Vector2(0, -top);
                    if (keepPosition)
                        CancelInvoke("BackToNormal");
                }
            }
            else
            {
                // isShowBanner = false;
                if (useForAnchor)
                {
                    rectTransform.anchoredPosition = anchorPosNormal;
                }
                else
                {
                    if (keepPosition)
                        Invoke("BackToNormal", .5f);
                    else
                    {
                        rectTransform.offsetMin = new Vector2(0, 0);
                        rectTransform.offsetMax = new Vector2(0, 0);
                    }
                }
            }
        }
#endif
#endif
    }
}

/* [CustomEditor(typeof(CanvasDetechBanner))]
public class CanvasDetechBannerEditor : Editor
{
    void OnSceneGUI()
    {
        CanvasDetechBanner script = (CanvasDetechBanner)target;
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.KeyDown:
                {
                    if (Event.current.keyCode == (KeyCode.A))
                    {
                        script.OnShowBanner();
                        e.Use();
                    }
                    break;
                }
        }
        // Debug.Log("Zoday");
    }
} */