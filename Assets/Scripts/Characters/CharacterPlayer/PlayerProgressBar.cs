using System;
using Characters.CharacterPlayer.States;
using Characters.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Characters.CharacterPlayer
{
    public class PlayerProgressBar: MonoBehaviour, IProgressBar
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private Vector3 _offset;
        private CharacterPlayer.Player _player;
        private Camera _mainCamera;


        [Inject]
        public void Construct(CharacterPlayer.Player player)
        {
            _player = player;
            _mainCamera = Camera.main;
            _player.Status.OnStateChanged
                .Subscribe(HandlerBar)
                .AddTo(this);
            _player.Progress.ProgressTimeProperty
                .Subscribe(value => Progress = value)
                .AddTo(this);
            _player.Progress.ProgressDone
                .Subscribe(_=> Hide())
                .AddTo(this);
        }

        private float Progress
        {
            get => _progressBar.fillAmount;
            set
            {
                _progressBar.fillAmount = value;
                transform.position = _mainCamera.WorldToScreenPoint(_player.transform.position + _offset);
            }
        }

        private void HandlerBar(Type type)
        {
            if (type == typeof(Idle)) Show();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}