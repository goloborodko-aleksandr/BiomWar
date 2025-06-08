using System.Collections.Generic;
using UnityEngine;

namespace GenerateAndCreateMap
{
    [CreateAssetMenu(menuName = "Map/FloorMap")]
    public class FloorMap : ScriptableObject
    {
        public List<Point> points;
    }
}