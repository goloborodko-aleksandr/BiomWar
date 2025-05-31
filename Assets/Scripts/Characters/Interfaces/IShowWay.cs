using System.Collections.Generic;
using GenerateAndCreateMap.Mono;

namespace Characters.Interfaces
{
    public interface IShowWay
    {
        public void Show(List<Floor> floors);
        public void Hide();
    }
}