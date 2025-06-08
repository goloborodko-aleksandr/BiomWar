using System;
using R3;
using UnityEngine;

namespace Characters
{
    public class Progress : IDisposable
    {
        private readonly ReactiveProperty<float> _progressTimeProperty = new ReactiveProperty<float>(0f);
        private ReadOnlyReactiveProperty<float> _progressTimeReadOnly;
        private bool _isRunning = false;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        public ReadOnlyReactiveProperty<float> ProgressTimeProperty => _progressTimeReadOnly;
        private readonly Subject<Unit> _progressDone = new Subject<Unit>();
        public Observable<Unit> ProgressDone => _progressDone;

        public Progress()
        {
            _progressTimeReadOnly = _progressTimeProperty.ToReadOnlyReactiveProperty();
        }

        public void StartProgress(int speed, float coolDown)
        {
            if (_isRunning)
                return;
            _isRunning = true;
            _progressTimeProperty.Value = 0f;
            Observable
                .EveryUpdate()
                .Select(_ => Time.deltaTime * speed)
                .Scan(0f, (total, step) => total + step)
                .Select(progress => Mathf.Clamp01(progress / coolDown))
                .TakeUntil(progress => progress  >= 1)
                .Subscribe(progress =>
                {
                    _progressTimeProperty.Value = progress;
                    if (_progressTimeProperty.Value >= 1)
                    {
                        _isRunning = false;
                        _progressDone.OnNext(Unit.Default);
                    }
                })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}