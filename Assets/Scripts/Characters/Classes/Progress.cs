using System;
using R3;
using UnityEngine;

namespace Characters.Classes
{
    public class Progress : IDisposable
    {
        private readonly ReactiveProperty<float> progressTimeProperty = new ReactiveProperty<float>(0f);
        private ReadOnlyReactiveProperty<float> progressTimeReadOnly;
        public ReadOnlyReactiveProperty<float> ProgressTimeProperty => progressTimeReadOnly;
        private readonly CompositeDisposable disposable = new CompositeDisposable();
        private bool isRunning = false;

        public Progress()
        {
            progressTimeReadOnly = progressTimeProperty.ToReadOnlyReactiveProperty();
        }

        public void StartProgress(int speed, float coolDown)
        {
            if (isRunning)
                return;
            isRunning = true;
            progressTimeProperty.Value = 0f;
            Observable
                .EveryUpdate()
                .Select(_ => Time.deltaTime * speed)
                .Scan(0f, (total, step) => total + step)
                .Select(progress => Mathf.Clamp01(progress / coolDown))
                .TakeUntil(progress => progress  >= 1)
                .Subscribe(progress =>
                {
                    progressTimeProperty.Value = progress;
                    if(progressTimeProperty.Value >= 1) isRunning = false;
                })
                .AddTo(disposable);
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}