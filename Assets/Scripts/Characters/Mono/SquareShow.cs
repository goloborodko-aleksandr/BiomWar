using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Classes;
using Characters.Interfaces;
using GenerateAndCreateMap.Mono;
using R3;
using UnityEngine;
using Zenject;

namespace Characters.Mono
{
    public class SquareShow: MonoBehaviour, IShowWay
    {
        [SerializeField] private List<GameObject> pool;
        private Player player;

        [Inject]
        public void Construct(Player player)
        {
            this.player = player;
            Show(this.player.EligibleFloors);
            this.player.Status.OnStateChanged
                .Subscribe(HandlerSquareShow)
                .AddTo(this);
        }

        private void HandlerSquareShow(Type type)
        {
            if (type == typeof(Idle))
            {
                Show(player.EligibleFloors);
                return;
            }
            if (type == typeof(Move))
            {
                Hide();
            }
        }
        
        public void Show(List<Floor> floors)
        {
            Hide();
            if (floors.Count > pool.Count)
            {
                int diff = floors.Count - pool.Count;
                for (int i = 0; i < diff; i++)
                {
                    pool.Add(Instantiate(pool.First(),transform));
                }
            }

            for (int i = 0; i < floors.Count; i++)
            {
                pool[i].transform.position = floors[i].transform.position;
                pool[i].SetActive(true);
            }
        }

        public void Hide()
        {
            pool.ForEach(i => i.SetActive(false));
        }
    }
}