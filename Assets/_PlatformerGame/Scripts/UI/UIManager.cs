using GameToolSample.Scripts.LoadScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _setting;
    [SerializeField] private Button _rePlay;
    [SerializeField] private GameObject objSetting;
    [SerializeField] private TextMeshProUGUI timeItemPlayer;
    [SerializeField] private TextMeshProUGUI timeItemProtect;

    private void Start()
    {
        _setting.onClick.AddListener(() => Setting());
        _rePlay.onClick.AddListener(() => Replay());
    }


    private void Setting()
    {
        objSetting.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Replay()
    {
        SceneLoadManager.Instance.LoadCurrentScene();
    }
}