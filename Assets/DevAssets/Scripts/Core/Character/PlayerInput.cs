using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Core.Character
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference _moveInput;
        [SerializeField] private InputActionReference _attackInput;
        [SerializeField] private InputActionReference _dashInput;

        public InputActionReference MoveInput => _moveInput;

        public event Action OnInputAttack;
        public event Action OnInputDash;

        private void Awake()
        {
            _attackInput.action.started += Attack;
            _dashInput.action.started += Dash;

        }

        private void OnDestroy()
        {
            _attackInput.action.started -= Attack;
            _dashInput.action.started -= Dash;
        }

        private void Attack(InputAction.CallbackContext callbackContext)
        {
            OnInputAttack?.Invoke();
        }

        private void Dash(InputAction.CallbackContext callbackContext)
        {
            OnInputDash?.Invoke();
        }
    }
}