using System;
using System.Collections.Generic;
using UnityEngine;
using GenerateAndCreateMap.Classes;

[CreateAssetMenu(menuName = "Map/FloorMap")]
public class FloorMap : ScriptableObject
{
    public List<Point> points;
}