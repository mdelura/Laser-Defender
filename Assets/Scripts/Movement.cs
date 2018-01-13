using UnityEngine;

class Movement
{
    GameObject _gameObject;
    Boundaries _boundaries;

    public Movement(GameObject gameObject) : this(gameObject, new Boundaries())
    {
    }

    public Movement(GameObject gameObject, Boundaries boundaries)
    {
        _gameObject = gameObject;
        _boundaries = boundaries;
    }


    public Vector3 MoveBounded(Vector3 velocity, float speedFactor)
    {
        var move = _boundaries.GetRestrictedPosition(GetMovePosition(velocity, speedFactor)) - _gameObject.transform.position;
        _gameObject.transform.position = _boundaries.GetRestrictedPosition(GetMovePosition(velocity, speedFactor));

        return move;
    }

    public Vector3 MoveUnbounded(Vector3 velocity, float speedFactor)
    {
        var move = GetMovePosition(velocity, speedFactor) - _gameObject.transform.position;
        _gameObject.transform.position = GetMovePosition(velocity, speedFactor);

        return move;
    }

    public Vector3 GetMovePosition(Vector3 velocity, float speedFactor) => _gameObject.transform.position + velocity * speedFactor * Time.deltaTime;
}