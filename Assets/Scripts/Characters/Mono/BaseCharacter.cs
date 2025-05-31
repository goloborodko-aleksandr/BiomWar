using System;
using GenerateAndCreateMap.Mono;
using UnityEngine;

namespace Characters.Mono
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField] private int characterLevel;
        [SerializeField] private int characterSpeed;
        [SerializeField] private int characterLucky;

        public int CharacterLevel => characterLevel;
        public int CharacterSpeed => characterSpeed;
        public int CharacterLucky => characterLucky;

        public abstract Floor Floor{get; set;}
        
    }
}