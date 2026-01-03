using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPlayer();
    }
    private void BindPlayer()
    {
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerInput>().FromComponentInHierarchy().AsSingle();
    }
}
