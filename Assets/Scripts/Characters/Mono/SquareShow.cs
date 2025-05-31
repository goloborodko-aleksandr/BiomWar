using System.Collections.Generic;
using System.Linq;
using Characters.Interfaces;
using GenerateAndCreateMap.Mono;
using UnityEngine;

namespace Characters.Mono
{
    public class SquareShow: MonoBehaviour, IShowWay
    {
        [SerializeField] private List<GameObject> pool;

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