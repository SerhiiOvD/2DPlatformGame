using Zenject;
using Core.Character;
using Core.Character.Aim;
using Core.Projectile;


namespace Configs.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindPlayer();
            BindProjectile();

            Container.Bind<AimController>().FromComponentInHierarchy().AsSingle();
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
