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
        private List<Floor> floorsMap = new();

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
                floorsMap.Add(floor);
            }
            Debug.Log($"Map created {floorsMap.Count}");
        }

        public List<Floor> GetFloorsMap() => floorsMap;
    }
}