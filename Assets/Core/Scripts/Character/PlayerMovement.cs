using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private readonly PlayerInput _playerInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }
}
