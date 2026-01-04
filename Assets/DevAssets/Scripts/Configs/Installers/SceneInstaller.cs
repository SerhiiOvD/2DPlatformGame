using Zenject;
using Core.Character;
using Core.Projectile;

namespace Configs.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayer();
            BindProjectile();
        }
        private void BindPlayer()
        {
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerInput>().FromComponentInHierarchy().AsSingle();
        }

        private void BindProjectile()
        {
            Container.Bind<ProjectilePool>().FromComponentInHierarchy().AsSingle();
        }
    }
}
