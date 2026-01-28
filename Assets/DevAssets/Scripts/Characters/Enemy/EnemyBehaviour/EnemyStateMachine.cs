using System;
using System.Threading.Tasks;
using DevAssets.Characters.Enemies.EnemyStates;

namespace DevAssets.Characters.Enemies
{
    public class EnemyStateMachine
    {
        public IState CurrentState { get; private set; }

        private readonly DeathState _deathState;
        private readonly WalkState _walkState;
        private readonly AttackState _attackState;

        public DeathState IdleState => _deathState;
        public WalkState WalkState => _walkState;
        public AttackState AttackState => _attackState;

        public event Action<IState> OnStateChanged;

        public EnemyStateMachine(Enemy enemy)
        {
            _deathState = new DeathState(enemy);
            _walkState = new WalkState(enemy);
            _attackState = new AttackState(enemy);
        }

        public void Initialize(IState state)
        {
            CurrentState = state;
            state.Enter();

            OnStateChanged?.Invoke(state);
        }

        public async void TransitionTo(IState nextState)
        {
            CurrentState.Exit();

            CurrentState = nextState;
            await Task.Yield();

            nextState.Enter();

            OnStateChanged?.Invoke(nextState);
        }

        public void Update()
        {
            CurrentState?.Execute();
        }
    }
}