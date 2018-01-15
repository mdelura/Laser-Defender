using System.Linq;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float movementSpeed = 5f;
    public float spawnDelay = 0.2f;

    private float _maxRandomDirectionChangePerSeconds = 0.1f;

    Boundaries _boundaries;
    Movement _movement;

    Vector3 _moveDirection;

    Vector2 _enemySize;

    // Use this for initialization
    void Start()
    {
        _enemySize = enemyPrefab.GetComponent<SpriteRenderer>().size;
        //Spawn the enemies
        SpawnNewEnemies();

        //Calculate initial boundaries
        _boundaries = new Boundaries(_enemySize / 2);
        _movement = new Movement(gameObject, _boundaries);

        //Set the initial move direction
        _moveDirection = Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;
    }

    private void SpawnNewEnemies()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount == 0)
            {
                var enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity);
                enemy.transform.parent = child;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetExistingEnemiesPositions().Length == 0)
        {
            SpawnUntilFull();
        }

        _movement.MoveUnbounded(_moveDirection, movementSpeed);

        if (GetMinX() <= _boundaries.XMin)
        {
            _moveDirection = Vector3.right;
        }
        else if (GetMaxX() >= _boundaries.XMax)
        {
            _moveDirection = Vector3.left;
        }

        ////Occassionaly change direction
        float directionChangeProbability = Time.deltaTime * _maxRandomDirectionChangePerSeconds;
        if (Random.value < directionChangeProbability)
        {
            _moveDirection = -_moveDirection;
        }
    }


    private float GetMinX() => GetExistingEnemiesPositions().Min(p => p.x);

    private float GetMaxX() => GetExistingEnemiesPositions().Max(p => p.x);


    private Transform NextFreePosition()
    {
        return transform
            .Cast<Transform>()
            .Where(t => t.childCount == 0)
            .FirstOrDefault();
    }

    private void SpawnUntilFull()
    {
        var nextFreePosition = NextFreePosition();
        if (nextFreePosition)
        {
            var enemy = Instantiate(enemyPrefab, nextFreePosition.position, Quaternion.identity);
            enemy.transform.parent = nextFreePosition;
            Invoke(nameof(SpawnUntilFull), spawnDelay);
        }
    }

    //private Vector3 CalculateFormationSize(Vector3[] positions)
    //{
    //    if (positions.Length == 0)
    //        return new Vector3();

    //    float xMin = positions.Min(p => p.x);
    //    float xMax = positions.Max(p => p.x);
    //    float yMin = positions.Min(p => p.y);
    //    float yMax = positions.Max(p => p.y);

    //    return new Vector3(xMax - xMin + _enemySize.x, yMax - yMin + _enemySize.y);
    //}

    //private void OnDrawGizmos()
    //{
    //    var positions = transform
    //        .Cast<Transform>()
    //        .Select(t => t.position)
    //        .ToArray();

    //    float xMin = positions.Min(p => p.x);
    //    float xMax = positions.Max(p => p.x);
    //    float yMin = positions.Min(p => p.y);
    //    float yMax = positions.Max(p => p.y);

    //    var center = new Vector3((xMin + xMax) / 2, (yMin + yMax) / 2);


    //    Gizmos.DrawWireCube(center, CalculateFormationSize(positions));
    //}

    private Vector3[] GetExistingEnemiesPositions()
    {
        return transform
            .Cast<Transform>()
            .Where(t => t.childCount > 0)
            .Select(t => t.position)
            .ToArray();
    }
}
