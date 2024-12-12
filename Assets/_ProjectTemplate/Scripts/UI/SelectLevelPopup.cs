using System.Collections.Generic;
using _ProjectTemplate.Models;
using _ProjectTemplate.Scripts.Others;
using GameTool.UI.Scripts.CanvasPopup;
using UnityEngine;

namespace _ProjectTemplate.Scripts.UI
{
    public class SelectLevelPopup : SingletonUI<SelectLevelPopup>
    {
        [SerializeField] private SelectLevelItem _itemPrefab;
        [SerializeField] private Transform _itemParent;
        [SerializeField] private ButtonUI _backBtn;

        private List<SelectLevelItem> _selectLevelItems;
        [Header("RESOURCES")] [SerializeField] private DataLevelResources _dataLevelResources;

        protected override void Awake()
        {
            base.Awake();
            if (!_dataLevelResources)
            {
                _dataLevelResources = DataResources.GetDataLevelResources();
            }
        }

        private void Start()
        {
            for (int i = 0; i < _dataLevelResources.TotalLevel; i++)
            {
                SelectLevelItem item = Instantiate(_itemPrefab, _itemParent, false);
                item.Init(_dataLevelResources.InfoLevels[i]);
            }

            _backBtn.onClick.AddListener(BackListener);
        }

        private void BackListener()
        {
            Pop();
        }
    }
}