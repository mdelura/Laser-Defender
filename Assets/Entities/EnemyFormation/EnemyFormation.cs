using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyFormation : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float movementSpeed = 5f;
    

    Boundaries _boundaries;
    Movement _movement;

    Vector3 _moveDirection;

    // Use this for initialization
    void Start()
    {
        //Spawn the enemies
        SpawnNewEnemies();

        //Calculate initial boundaries
        _boundaries = new Boundaries(CalculateFormationSize(GetExistingEnemiesPositions()) / 2);
        _movement = new Movement(gameObject, _boundaries);

        //Set the initial move direction
        _moveDirection = Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right;
    }

    private void SpawnNewEnemies()
    {
        foreach (Transform child in transform)
        {
            var enemy = Instantiate(enemyPrefab, child.position, Quaternion.identity);
            enemy.transform.parent = child;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var enemiesPositions = GetExistingEnemiesPositions();

        if (enemiesPositions.Length == 0)
        {
            SpawnNewEnemies();
        }

        _movement.MoveBounded(_moveDirection, movementSpeed);

        var newPosition = _boundaries.GetRestrictedPosition(_movement.GetMovePosition(_moveDirection, movementSpeed));

        if (newPosition.x <= _boundaries.XMin)
        {
            _moveDirection = Vector3.right;
        }
        else if (newPosition.x >= _boundaries.XMax)
        {
            _moveDirection = Vector3.left;
        }
    }

    private Vector3 CalculateFormationSize(Vector3[] positions)
    {
        if (positions.Length == 0)
            return new Vector3();

        float xMin = positions.Min(p => p.x);
        float xMax = positions.Max(p => p.x);
        float yMin = positions.Min(p => p.y);
        float yMax = positions.Max(p => p.y);

        var enemySize = enemyPrefab.GetComponent<SpriteRenderer>().size;

        return new Vector3(xMax - xMin + enemySize.x, yMax - yMin + enemySize.y);
    }

    private void OnDrawGizmos()
    {
        var positions = transform
            .Cast<Transform>()
            .Select(t => t.position)
            .ToArray();

        float xMin = positions.Min(p => p.x);
        float xMax = positions.Max(p => p.x);
        float yMin = positions.Min(p => p.y);
        float yMax = positions.Max(p => p.y);

        var center = new Vector3((xMin + xMax) / 2, (yMin + yMax) / 2);


        Gizmos.DrawWireCube(center, CalculateFormationSize(positions));
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
