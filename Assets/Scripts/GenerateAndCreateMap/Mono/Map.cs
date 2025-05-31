using System;
using System.Collections.Generic;
using System.Linq;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;
using Zenject;

namespace GenerateAndCreateMap.Mono
{
    public class Map : MonoBehaviour
    {
        private IFloorFactory factory;
        private List<IPoint> points;
        private List<Floor> pointsMap = new();

        [Inject]
        void Construct(IFloorFactory factory, List<IPoint> points)
        {
            Debug.Log($"Constructing Map");
            this.factory = factory;
            this.points = points;
            CreateMap();
        }

        void CreateMap()
        {
            foreach (var point in points)
            {
                var floor = factory.CreateFloor(point, transform);
                floor.Initialize(point);
                pointsMap.Add(floor);
            }
            Debug.Log($"Map created {pointsMap.Count}");
        }

        public List<Floor> GetPointsMap() => pointsMap;
    }
}