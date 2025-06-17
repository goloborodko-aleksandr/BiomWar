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
        private List<GridPoint> points => _floorMap.points;
        public override void InstallBindings()
        {
            Container
                .Bind<Map>()
                .FromInstance(_map)
                .AsSingle()
                .NonLazy();
            
            var floorColorMap = new Dictionary<FloorType, Color>
            {
                { FloorType.Grass, new Color(0.2800419f,0.8301887f,0.2062417f) },
                { FloorType.Water, new Color(0f, 0.5311722f, 1f) },
                { FloorType.Lava, new Color(0.8313726f, 0.5027158f, 0.2078431f) },
                { FloorType.Ground,  new Color(0.4779873f, 0.3507593f, 0.2419998f)},
            };
            
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
                .Bind<Dictionary<FloorType, Color>>()
                .FromInstance(floorColorMap)
                .AsSingle();
            
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
                .Bind<List<IGridPoint>>()
                .FromInstance(points.Select(i=>(IGridPoint)i).ToList())
                .AsSingle();
        }
    }
}
