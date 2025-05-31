using GenerateAndCreateMap.Mono;
using UnityEngine;

namespace Characters.Mono
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        private float experienceValue;
        private float experienceMax;
        private float experienceIcost;
        [SerializeField] private int characterSpeed;
        [SerializeField] private int characterLevel;
        [SerializeField] private int characterLucky;
        public int CharacterSpeed => characterSpeed;
        public int CharacterLevel => characterLevel;
        public int CharacterLucky => characterLucky;
        public Floor Floor{get; set;}
    }
}