using System.Collections.Generic;
using System.Linq;
using GenerateAndCreateMap.Classes;
using GenerateAndCreateMap.Interfaces;
using GenerateAndCreateMap.Mono;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installs
{
    public class InstallMap : MonoInstaller
    {
        [SerializeField] Map map;
        [SerializeField] GrassFloor grassPrefab;
        [SerializeField] LavaFloor lavaPrefab;
        [SerializeField] WaterFloor waterPrefab;
        [SerializeField] StoneFloor stonePrefab;
        [SerializeField] GroundFloor groundPrefab;
        [SerializeField] private float scaleStep;
        [SerializeField] private FloorMap floorMap;
        private List<Point> points => floorMap.points;
        public override void InstallBindings()
        {
            Container
                .Bind<Map>()
                .FromInstance(map)
                .AsSingle()
                .NonLazy();
            
            
            var prefabMap = new Dictionary<FloorType, Floor>
            {
                { FloorType.Grass, grassPrefab },
                { FloorType.Water, waterPrefab },
                { FloorType.Lava, lavaPrefab },
                { FloorType.Stone, stonePrefab },
                { FloorType.Ground,  groundPrefab},
            };
            
            var isWalkableMap = new Dictionary<FloorType, bool>
            {
                { FloorType.Grass, true },
                { FloorType.Water, true },
                { FloorType.Lava, true },
                { FloorType.Stone, true },
                { FloorType.Ground,  true},
            };

            Container
                .Bind<Dictionary<FloorType, bool>>()
                .FromInstance(isWalkableMap)
                .AsCached();

            
            Container
                .Bind<IFloorFactory>()
                .To<FloorFactory>()
                .AsSingle()
                .WithArguments(prefabMap, scaleStep);

            Container
                .Bind<List<IPoint>>()
                .FromInstance(points.Select(i=>(IPoint)i).ToList())
                .AsSingle();
        }
    }
}
