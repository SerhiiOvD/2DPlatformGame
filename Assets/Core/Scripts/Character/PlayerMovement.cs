using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _dashForce = 10f;
    [SerializeField] private int _dashingTime = 5;
    [SerializeField] private int _dashingCooldown = 2;
    private Vector2 _moveDirection;
    private bool _isDashing = false;
    private bool _canDash = false;
    private Player _player;
    private PlayerInput _playerInput;

    [Inject]
    public void Construct(Player player, PlayerInput playerInput)
    {
        _player = player;
        _playerInput = playerInput;
    }

    private void OnEnable()
    {
        _playerInput.OnInputDash += DashHandler;
    }

    private void OnDisable()
    {
        _playerInput.OnInputDash -= DashHandler;
    }
    
    private void Update()
    {
        if(!_isDashing)
            _moveDirection = _playerInput.MoveInput.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if(!_isDashing)
            _player.RigidBody.linearVelocity = new Vector2(_moveDirection.x * _speed, _moveDirection.y * _speed);
    }

    private async void DashHandler()
    {
        if(_canDash)
            Dash();

        await Task.Delay(_dashingCooldown * 1000);
        Debug.Log("Dash is ready to use.");
        _canDash = true;
    }
    private async void Dash()
    {
        _isDashing = true;
        _canDash = false;
        _player.TrailRenderer.emitting = true;

        _player.RigidBody.linearVelocity = new Vector2(_moveDirection.x * _dashForce, _moveDirection.y * _dashForce);
        await Task.Delay(_dashingTime * 100);

        _player.TrailRenderer.emitting = false;
        _isDashing = false;
    }
}
