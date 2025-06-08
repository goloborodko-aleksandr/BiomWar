using System.Collections;
using System.Collections.Generic;
using Characters.Interfaces;
using Characters.Mono;
using Characters.Player;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    private Transform _player;
    private Transform _mapCenter;
    
    [Inject]
    public void Construct(Player player, MapCenter mapCenter)
    {
        
        _player = player.transform;
        _mapCenter = mapCenter.transform;
        _playerCamera.m_Follow = _player;
        _playerCamera.m_LookAt = _mapCenter;
    }
}
