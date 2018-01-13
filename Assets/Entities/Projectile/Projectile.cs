using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    public int damage = 1;

    private bool _isFired;

    public void Fire(params Vector2[] velocities)
    {
        if (_isFired)
        {
            Debug.LogError("Projectile cannot be fired more than once.");
            return;
        }

        _isFired = true;

        Vector2 totalVelocity = new Vector2();
        foreach (var velocity in velocities)
        {
            totalVelocity += velocity;
        }

        GetComponent<Rigidbody2D>().velocity = totalVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.Damage(damage);
        Destroy(gameObject);
    }


}
