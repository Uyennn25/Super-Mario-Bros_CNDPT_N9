using System.Collections.Generic;
using _ProjectTemplate.Scripts.Datas;
using UnityEditor;
using UnityEngine;

namespace _ProjectTemplate.Models
{
    [CreateAssetMenu(fileName = "DataLevelResources", menuName = "_DataResources/DataLevelResources", order = 0)]
    public class DataLevelResources : ScriptableObject
    {
        [SerializeField] private List<InfoLevel> _infoLevels;

        public List<InfoLevel> InfoLevels => _infoLevels;

        public int TotalLevel => InfoLevels.Count;

#if UNITY_EDITOR

        [Header("EDITOR")] [SerializeField] private bool _updateEditor;

        private void OnValidate()
        {
            if (!_updateEditor)
            {
                return;
            }

            _infoLevels.Clear();
            for (int i = 0; i < 100; i++)
            {
                var index = i + 1;
                InfoLevel info = Resources.Load<InfoLevel>($"InfoLevels/{index}");
                if (info && !_infoLevels.Contains(info))
                {
                    _infoLevels.Add(info);
                }
            }

            _updateEditor = false;
            EditorUtility.SetDirty(this);
        }
#endif

        public static InfoLevel LoadInfoLevel(int level)
        {
            return Resources.Load<InfoLevel>("InfoLevels/" + level);
        }
    }
}