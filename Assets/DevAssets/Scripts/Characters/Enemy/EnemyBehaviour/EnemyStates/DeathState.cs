using UnityEngine;

namespace DevAssets.Characters.Enemies.EnemyStates
{
    public class DeathState : IState
    {
        private readonly Enemy _enemy;

        public DeathState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {
            _enemy.RigidBody.linearVelocity = Vector2.zero;

            _enemy.Death();
        }

        public void Execute()
        {
            
        }

        public void Exit()
        {
            //On exit 
        }
    }
}