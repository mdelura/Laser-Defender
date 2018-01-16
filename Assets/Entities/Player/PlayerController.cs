using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{

    public int hitPoints = 3;
    public int collisionDamage = 1;
    public float movementSpeed = 10f;

    public GameObject projectile;
    public float projectileSpeed = 20;
    public float projectilesInterval;

    public AudioClip shot;
    public AudioClip destroy;

    Boundaries _boundaries;
    Movement _movement;
    Vector2 _size;

    float _lastShotTime;

    LevelManager _levelManager;

    // Use this for initialization
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _lastShotTime = -projectilesInterval;
        _size = GetComponent<SpriteRenderer>().size;
        _boundaries = new Boundaries(_size / 2);
        _movement = new Movement(gameObject, _boundaries);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            move += _movement.MoveBounded(Vector3.up, movementSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            move += _movement.MoveBounded(Vector3.down, movementSpeed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            move += _movement.MoveBounded(Vector3.left, movementSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            move += _movement.MoveBounded(Vector3.right, movementSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= _lastShotTime + projectilesInterval)
        {
            ShootProjectile(move);
        }
    }

    private void ShootProjectile(Vector3 playerMovement)
    {
        _lastShotTime = Time.time;
        var projectile = Instantiate(this.projectile, transform.position, Quaternion.identity).GetComponent<IProjectile>();
        AudioSource.PlayClipAtPoint(shot, transform.position);

        projectile.Fire(playerMovement, new Vector2(0, projectileSpeed));
    }

    public void Damage(int damage)
    {

        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(destroy, transform.position);
            _levelManager.LoadLevel("Lose");

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.Damage(collisionDamage);
    }
}
