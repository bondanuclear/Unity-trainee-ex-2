
using System;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    
    public override void InstallBindings()
    {
        // Grid generator and pool of numbers are already binded with Zenject Binding
        // as well as helper
        BindNumberSpawner();
        BindPlayerInput();
        BindBlocksMover();
       
    }

   

    private void BindBlocksMover()
    {
        Container.Bind<BlocksMover>()
            .AsSingle();
    }

    private void BindPlayerInput()
    {
        Container.Bind<PlayerInput>()
            .AsSingle()
            .NonLazy();
    }

    private void BindNumberSpawner()
    {
        Container.Bind<NumberSpawner>()
            .AsSingle()
            .NonLazy();
    }

 

    // private void BindHelper()
    // {
    //     Container.Bind<Helper>()
    //             .FromInstance(helper)
    //             .AsSingle();
    // }
    // private void BindGenerator()
    // {
    //     Container.Bind<GridGenerator>()
    //             .FromInstance(grid)
    //             .AsSingle()
    //             .NonLazy();
    // }

    // private void BindNumbersPool()
    // {
    //     Container.Bind<NumbersPool>()
    //             .FromInstance(numbersPool)
    //             .AsSingle()
    //             .NonLazy();
    // }
    

}
