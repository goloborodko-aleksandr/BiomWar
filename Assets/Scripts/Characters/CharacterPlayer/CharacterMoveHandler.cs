using System;
using System.Linq;
using Characters.Interfaces;
using GenerateAndCreateMap.Interfaces;
using R3;
using UnityEngine;
using Zenject;

namespace Characters.CharacterPlayer
{
    public class CharacterMoveHandler : IDisposable
    {
        private Player _player;
        private IInput _input;
        private EligiblePath _eligiblePath;


        private CompositeDisposable compositeDisposable = new CompositeDisposable();
        
        [Inject]
        CharacterMoveHandler(Player player, IInput input, EligiblePath eligiblePath)
        {
            Debug.Log($"Creating CharacterMoveManager");
            _player = player;
            _eligiblePath = eligiblePath;
            _input = input;
            _input
                .OnDirectionInput
                .Subscribe(Path)
                .AddTo(compositeDisposable);
        }

        private void Path(IGridPoint gridPoint)
        {
            var eligibleFloors = _eligiblePath.GetVariantsPath(_player, _player.CurrentFloor);
            var floor = eligibleFloors.FirstOrDefault(i => i.GetPoint() == gridPoint.GetPoint());
            if (eligibleFloors.Contains(floor))
            {
                var newEligibleFloors = _eligiblePath.GetVariantsPath(_player, floor);
                _player.Move(floor, newEligibleFloors);    
            }
        }
        
        public void Dispose()
        {
            compositeDisposable.Dispose();
        }
    }
}