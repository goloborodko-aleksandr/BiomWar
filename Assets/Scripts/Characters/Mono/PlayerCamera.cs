using System.Collections;
using System.Collections.Generic;
using Characters.Interfaces;
using Characters.Mono;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    private Transform player;
    private Transform mapCenter;
    
    [Inject]
    public void Construct(Player player, MapCenter mapCenter)
    {
        
        this.player = player.transform;
        this.mapCenter = mapCenter.transform;
        playerCamera.m_Follow = this.player;
        playerCamera.m_LookAt = this.mapCenter;
    }
}
