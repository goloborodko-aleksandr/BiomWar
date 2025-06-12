using System.Linq;
using Characters;
using Characters.CharacterPlayer;
using Characters.CharacterPlayer.Input;
using Characters.Interfaces;
using GenerateAndCreateMap;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installs
{
    public class InstallCharacter : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private MapCenter _mapCenter;
        [SerializeField] private PlayerCamera _playerCamera;
        [SerializeField] private Square _square;
        [SerializeField] private PlayerProgressBar _playerProgressBar;
        [SerializeField] private Canvas _playGameCanvas;

        public override void InstallBindings()
        {
            Container
                .Bind<Canvas>()
                .FromInstance(_playGameCanvas)
                .AsSingle();

            Container
                .Bind<IWayVisualizer>()
                .FromComponentInNewPrefab(_square)
                .AsSingle()
                .NonLazy();
            Container
                .Bind<Progress>()
                .AsCached();

            Container
                .BindInterfacesAndSelfTo<MapCenter>()
                .FromInstance(_mapCenter)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlayerCamera>()
                .FromInstance(_playerCamera)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CharacterMoveHandler>()
                .FromNew()
                .AsSingle()
                .NonLazy();
            
            
            Container
                .Bind<Player>()
                .FromComponentInNewPrefab(_player)
                .AsSingle()
                .OnInstantiated<Player>((ctx, player) =>
                {
                    var map = ctx.Container.Resolve<Map>();
                    var path = ctx.Container.Resolve<EligiblePath>();

                    var startFloor = map.GetFloorsMap().Last();
                    var variants = path.GetVariantsPath(player, startFloor);
                    player.Init(startFloor, variants);
                });

            Container
                .Bind<IAnimator>()
                .To<StandartAnimator>()
                .FromNew()
                .AsCached();

            Container
                .Bind<EligiblePath>()
                .FromNew()
                .AsCached()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<BiomInput>()
                .FromNew()
                .AsSingle()
                .NonLazy();
                

            Container
                .Bind<IProgressBar>()
                .To<PlayerProgressBar>()
                .FromComponentInNewPrefab(_playerProgressBar)
                .UnderTransform(_playGameCanvas.transform)
                .AsSingle()
                .NonLazy();
        }
    }
}