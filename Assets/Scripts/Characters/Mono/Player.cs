using Characters.Interfaces;
using GenerateAndCreateMap.Mono;
using UnityEngine;
using Zenject;

namespace Characters.Mono
{
    public class Player: BaseCharacter,IPlayer
    {
        public Transform MainPlayerTransform => transform;
        
    }
}