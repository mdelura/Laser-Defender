﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int hitPoints = 1;

    public GameObject projectile;
    public float shotsPerSeconds = 0.5f;
    public float projectileSpeed = 20;
    private int collisionDamage = 1;

    void Update()
    {
        float shotProbability = Time.deltaTime * shotsPerSeconds;
        if (Random.value < shotProbability)
        {
            ShootProjectile(new Vector3());
        }
    }

    private void ShootProjectile(Vector3 velocity)
    {
        var projectile = Instantiate(this.projectile, transform.position, Quaternion.identity).GetComponent<IProjectile>();
        projectile.Fire(velocity, new Vector2(0, -projectileSpeed));
    }

    public void Damage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.Damage(collisionDamage);
    }

}