using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public event Action Destroyed;

    #region Public fields exposed to Editor
    public GameObject projectile;
    public int initialHitPoints = 1;
    public float initialShotsPerSeconds = 0.6f;
    public float initialProjectileSpeed = 15;
    public int initialScoreValue = 150; 
    public Sprite[] sprites;
    public AudioClip shot;
    public AudioClip destroy;
    public GameObject explosion;

    #endregion

    #region Private fields
    private int _level;
    private int _hitPoints;
    private float _shotsPerSeconds;
    private float _projectileSpeed;
    private int _scoreValue;


    private int _collisionDamage = 1;
    private ScoreKeeper _scoreKeeper;

    #endregion

    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;

            OnSetLevel();
        }
    }

    private void Start()
    {
        _scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        OnSetLevel();
    }

    void Update()
    {
        float shotProbability = Time.deltaTime * _shotsPerSeconds;
        if (UnityEngine.Random.value < shotProbability)
        {
            ShootProjectile(new Vector3());
        }
    }

    private void OnSetLevel()
    {
        _hitPoints = initialHitPoints + _level;
        _shotsPerSeconds = initialShotsPerSeconds + 0.1f * _level;
        _projectileSpeed = initialProjectileSpeed + _level;
        _scoreValue = initialScoreValue + 50 * _level;
        SetSprite();
        print($"Level {_level}, HP: {_hitPoints}, SP: {_shotsPerSeconds}, PS: {_projectileSpeed}, SV: {_scoreValue}");
    }

    private void SetSprite() => GetComponent<SpriteRenderer>().sprite = sprites[Mathf.Clamp(_level, 0, sprites.Length - 1)];

    private void ShootProjectile(Vector3 velocity)
    {
        var projectile = Instantiate(this.projectile, transform.position, Quaternion.identity).GetComponent<IProjectile>();
        projectile.Fire(velocity, new Vector2(0, -_projectileSpeed));
        AudioSource.PlayClipAtPoint(shot, transform.position);
    }

    public void Damage(int damage)
    {
        _hitPoints -= damage;
        if (_hitPoints <= 0)
        {
            _scoreKeeper.Score(_scoreValue);
            AudioSource.PlayClipAtPoint(destroy, transform.position);
            Instantiate(explosion, transform.position, Quaternion.identity);
            OnDestroyed();
            Destroy(gameObject);
        }
    }

    private void OnDestroyed() => Destroyed?.Invoke();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.Damage(_collisionDamage);
    }

}