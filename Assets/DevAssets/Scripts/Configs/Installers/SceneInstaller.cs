using Zenject;
using DevAssets.Core.Characters.Player;
using DevAssets.Characters.Enemies;
using DevAssets.Controllers;
using Core.Projectile;
using UnityEngine;
using DevAssets.Interfaces;


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
            Container.Bind<ITarget>().To<Player>().FromComponentInHierarchy().AsCached();
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
