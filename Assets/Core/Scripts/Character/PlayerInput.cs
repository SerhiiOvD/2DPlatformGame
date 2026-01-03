using UnityEngine;
using System;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _attackInput;
    [SerializeField] private InputActionReference _dashInput;

    public InputActionReference MoveInput => _moveInput;

    public event Action OnInputAttack;
    public event Action OnInputDash;

    private void OnEnable()
    {
       _attackInput.action.started += Attack;
       _dashInput.action.started += Dash;

    }

    private void OnDisable()
    {
        _attackInput.action.started -=  Attack;
        _dashInput.action.started -= Dash;
    }

    private void Attack(InputAction.CallbackContext callbackContext)
    {
        OnInputAttack?.Invoke();
        Debug.Log("Button Attack Pressed");
    }

    private void Dash(InputAction.CallbackContext callbackContext)
    {
        OnInputDash?.Invoke();
        Debug.Log("Dash");
    }
    
}
