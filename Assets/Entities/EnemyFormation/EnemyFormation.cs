using System;
using System.Collections.Generic;
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

    Dictionary<Transform, int> _formationSetup = new Dictionary<Transform, int>();

    // Use this for initialization
    void Start()
    {
        _enemySize = enemyPrefab.GetComponent<SpriteRenderer>().size;
        //Spawn the enemies
        SpawnNewEnemies();

        //Calculate initial boundaries
        _boundaries = new Boundaries(_enemySize / 2);
        _movement = new Movement(gameObject, _boundaries);

        _formationSetup = transform
            .Cast<Transform>()
            .ToDictionary(t => t, t => 0);



        //Set the initial move direction
        _moveDirection = UnityEngine.Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;
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
            SendNewWave();
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
        if (UnityEngine.Random.value < directionChangeProbability)
        {
            _moveDirection = -_moveDirection;
        }
    }

    private void SendNewWave()
    {
        RaiseLevel();
        SpawnUntilFull();
    }

    private void RaiseLevel()
    {
        var lowestLevel = _formationSetup.Min(kv => kv.Value);

        var raisedPosition = _formationSetup
            .First(kv => kv.Value == lowestLevel)
            .Key;

        _formationSetup[raisedPosition]++;

        movementSpeed += lowestLevel * 0.1f;
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
            enemy.GetComponent<Enemy>().Level = _formationSetup[nextFreePosition];

            Invoke(nameof(SpawnUntilFull), spawnDelay);
        }
    }

    private Vector3[] GetExistingEnemiesPositions()
    {
        return transform
            .Cast<Transform>()
            .Where(t => t.childCount > 0)
            .Select(t => t.position)
            .ToArray();
    }
}
