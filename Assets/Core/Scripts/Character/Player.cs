using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(TrailRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private TrailRenderer _trailRenderer;

    public Rigidbody2D RigidBody => _rigidBody;
    public TrailRenderer TrailRenderer => _trailRenderer;

    private void OnValidate()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }
    
}
