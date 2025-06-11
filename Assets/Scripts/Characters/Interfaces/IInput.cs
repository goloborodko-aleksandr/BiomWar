using System;
using GenerateAndCreateMap.Interfaces;
using R3;

namespace Characters.Interfaces
{
    public interface IInput
    {
        public Observable<IPoint> OnDirectionInput { get; }
    }
}