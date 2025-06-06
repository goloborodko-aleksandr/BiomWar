using System.Collections.Generic;
using GenerateAndCreateMap.Interfaces;
using GenerateAndCreateMap.Mono;
using UnityEngine;
using Zenject;

namespace GenerateAndCreateMap.Classes
{
    public class FloorFactory: IFloorFactory
    {
        private  DiContainer _container;
        private  Dictionary<FloorType, Floor> _prefabMap;
        private  float _scaleStep;

        [Inject]
        public FloorFactory(DiContainer container, Dictionary<FloorType, Floor> prefabMap, float scaleStep)
        {
            _container = container;
            _prefabMap = prefabMap;
            _scaleStep = scaleStep;
        }
        
        public Floor CreateFloor(IPoint point, Transform transform)
        {
            return _container.InstantiatePrefabForComponent<Floor>(_prefabMap[point.GetFloorType()],point.GetPoint() * _scaleStep, Quaternion.identity, transform);
        }
    }
}