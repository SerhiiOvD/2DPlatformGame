using Core.Projectile;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private const string PLAYER_TAG = "Player";
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            Debug.Log($"Damage to {collision.gameObject}");
        }
    }
}
