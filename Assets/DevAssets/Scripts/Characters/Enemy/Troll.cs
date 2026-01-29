using UnityEngine;

namespace DevAssets.Characters.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class Troll : Enemy
    {
        private const string ATTACK_PARAMETER = "Attack";

        private Animator _animator;

        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
        }

        protected override void Attack()
        {
            _animator.SetTrigger(ATTACK_PARAMETER);
        }
    }
}