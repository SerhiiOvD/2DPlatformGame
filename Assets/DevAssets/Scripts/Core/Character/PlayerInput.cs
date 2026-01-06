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
        [SerializeField] private InputActionReference _mousePosition;

        public InputActionReference MoveInput => _moveInput;
        public InputActionReference MousePosition => _mousePosition;

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

        public Vector2 GetMoveInputPos() => _moveInput.action.ReadValue<Vector2>();
        
        public Vector2 GetMousePos() => _mousePosition.action.ReadValue<Vector2>();
    }
}