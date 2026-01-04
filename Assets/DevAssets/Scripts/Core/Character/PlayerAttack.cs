using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Core.Projectile;

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

        [Inject]
        public void Construct(PlayerInput playerInput, ProjectilePool projectilePooling)
        {
            _playerInput = playerInput;
            _projectilePool = projectilePooling;
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
            await Task.Delay(_cooldownAttack);
            _canAttack = true;
        }

        private void Attack()
        {
            _canAttack = false;
            // Vector2 mousePos = Input.mousePosition; //TODO: add direction attack depands on mouse pos.
            var projectileObject = _projectilePool.GetPooledProjectile();

            if (projectileObject == null) return;

            projectileObject.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
            projectileObject.RigidBody.AddForce(projectileObject.transform.up * _projectileForce, ForceMode2D.Impulse);
            
            projectileObject.Deactivate();
        }
    }
}