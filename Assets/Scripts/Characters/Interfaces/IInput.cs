using System;
using GenerateAndCreateMap.Interfaces;
using R3;

namespace Characters.Interfaces
{
    public interface IInput
    {
        public Observable<IGridPoint> OnDirectionInput { get; }
    }
}