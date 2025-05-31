using System;
using Characters.Mono;
using GenerateAndCreateMap.Interfaces;
using GenerateAndCreateMap.Mono;

namespace Characters.Interfaces
{
    public interface IInput
    {
        public event Action<IPoint> OnDirectionInput;
    }
}