using System.Collections.Generic;
using System.Linq;
using GenerateAndCreateMap;
using GenerateAndCreateMap.Floors;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installs
{
    public class InstallMap : MonoInstaller
    {
        [SerializeField] Map _map;
        [SerializeField] GrassFloor _grassPrefab;
        [SerializeField] LavaFloor _lavaPrefab;
        [SerializeField] WaterFloor _waterPrefab;
        [SerializeField] GroundFloor _groundPrefab;
        [SerializeField] private float _scaleStep;
        [SerializeField] private FloorMap _floorMap;
        private List<Point> points => _floorMap.points;
        public override void InstallBindings()
        {
            Container
                .Bind<Map>()
                .FromInstance(_map)
                .AsSingle()
                .NonLazy();
            
            
            var prefabMap = new Dictionary<FloorType, Floor>
            {
                { FloorType.Grass, _grassPrefab },
                { FloorType.Water, _waterPrefab },
                { FloorType.Lava, _lavaPrefab },
                { FloorType.Ground,  _groundPrefab},
            };
            
            var isWalkableMap = new Dictionary<FloorType, bool>
            {
                { FloorType.Grass, true },
                { FloorType.Water, true },
                { FloorType.Lava, true },
                { FloorType.Ground,  true},
            };

            Container
                .Bind<Dictionary<FloorType, bool>>()
                .FromInstance(isWalkableMap)
                .AsSingle();

            
            Container
                .Bind<IFloorFactory>()
                .To<FloorFactory>()
                .AsSingle()
                .WithArguments(prefabMap, _scaleStep);

            Container
                .Bind<List<IPoint>>()
                .FromInstance(points.Select(i=>(IPoint)i).ToList())
                .AsSingle();
        }
    }
}
