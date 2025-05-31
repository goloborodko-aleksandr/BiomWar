using System;
using Characters.Interfaces;
using GenerateAndCreateMap.Classes;
using GenerateAndCreateMap.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Characters.Mono
{
    public class ButtonInput: MonoBehaviour , IInput
    {
        [SerializeField] private Button up;
        [SerializeField] private Button down;
        [SerializeField] private Button left;
        [SerializeField] private Button right;
        
        public event Action<IPoint> OnDirectionInput;

        private void Awake()
        {
            up.onClick.AddListener(() => OnDirectionInput?.Invoke(new Point(0, 0, 1)));
            down.onClick.AddListener(() => OnDirectionInput?.Invoke(new Point(0, 0, -1)));
            left.onClick.AddListener(() => OnDirectionInput?.Invoke(new Point(-1, 0, 0)));
            right.onClick.AddListener(() => OnDirectionInput?.Invoke(new Point(1, 0, 0)));
        }
    }
}