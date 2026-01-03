using Core.InputManager;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    //[SerializeField] private GameObject _playerPrefab;
    public override void InstallBindings()
    {
        BindPlayer();
    }

    private void BindInputManager()
    {
    }

    private void BindPlayer()
    {
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInput>().FromComponentInHierarchy().AsSingle();
    }
}
