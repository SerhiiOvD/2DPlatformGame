using System;
using System.Threading.Tasks;
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
        [Tooltip("In seconds.")][SerializeField] protected int _attackSequance = 1;

        protected bool _canAttack = true;

        public Rigidbody2D RigidBody => _rigidBody;
        public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;
        
        public event Action OnEnemyDie;

        [Inject] protected readonly ITarget _target;

        private void OnValidate()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _enemyStateMachine = new EnemyStateMachine(this);

            EnemyStateMachine.Initialize(EnemyStateMachine.IdleState);
        }

        protected virtual void Update()
        {
            EnemyStateMachine.Update();
        }

        public async void AttackSequance()
        {
            if (!_canAttack) return;

            Attack();
            _canAttack = false;
            await Task.Delay(_attackSequance * 1000);
            _canAttack = true;
        }

        protected abstract void Attack();
        public virtual void Death()
        {
            OnEnemyDie?.Invoke();
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