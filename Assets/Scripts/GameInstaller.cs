using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private Controls _controls;
    public override void InstallBindings()
    {
        _controls = new Controls();
        
        _controls.Game.Enable();
        Container.BindInstance(_controls.Game).AsSingle();
        //Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<CellManager>().FromComponentInHierarchy().AsSingle();
        
    }
}