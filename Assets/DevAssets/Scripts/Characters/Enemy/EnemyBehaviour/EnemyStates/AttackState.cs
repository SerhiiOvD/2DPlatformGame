using UnityEngine;

namespace DevAssets.Characters.Enemies.EnemyStates
{
    public class AttackState : IState
    {
        private readonly Enemy _enemy;

        public AttackState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.RigidBody.linearVelocity = Vector2.zero;
        }

        public void Execute()
        {
            if (_enemy.IsPlayerActive() && !_enemy.IsDistanceToAttack())
                _enemy.EnemyStateMachine.TransitionTo(_enemy.EnemyStateMachine.WalkState);

            if (!_enemy.IsPlayerActive())
                _enemy.EnemyStateMachine.TransitionTo(_enemy.EnemyStateMachine.IdleState);

            _enemy.AttackSequance();
        }

        public void Exit()
        {
            // on exit 
        }

    }
}