using System;
using Characters.Mono;
using GenerateAndCreateMap.Interfaces;
using GenerateAndCreateMap.Mono;
using R3;

namespace Characters.Interfaces
{
    public interface IInput
    {
        public Observable<IPoint> OnDirectionInput { get; }
    }
}