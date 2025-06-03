
using Characters.Interfaces;
using GenerateAndCreateMap.Interfaces;
using R3;
using UnityEngine;

namespace Characters.Mono
{
    public class BiomInput : MonoBehaviour, IInput
    {
        private readonly Subject<IPoint> onDirectionInput = new Subject<IPoint>();
        public Observable<IPoint> OnDirectionInput => onDirectionInput;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent<IPoint>(out IPoint point))
                    {
                        onDirectionInput.OnNext(point);
                    }
                }
            }
        }
    }
}