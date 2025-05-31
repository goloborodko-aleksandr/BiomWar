using System.Collections.Generic;
using GenerateAndCreateMap.Interfaces;
using GenerateAndCreateMap.Mono;
using UnityEngine;
using Zenject;

namespace GenerateAndCreateMap.Classes
{
    public class FloorFactory: IFloorFactory
    {
        private  DiContainer container;
        private  Dictionary<FloorType, Floor> prefabMap;
        private  float scaleStep;

        [Inject]
        public FloorFactory(DiContainer container, Dictionary<FloorType, Floor> prefabMap, float scaleStep)
        {
            this.container = container;
            this.prefabMap = prefabMap;
            this.scaleStep = scaleStep;
        }
        
        public Floor CreateFloor(IPoint point, Transform transform)
        {
            return container.InstantiatePrefabForComponent<Floor>(prefabMap[point.GetFloorType()],point.GetPoint() * scaleStep, Quaternion.identity, transform);
        }
    }
}