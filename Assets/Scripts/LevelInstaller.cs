
using System;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] Helper helper; 
    [SerializeField] GridGenerator grid;
    [SerializeField] NumberSpawner numberSpawner;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] BlocksMover blocksMover;
    public override void InstallBindings()
    {
        BindHelper();
        BindGenerator();
        BindNumberSpawner();
        BindPlayerInput();
        BindBlocksMover();
    }

    private void BindBlocksMover()
    {
        Container.Bind<BlocksMover>()
            .FromInstance(blocksMover)
            .AsSingle();
    }

    private void BindPlayerInput()
    {
        Container.Bind<PlayerInput>()
            .FromInstance(playerInput)
            .AsSingle()
            .NonLazy();
    }

    private void BindNumberSpawner()
    {
        Container.Bind<NumberSpawner>()
            .FromInstance(numberSpawner)
            .AsSingle();
    }

    private void BindGenerator()
    {
        Container.Bind<GridGenerator>()
                .FromInstance(grid)
                .AsSingle()
                .NonLazy();
    }

    private void BindHelper()
    {
        Container.Bind<Helper>()
                .FromInstance(helper)
                .AsSingle();
    }
}
