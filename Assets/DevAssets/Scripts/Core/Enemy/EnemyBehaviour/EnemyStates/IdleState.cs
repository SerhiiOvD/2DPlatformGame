using UnityEngine;

public class IdleState : IState
{
    private readonly Enemy _enemy;

    public IdleState(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        _enemy.RigidBody.linearVelocity = Vector2.zero;

        _enemy.Idle();
    }

    public void Execute()
    {
        if (_enemy.IsPlayerActive() && !_enemy.IsDistanceToAttack())
            _enemy.EnemyStateMachine.TransitionTo(_enemy.EnemyStateMachine.WalkState);

        if (_enemy.IsDistanceToAttack())
            _enemy.EnemyStateMachine.TransitionTo(_enemy.EnemyStateMachine.AttackState);
    }

    public void Exit()
    {
        //On exit 
    }
}