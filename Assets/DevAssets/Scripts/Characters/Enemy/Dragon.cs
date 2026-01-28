using UnityEngine;
using Zenject;
using Core.Projectile;
using DevAssets.Characters.Enemies;


namespace DevAssets.Core.Characters.Enemies
{
    public class Dragon : Enemy
    {
        [SerializeField] private float _projectileForce = 5f;
        [Inject] private readonly ProjectilePool _projectilePool;

        protected override void Attack()
        {
            var projectile = _projectilePool.GetPooledProjectile();
            if (projectile == null) return;

            var dirToPlayer = (_target.Transform.position - gameObject.transform.position).normalized;

            float rotZ = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
            rotZ -= 90f; // shall to adjust the angle, unity think that forward is a right
            var projectileRotToPlayer = Quaternion.Euler(0, 0, rotZ);

            projectile.transform.SetPositionAndRotation(gameObject.transform.position, projectileRotToPlayer);
            projectile.RigidBody.linearVelocity = dirToPlayer * _projectileForce;

            projectile.Deactivate();
        }

    }
}