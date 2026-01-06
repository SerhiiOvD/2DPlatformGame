using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Core.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        private const int SECOND_IN_MILLISECONDS = 1000;

        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 5f;

        [Header("Dashing")]
        [SerializeField] private float _dashForce = 10f;

        [Tooltip("In milliseconds delay")]
        [SerializeField]
        private int _dashingTime = 500;

        [Tooltip("In seconds delay")]
        [SerializeField]
        private int _dashingCooldown = 2;

        private Vector2 _moveDirection;
        private bool _isDashing = false;
        private bool _canDash = true;

        private Player _player;
        private PlayerInput _playerInput;

        [Inject]
        public void Construct(Player player, PlayerInput playerInput)
        {
            _player = player;
            _playerInput = playerInput;
        }

        private void Awake()
        {
            _playerInput.OnInputDash += DashHandler;
        }

        private void OnDestroy()
        {
            _playerInput.OnInputDash -= DashHandler;
        }

        private void Update()
        {
            if (!_isDashing)
                _moveDirection = _playerInput.GetMoveInputPos();
        }

        private void FixedUpdate()
        {
            if (!_isDashing)
                _player.RigidBody.linearVelocity = new Vector2(_moveDirection.x * _moveSpeed, _moveDirection.y * _moveSpeed);
        }

        private async void DashHandler()
        {
            if (!_canDash) return;

            Dash();
            _canDash = false;
            await Task.Delay(_dashingCooldown * SECOND_IN_MILLISECONDS);
            _canDash = true;
            Debug.Log("Dash is ready to use.");
        }
        private async void Dash()
        {
            _isDashing = true;
            _player.TrailRenderer.emitting = true;

            _player.RigidBody.linearVelocity = new Vector2(_moveDirection.x * _dashForce, _moveDirection.y * _dashForce);
            await Task.Delay(_dashingTime);

            _player.TrailRenderer.emitting = false;
            _isDashing = false;
        }
    }
}