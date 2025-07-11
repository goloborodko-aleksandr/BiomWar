﻿using System;
using System.Collections.Generic;
using System.Linq;
using Characters.CharacterPlayer.States;
using Characters.Interfaces;
using GenerateAndCreateMap.Floors;
using R3;
using UnityEngine;
using Zenject;

namespace Characters.CharacterPlayer
{
    public class Square: MonoBehaviour, IWayVisualizer
    {
        [SerializeField]private List<GameObject> _pool;
        private CharacterPlayer.Player _player;

        [Inject]
        public void Construct(CharacterPlayer.Player player)
        {
            _player = player;
            Show(_player.EligibleFloors);
            _player.Status.OnStateChanged
                .Subscribe(HandlerSquareShow)
                .AddTo(this);
            _player.Progress.ProgressDone
                .Subscribe(_=> Show(_player.EligibleFloors))
                .AddTo(this);
        }

        private void HandlerSquareShow(Type type)
        {
            if (type == typeof(Move)) Hide();
        }
        
        public void Show(List<Floor> floors)
        {
            Hide();
            if (floors.Count > _pool.Count)
            {
                int diff = floors.Count - _pool.Count;
                for (int i = 0; i < diff; i++)
                {
                    _pool.Add(Instantiate(_pool.First(),transform));
                }
            }

            for (int i = 0; i < floors.Count; i++)
            {
                _pool[i].transform.position = floors[i].transform.position;
                _pool[i].SetActive(true);
            }
        }

        public void Hide()
        {
            _pool.ForEach(i => i.SetActive(false));
        }
    }
}