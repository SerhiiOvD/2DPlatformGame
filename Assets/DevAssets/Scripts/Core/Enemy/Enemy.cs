using Core.Character;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private EnemyStateMachine _enemyStateMachine;

    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _rangeAttack = 5f;

    public Rigidbody2D RigidBody => _rigidBody;
    public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

    [Inject] private readonly Player _player;

    private void OnValidate()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _enemyStateMachine = new EnemyStateMachine(this);

        EnemyStateMachine.Initialize(EnemyStateMachine.WalkState);
    }

    private void Update()
    {
        EnemyStateMachine.Update();
    }

    public void Idle()
    {
        Debug.Log("Play Idle anim");
    }

    public void ChaseThePlayer()
    {
        Vector2 dirToPlayer = (_player.transform.position - transform.position).normalized;
        _rigidBody.linearVelocity = dirToPlayer * _walkSpeed;
    }

    public void Attack()
    {
        Debug.Log("Enemy Attack!");
    }

    public bool IsPlayerActive() => _player.isActiveAndEnabled;
    public bool IsDistanceToAttack() => Vector2.Distance(transform.position, _player.transform.position) < _rangeAttack;

}