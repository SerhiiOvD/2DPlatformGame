using UnityEngine;
using Core.InputManager;
using System;
using Zenject;


public class PlayerInput : MonoBehaviour
{
    [Inject] private readonly InputManager _inputManager;

    public InputManager InputManager => _inputManager;
    public event Action OnInputAttack;

    private void OnEnable()
    {
        _inputManager.Player.Attack.performed += (e) => Attack();
    }

    private void OnDisable()
    {
        _inputManager.Player.Attack.performed -= (e) => Attack();
    }

    private void Attack()
    {
        OnInputAttack?.Invoke();
        Debug.Log("Button Attack Pressed");
    }
    
}
