using System;
using Characters.Interfaces;
using GenerateAndCreateMap.Interfaces;
using R3;
using UnityEngine;

namespace Characters.Mono
{
    public class BiomInput : MonoBehaviour, IInput
    {
        public Subject<IPoint> OnDirectionInput { get; } = new Subject<IPoint>();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent<IPoint>(out IPoint point))
                    {
                        OnDirectionInput.OnNext(point);
                    }
                }
            }
        }
    }
}