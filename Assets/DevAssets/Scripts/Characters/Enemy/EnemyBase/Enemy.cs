using System;
using DevAssets.Interfaces;
using UnityEngine;
using Zenject;

namespace DevAssets.Characters.Enemies
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public abstract class Enemy : MonoBehaviour
    {
        private EnemyStateMachine _enemyStateMachine;

        [SerializeField] protected Rigidbody2D _rigidBody;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] protected float _walkSpeed = 5f;
        [SerializeField] protected float _rangeAttack = 5f;
        [SerializeField] protected float _timeBetweenAttacks = 2f;

        protected float _lastTimeAttack;

        public Rigidbody2D RigidBody => _rigidBody;
        public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

        public event Action OnEnemyDeath;

        [Inject] protected readonly ITarget _target;

        private void OnValidate()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _enemyStateMachine = new EnemyStateMachine(this);

            EnemyStateMachine.Initialize(EnemyStateMachine.NeutralState);
        }

        protected virtual void Update()
        {
            EnemyStateMachine.Update();
        }

        public void AttackSequance()
        {
            if (Time.time - _lastTimeAttack >= _timeBetweenAttacks)
            {
                _lastTimeAttack = Time.time;
                Attack();
            }
        }

        protected abstract void Attack();
        public virtual void Death()
        {
            OnEnemyDeath?.Invoke();
            gameObject.SetActive(false);
        }
        public virtual void ChaseTheTarget()
        {
            FlipSpriteToPlayer();

            Vector2 dirToPlayer = (_target.Transform.position - transform.position).normalized;
            _rigidBody.linearVelocity = dirToPlayer * _walkSpeed;
        }

        private void FlipSpriteToPlayer()
        {
            var dirToPlayer = (_target.Transform.position - gameObject.transform.position).normalized;

            if (dirToPlayer.x > 0)
                _spriteRenderer.flipX = true;
            else if (dirToPlayer.x < 0)
                _spriteRenderer.flipX = false;
        }

        public bool IsPlayerActive() => _target.IsActive;
        public bool IsDistanceToAttack() => Vector2.Distance(transform.position, _target.Transform.position) < _rangeAttack;
    }
}