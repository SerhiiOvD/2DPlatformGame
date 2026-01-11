using Zenject;
using Core.Character;
using Core.Character.Aim;
using Core.Projectile;
using UnityEngine;


namespace Configs.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Enemy _enemyPrefab; 
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindProjectile();
            BindEnemy();

            Container.Bind<AimController>().FromComponentInHierarchy().AsSingle();
        }
        private void BindPlayer()
        {
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerInput>().FromComponentInHierarchy().AsSingle();
        }

        private void BindEnemy()
        {
            // Container.Bind<Enemy>().FromComponentInNewPrefab(_enemyPrefab).AsSingle().NonLazy();
            Container.Bind<Enemy>().FromComponentInHierarchy().AsSingle();
        }

        private void BindProjectile()
        {
            Container.Bind<ProjectilePool>().FromComponentInHierarchy().AsSingle();
        }

    }
}
