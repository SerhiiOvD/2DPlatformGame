using UnityEngine;

namespace DevAssets.Characters.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class Troll : Enemy
    {
        private const string ANIMATION_ATTACK_PARAMETER = "Attack";

        [SerializeField] private Animator _animator;

        private void OnValidate()
        {
            _animator = GetComponent<Animator>();
        }

        protected override void Attack()
        {
            _animator.SetTrigger(ANIMATION_ATTACK_PARAMETER);
        }
    }
}