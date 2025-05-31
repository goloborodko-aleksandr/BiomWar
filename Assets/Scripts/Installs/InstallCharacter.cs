using Characters.Classes;
using Characters.Interfaces;
using Characters.Mono;
using Tools;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Zenject;

namespace Installs
{
    public class InstallCharacter: MonoInstaller
    {
        [SerializeField] private Player player;
        [SerializeField] private MapCenter mapCenter;
        [SerializeField] private PlayerCamera playerCamera;
        [SerializeField] private BiomInput biomInput;
        [SerializeField] private SquareShow squareShow;
        [SerializeField] private PlayerProgressBar playerProgressBar;
        [SerializeField] private Canvas PlayGameCanvas;
        public override void InstallBindings()
        {

            Container
                .Bind<Canvas>()
                .FromInstance(PlayGameCanvas)
                .AsSingle();
            
            Container
                .Bind<IShowWay>()
                .FromComponentInNewPrefab(squareShow)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<MapCenter>()
                .FromInstance(mapCenter)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<PlayerCamera>()
                .FromInstance(playerCamera)
                .AsSingle();
           
            Container
                .BindInterfacesAndSelfTo<TimerService>()
                .FromNew()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CharacterManager>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<Player>()
                .FromComponentInNewPrefab(player)
                .AsSingle();
            
            Container
                .Bind<IInput>()
                .To<BiomInput>()
                .FromComponentInNewPrefab(biomInput)
                .AsSingle();

            Container
                .Bind<IProgressBar>()
                .To<PlayerProgressBar>()
                .FromComponentInNewPrefab(playerProgressBar)
                .UnderTransform(PlayGameCanvas.transform)
                .AsSingle()
                .NonLazy();

        }
    }
}