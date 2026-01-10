using UnityEngine;

public class WalkState : IState
{
    private readonly Enemy _enemy;
    
    public WalkState (Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        if (!_enemy.IsPlayerActive())
            _enemy.EnemyStateMachine.TransitionTo(_enemy.EnemyStateMachine.IdleState);

        if (_enemy.IsDistanceToAttack())
            _enemy.EnemyStateMachine.TransitionTo(_enemy.EnemyStateMachine.AttackState);

        _enemy.ChaseThePlayer();

    }

    public void Exit()
    {
       
    }
}