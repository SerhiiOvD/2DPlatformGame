using Core.Character;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Enemy : MonoBehaviour
{
    private EnemyStateMachine _enemyStateMachine;

    [SerializeField] protected Rigidbody2D _rigidBody;
    [SerializeField] protected float _walkSpeed = 5f;
    [SerializeField] protected float _rangeAttack = 5f;
    [Tooltip("In seconds.")][SerializeField] protected int _attackSequance = 1;

    protected bool _canAttack = true;

    public Rigidbody2D RigidBody => _rigidBody;
    public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

    [Inject] protected readonly Player _player;

    private void OnValidate()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _enemyStateMachine = new EnemyStateMachine(this);

        EnemyStateMachine.Initialize(EnemyStateMachine.IdleState);
    }

    private void Update()
    {
        EnemyStateMachine.Update();
    }

    public virtual void Idle()
    {
        
    }
    public virtual void ChaseThePlayer()
    {
        Vector2 dirToPlayer = (_player.transform.position - transform.position).normalized;
        _rigidBody.linearVelocity = dirToPlayer * _walkSpeed;
    }
    
    public abstract void Attack();
    

    public bool IsPlayerActive() => _player.isActiveAndEnabled;
    public bool IsDistanceToAttack() => Vector2.Distance(transform.position, _player.transform.position) < _rangeAttack;

}