using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public GameObject _player;

    //spawn related
    public Transform[] respawnPositions;

    

    public enum gameState
    {
        Menu,
        Idle,
        Round,
        End
    };

    

    
    void Awake()
    {
        
        if(_instance != null)
        {
            Debug.LogError("Already an instance of " + this.gameObject.name);
        }
        else
        {
            _instance = new GameManager();

        }
        //initialize player stuff
        _player = GameObject.Find("Player");
        //sub to events it will need to hear
        _player.GetComponent<Player>().PlayerDead += PlayerDead;
    }

    public void PlayerRespawn()
    {
        _player.transform.position = respawnPositions[Random.Range(0, respawnPositions.Length)].position;
        _player.GetComponent<Health>().SetHealth(100);
    }

    public void PlayerDead()
    {
        PlayerRespawn();
    }
}
