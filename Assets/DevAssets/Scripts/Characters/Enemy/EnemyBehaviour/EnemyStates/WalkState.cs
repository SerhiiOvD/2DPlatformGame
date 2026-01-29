namespace DevAssets.Characters.Enemies.EnemyStates
{
    public class WalkState : IState
    {
        private readonly Enemy _enemy;

        public WalkState(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void Enter()
        {

        }

        public void Execute()
        {
            if (!_enemy.IsPlayerActive())
                _enemy.EnemyStateMachine.TransitionTo(_enemy.EnemyStateMachine.NeutralState);

            if (_enemy.IsDistanceToAttack())
                _enemy.EnemyStateMachine.TransitionTo(_enemy.EnemyStateMachine.AttackState);

            _enemy.ChaseTheTarget();

        }

        public void Exit()
        {

        }
    }
}