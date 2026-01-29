using UnityEngine;

namespace DevAssets.Characters.Enemies.EnemyStates
{
    public class NeutralState : IState
    {
        private readonly Enemy _enemy;

        public NeutralState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.RigidBody.linearVelocity = Vector2.zero;

        }

        public void Execute()
        {
            if (_enemy.IsPlayerActive())
                _enemy.EnemyStateMachine.TransitionTo(_enemy.EnemyStateMachine.WalkState);

            if (_enemy.IsDistanceToAttack())
                _enemy.EnemyStateMachine.TransitionTo(_enemy.EnemyStateMachine.AttackState);
        }

        public void Exit()
        {
            //On exit 
        }
    }
}