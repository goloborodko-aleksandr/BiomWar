using System;
using Characters.Interfaces;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;

namespace Characters.Mono
{
    public class BiomInput : MonoBehaviour, IInput
    {
        public event Action<IPoint> OnDirectionInput;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent<IPoint>(out IPoint point))
                    {
                        OnDirectionInput?.Invoke(point);
                    }
                }
            }
        }
    }
}