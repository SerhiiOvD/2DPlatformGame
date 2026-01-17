using System.Threading.Tasks;
using Core.Projectile;
using UnityEngine;
using Zenject;

public class Dragon : Enemy
{
    [SerializeField] private float _projectileForce = 5f;
    [Inject] private ProjectilePool _projectilePool;

    protected override void Attack()
    {
        var projectile = _projectilePool.GetPooledProjectile();
        if (projectile == null) return;
        
        var dirToPlayer = (_player.transform.position - gameObject.transform.position).normalized;

        float rotZ = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
        rotZ -= 90f; // shall to adjust the angle, unity think that forward is a right
        var projectileRotToPlayer = Quaternion.Euler(0, 0, rotZ);

        projectile.transform.SetPositionAndRotation(gameObject.transform.position, projectileRotToPlayer);
        projectile.RigidBody.linearVelocity = dirToPlayer * _projectileForce;
        
        projectile.Deactivate();
    }

    // private async void AttackSequance()
    // {
    //     if (!_canAttack) return;

    //     var projectile = _projectilePool.GetPooledProjectile();
    //     if (projectile == null) return;
        
    //     var dirToPlayer = (_player.transform.position - gameObject.transform.position).normalized;

    //     float rotZ = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
    //     rotZ -= 90f; // shall to adjust the angle, unity think that forward is a right
    //     var projectileRotToPlayer = Quaternion.Euler(0, 0, rotZ);

    //     projectile.transform.SetPositionAndRotation(gameObject.transform.position, projectileRotToPlayer);
    //     projectile.RigidBody.linearVelocity = dirToPlayer * _projectileForce;
        
    //     projectile.Deactivate();

    //     _canAttack = false;
    //     await Task.Delay(_attackSequance * 1000);
    //     _canAttack = true;
    // }
}