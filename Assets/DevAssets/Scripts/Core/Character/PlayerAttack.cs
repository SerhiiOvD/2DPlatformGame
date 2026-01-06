using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Core.Projectile;
using Core.Character.Aim;

namespace Core.Character
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private int _projectileForce = 10;

        [Tooltip ("In milliseconds")]
        [SerializeField] private int _cooldownAttack = 500;
        private bool _canAttack = true;

        private ProjectilePool _projectilePool;
        private PlayerInput _playerInput;
        private AimController _aimController;

        [Inject]
        public void Construct(PlayerInput playerInput, ProjectilePool projectilePooling, AimController aimController)
        {
            _playerInput = playerInput;
            _projectilePool = projectilePooling;
            _aimController = aimController;
        }

        private void Awake()
        {
            _playerInput.OnInputAttack += AttackHandler;
        }

        private void OnDestroy()
        {
            _playerInput.OnInputAttack -= AttackHandler;
        }

        private async void AttackHandler()
        {
            if (!_canAttack) return;

            Attack();
            _canAttack = false;
            await Task.Delay(_cooldownAttack);
            _canAttack = true;
        }

        private void Attack()
        {
            var projectileObject = _projectilePool.GetPooledProjectile();

            if (projectileObject == null) return;

            projectileObject.transform.SetPositionAndRotation(gameObject.transform.position, _aimController.AimPoint.rotation);
            
            var directionFire = (_aimController.AimPoint.position - transform.position).normalized;
            projectileObject.RigidBody.linearVelocity = directionFire * _projectileForce;

            projectileObject.Deactivate();
        }
    }
}