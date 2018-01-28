using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;

    public Sprite playerOneSprite;
    public Sprite playerTwoSprite;

    private int _playersCount;

    LevelManager _levelManager;

    private void Start()
    {
        _playersCount = (int)Preferences.PlayersNumber;
        _levelManager = FindObjectOfType<LevelManager>();

        switch (Preferences.PlayersNumber)
        {
            case Preferences.Players.One:
                InstantiatePlayer(0, playerOneSprite, Preferences.P1Controls);
                break;
            case Preferences.Players.Two:
                InstantiatePlayer(1, playerOneSprite, Preferences.P1Controls);
                InstantiatePlayer(2, playerTwoSprite, Preferences.P2Controls);
                break;
        }
    }

    private void InstantiatePlayer(int positionIndex, Sprite sprite, Controls controls)
    {
        Transform position = transform.GetChild(positionIndex);
        var player = Instantiate(playerPrefab, position.position, Quaternion.identity);

        player.GetComponent<SpriteRenderer>().sprite = sprite;
        var playerController = player.GetComponent<PlayerController>();
        playerController.Controls = controls;
        playerController.Destroyed += PlayerController_Destroyed;
    }

    private void PlayerController_Destroyed()
    {
        _playersCount--;
        if(_playersCount <= 0)
        {
            _levelManager.LoadLevel("Lose");
        }
    }
}
