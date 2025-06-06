using System;
using System.Collections.Generic;
using Characters.Classes;
using Characters.Interfaces;
using GenerateAndCreateMap.Mono;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Characters.Mono
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
        
        private protected Fsm status;
        private protected Fsm debuff;
        private protected List<Floor> eligibleFloors;
        private protected Floor targetFloor;
        private protected Progress progress;
        public Progress Progress => progress;
        
        [Inject]
        public void Construct(Progress progress)
        {
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
        public Fsm Status => status;
        public Fsm Debuff => debuff;
        
    }
}