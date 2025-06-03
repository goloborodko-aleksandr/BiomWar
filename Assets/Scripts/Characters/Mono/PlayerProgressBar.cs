using System;
using Characters.Interfaces;
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
        void Construct(Player player)
        {
            this.player = player;
            mainCamera = Camera.main;
        }

        float Progress
        {
            get => progressBar.fillAmount;
            set
            {
                progressBar.fillAmount = value;
                transform.position = mainCamera.WorldToScreenPoint(player.transform.position + offset);
                if (player.ProgressMoveValue >= 1) { Hide(); return; }
                if (player.ProgressMoveValue == 0) Show();
            }
        }

        private void Update()
        {
            Progress = player.ProgressMoveValue;
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