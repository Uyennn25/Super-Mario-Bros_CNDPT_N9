using System;
using GameTool.Assistants.DesignPattern;
using GameToolSample.Scripts.UI.ResourcesItems;
using UnityEngine;

namespace GameToolSample.GameConfigScripts
{
    public class GameConfig : SingletonMonoBehaviour<GameConfig>
    {
        [SerializeField] private ItemResourceData _itemResourceData;

        [Header("CONFIG")] [SerializeField] private int _totalLevel = 10;

        public ItemResourceData ItemResourceData => _itemResourceData;
        public int TotalLevel => _totalLevel;
    }
}