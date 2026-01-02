using Core.InputManager;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindInputManager();
        BindPlayer();
    }

    private void BindInputManager()
    {
        Container.Bind<InputManager>().AsSingle().OnInstantiated<InputManager>((c, i) => i.Enable());
    }

    private void BindPlayer()
    {
        Container.Bind<PlayerInput>().FromComponentInHierarchy().AsSingle();
    }
}
