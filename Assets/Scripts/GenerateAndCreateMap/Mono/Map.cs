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
        private IFloorFactory _factory;
        private List<IPoint> _points;
        private List<Floor> _floorsMap = new();

        [Inject]
        void Construct(IFloorFactory factory, List<IPoint> points)
        {
            Debug.Log($"Constructing Map");
            _factory = factory;
            _points = points;
            CreateMap();
        }

        void CreateMap()
        {
            foreach (var point in _points)
            {
                var floor = _factory.CreateFloor(point, transform);
                floor.Initialize(point);
                _floorsMap.Add(floor);
            }
            Debug.Log($"Map created {_floorsMap.Count}");
        }

        public List<Floor> GetFloorsMap() => _floorsMap;
    }
}