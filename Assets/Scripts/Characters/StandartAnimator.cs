using System;
using Characters.CharacterPlayer;
using Characters.CharacterPlayer.States;
using Characters.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;


namespace Characters
{
    public class StandartAnimator: IAnimator
    {
        private Animator _animator;


        public void Init(BaseCharacter character)
        {
            if (character.TryGetComponent(out _animator)) return;
            Debug.LogWarning($"Character Animator could not be found {character.GetType().Name}");
        }

        public  void Idle()
        {
            _animator.SetTrigger("Idle");
        }

        public  void Run()
        {
            
        }

        public  void Jump()
        {
           _animator.SetTrigger("Jump");
        }

        public  void Attack()
        {
            
        }
    }
}