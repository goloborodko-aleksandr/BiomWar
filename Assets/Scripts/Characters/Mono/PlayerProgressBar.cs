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
            }
        }

        private void Update()
        {
            Progress = player.ProgressMoveValue;
            transform.position = mainCamera.WorldToScreenPoint(player.transform.position + offset);
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