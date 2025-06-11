using System.Collections.Generic;
using Characters.Interfaces;
using GenerateAndCreateMap.Floors;
using UnityEngine;
using Zenject;

namespace Characters
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField] private float _coolDown;
        [SerializeField] private int _characterLevel;
        [SerializeField] private int _characterSpeed;
        [SerializeField] private int _characterLucky;
        private protected float coolDown;
        private protected int characterLevel;
        private protected int characterSpeed;
        private protected int characterLucky;
        
        private protected Fsm.Fsm status;
        private protected Fsm.Fsm debuff;
        private protected List<Floor> eligibleFloors;
        private protected Floor targetFloor;
        private protected Progress progress;
        public Progress Progress => progress;
        private protected IAnimator characterAnimator;
        public IAnimator CharacterAnimator => characterAnimator;
        
        [Inject]
        public void Construct(Progress progress, IAnimator characterAnimator)
        {
            this.characterAnimator = characterAnimator;
            this.characterAnimator.Init(this);
            this.progress = progress;
            coolDown = _coolDown;
            characterLevel = _characterLevel;
            characterSpeed = _characterSpeed;
            characterLucky = _characterLucky;
        }
        
        public  Floor CurrentFloor { get; set; }
        public  List<Floor> EligibleFloors => eligibleFloors;
        public  Floor TargetFloor => targetFloor;
        public int CharacterLevel => characterLevel;
        public int CharacterSpeed => characterSpeed;
        public int CharacterLucky => characterLucky;
        public float CoolDown => coolDown;
        public Fsm.Fsm Status => status;
        public Fsm.Fsm Debuff => debuff;
        
    }
}