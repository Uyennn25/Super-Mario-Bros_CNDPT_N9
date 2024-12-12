using _ProjectTemplate.Models;
using UnityEngine;

namespace _ProjectTemplate.Scripts
{
    public static class DataResources
    {
        public static DataLevelResources GetDataLevelResources()
        {
            return Resources.Load<DataLevelResources>("DataLevelResources");
        }
    }
}