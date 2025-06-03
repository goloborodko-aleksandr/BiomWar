using System;
using Characters.Classes;
using Characters.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Characters.Mono
{
    public class PlayerProgressBar: MonoBehaviour, IProgressBar
    {
        [SerializeField] private Image progressBar;
        [SerializeField] private Vector3 offset;
        private Player player;
        private Camera mainCamera;


        [Inject]
        public void Construct(Player player)
        {
            this.player = player;
            mainCamera = Camera.main;
            this.player.Status.OnStateChanged
                .Subscribe(HandlerBar)
                .AddTo(this);
        }

        private float Progress
        {
            get => progressBar.fillAmount;
            set
            {
                progressBar.fillAmount = value;
                transform.position = mainCamera.WorldToScreenPoint(player.transform.position + offset);
            }
        }

        private void HandlerBar(Type type)
        {
            if (type == typeof(Idle))
            {
                Show();
                return;
            }
            if (type == typeof(Move))
            {
                Hide();
            }
        }

        private void Update()
        {
            Progress = player.ProgressValue;
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