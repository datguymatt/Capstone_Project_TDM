using UnityEngine;
using UnityEngine.SceneManagement;

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
    }
}
