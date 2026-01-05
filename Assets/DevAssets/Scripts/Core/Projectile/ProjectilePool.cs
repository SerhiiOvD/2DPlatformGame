using UnityEngine;
using UnityEngine.Pool;

namespace Core.Projectile
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private int _defaultCapacity = 20;
        [SerializeField] private int _maxCapacity = 100;
        
        private bool _collectionCheck = true;
        private IObjectPool<Projectile> _projectilePool;


        private void Awake()
        {
            _projectilePool = new ObjectPool<Projectile>
                (CreatProjectileAndSetObjectPool, OnGetProjectile, OnReleaseProjectile, OnDestoryProjectile,
                    _collectionCheck, _defaultCapacity, _maxCapacity);
        }

        private Projectile CreatProjectileAndSetObjectPool()
        {
            Projectile projectileInstance = Instantiate(_projectilePrefab, gameObject.transform);
            projectileInstance.ObjectPool = _projectilePool;
            return projectileInstance;
        }

        private void OnGetProjectile(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }

        private void OnReleaseProjectile(Projectile projectile)
        {
            if (projectile != null)
                projectile.gameObject.SetActive(false);
        }

        private void OnDestoryProjectile(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }

        public Projectile GetPooledProjectile() => _projectilePool.Get();
    }
}