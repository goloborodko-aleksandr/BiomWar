using Characters.Classes;
using Characters.Interfaces;
using Characters.Mono;
using Tools;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installs
{
    public class InstallCharacter: MonoInstaller
    {
        [SerializeField] private Player player;
        [SerializeField] private MapCenter mapCenter;
        [SerializeField] private PlayerCamera playerCamera;
        [SerializeField] private ArrowsInput arrowsInput;
        [SerializeField] private SquareShow squareShow;
        public override void InstallBindings()
        {

            Container
                .Bind<IShowWay>()
                .FromComponentInNewPrefab(squareShow)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<MapCenter>()
                .FromInstance(mapCenter)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<IFollover>()
                .FromInstance(playerCamera)
                .AsSingle();
           
            Container
                .BindInterfacesAndSelfTo<TimerService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CharacterMoveManager>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<IPlayer>()
                .To<Player>()
                .FromComponentInNewPrefab(player)
                .AsSingle();
            
            Container
                .Bind<IInput>()
                .To<ArrowsInput>()
                .FromComponentInNewPrefab(arrowsInput)
                .AsSingle();
        }
    }
}