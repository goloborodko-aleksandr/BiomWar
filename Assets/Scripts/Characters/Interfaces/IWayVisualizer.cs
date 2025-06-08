using System.Collections.Generic;
using GenerateAndCreateMap.Floors;

namespace Characters.Interfaces
{
    public interface IWayVisualizer
    {
        public void Show(List<Floor> floors);
        public void Hide();
    }
}