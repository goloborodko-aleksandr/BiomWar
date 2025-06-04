
using System;
using Characters.Interfaces;
using GenerateAndCreateMap.Interfaces;
using R3;
using Unity.VisualScripting;
using UnityEngine;
using IInitializable = Zenject.IInitializable;

namespace Characters.Mono
{
    public class BiomInput : IInput, IInitializable, IDisposable
    {
        private Camera cameraInput;
        private readonly Subject<IPoint> onDirectionInput = new Subject<IPoint>();
        private readonly CompositeDisposable disposable = new CompositeDisposable();
        public Observable<IPoint> OnDirectionInput => onDirectionInput;

        public void Initialize()
        {
            Debug.Log("Initializing BiomInput");
            cameraInput = Camera.main;
            Observable
                .EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Select(_ => cameraInput.ScreenPointToRay(Input.mousePosition))
                .Select(ray =>
                {
                    if (Physics.Raycast(ray, out RaycastHit hit))
                        if (hit.collider.TryGetComponent<IPoint>(out IPoint point))
                        {
                            return point;
                        }
                    return null;
                })
                .Where(point => point != null)
                .Subscribe(point=> onDirectionInput.OnNext(point))
                .AddTo(disposable);
        }

        public void Dispose()
        {
            disposable?.Dispose();
        }
    }
}