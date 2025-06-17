using System.Collections.Generic;
using GenerateAndCreateMap.Floors;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;
using Zenject;

namespace GenerateAndCreateMap
{
    public class Map : MonoBehaviour
    {
        private IFloorFactory _factory;
        private List<IGridPoint> _points;
        private List<Floor> _floorsMap = new();
        private Dictionary<FloorType, Color> _floorColorMap = new();

        [Inject]
       public void Construct(IFloorFactory factory, List<IGridPoint> points, Dictionary<FloorType, Color> floorColorMap)
        {
            Debug.Log("Constructing Map");
            _factory = factory;
            _points = points;
            _floorColorMap = floorColorMap;
            CreateMap();
        }

        private void CreateMap()
        {
            var block = new MaterialPropertyBlock();

            foreach (var point in _points)
            {
                var floor = _factory.CreateFloor(point, transform);
                floor.Initialize(point);
                _floorsMap.Add(floor);

                if (floor.TryGetComponent(out MeshRenderer renderer))
                {
                    block.SetColor("_BaseColor", _floorColorMap[floor.GetFloorType()]);
                    renderer.SetPropertyBlock(block);
                }
            }

            Debug.Log($"Map created {_floorsMap.Count}");
        }

        public List<Floor> GetFloorsMap() => _floorsMap;
    }
}