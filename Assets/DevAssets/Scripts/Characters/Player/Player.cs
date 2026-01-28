using UnityEngine;
using DevAssets.Interfaces;

namespace DevAssets.Core.Characters.Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerInput))]
    public class Player : MonoBehaviour, ITarget
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerInput _playerInput;

        private Rigidbody2D _rigidBody;
        private TrailRenderer _trailRenderer;
        private SpriteRenderer _spriteRenderer;

        public Rigidbody2D RigidBody => _rigidBody;
        public TrailRenderer TrailRenderer => _trailRenderer;
        
        public Transform Transform => transform;
        public bool IsActive => gameObject.activeSelf;

        private void OnValidate()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Awake()
        {
            _rigidBody = _rigidBody ? _rigidBody : GetComponent<Rigidbody2D>();
            _trailRenderer = _trailRenderer ? _trailRenderer : GetComponent<TrailRenderer>();
            _spriteRenderer = _spriteRenderer ? _spriteRenderer : GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            FlipSpriteWhileMovement();
        }

        private void FlipSpriteWhileMovement()
        {
            var movementDirection = _playerInput.MoveInput.action.ReadValue<Vector2>();
            if (movementDirection.x > 0)
                FlipXSprite(true);

            else if (movementDirection.x < 0)
                FlipXSprite(false);
        }

        private void FlipXSprite(bool flip)
        {
            _spriteRenderer.flipX = flip;
        }
    }
}