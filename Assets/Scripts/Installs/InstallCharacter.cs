using Characters.Classes;
using Characters.Interfaces;
using Characters.Mono;
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
        [FormerlySerializedAs("_squareShow")] [SerializeField] private Square square;
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
                .FromComponentInNewPrefab(square)
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
                .AsSingle();

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