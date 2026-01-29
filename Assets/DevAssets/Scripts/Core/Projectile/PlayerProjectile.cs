using Core.Projectile;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    private const string ENEMY_TAG = "Enemy";
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Debug.Log($"Damage to {collision.gameObject}");
        }
    }
}
