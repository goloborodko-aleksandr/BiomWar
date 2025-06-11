using System;
using Characters.Interfaces;
using GenerateAndCreateMap.Interfaces;
using R3;
using UnityEngine;
using IInitializable = Zenject.IInitializable;

namespace Characters.CharacterPlayer.Input
{
    public class BiomInput : IInput, IInitializable, IDisposable
    {
        private Camera _cameraInput;
        private readonly Subject<IPoint> _onDirectionInput = new Subject<IPoint>();
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        public Observable<IPoint> OnDirectionInput => _onDirectionInput;

        public void Initialize()
        {
            _cameraInput = Camera.main;
            Observable
                .EveryUpdate()
                .Where(_ => UnityEngine.Input.GetMouseButtonDown(0))
                .Select(_ => _cameraInput.ScreenPointToRay(UnityEngine.Input.mousePosition))
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
                .Subscribe(point=> _onDirectionInput.OnNext(point))
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}