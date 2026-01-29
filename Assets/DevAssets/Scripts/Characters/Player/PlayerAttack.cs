using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Core.Projectile;
using DevAssets.Controllers;

namespace DevAssets.Core.Characters.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private ProjectilePool _playerProjectilePool;
        [SerializeField] private int _projectileForce = 10;

        [SerializeField] private float _timeBetweenAttacks = 1;
        private float _lastTimeAttack;

        private PlayerInput _playerInput;
        private AimController _aimController;

        [Inject]
        public void Construct(PlayerInput playerInput, AimController aimController)
        {
            _playerInput = playerInput;
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

        private void AttackHandler()
        {
            if (Time.time - _lastTimeAttack >= _timeBetweenAttacks)
            {
                _lastTimeAttack = Time.time;
                Attack();
            }
        }

        private void Attack()
        {
            var projectileObject = _playerProjectilePool.GetPooledProjectile();

            if (projectileObject == null) return;

            projectileObject.transform.SetPositionAndRotation(gameObject.transform.position, _aimController.AimPoint.rotation);

            var directionFire = (_aimController.AimPoint.position - transform.position).normalized;
            projectileObject.RigidBody.linearVelocity = directionFire * _projectileForce;
            projectileObject.Deactivate();
        }
    }
}