using UnityEngine;

namespace _ProjectTemplate.Scripts.Datas
{
    [CreateAssetMenu(fileName = "InfoLevel", menuName = "_DataResources/InfoLevel", order = 0)]
    public class InfoLevel : ScriptableObject
    {
        public int Level => int.Parse(name);

        public Sprite LevelImage => Resources.Load<Sprite>($"LevelImages/{Level}");
        
    }
}