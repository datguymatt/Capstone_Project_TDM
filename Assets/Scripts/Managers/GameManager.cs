using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public GameObject _player;

    public enum gameState
    {
        Menu,
        Idle,
        Round,
        End
    };

    

    //spawn related
    public Transform[] respawnPositions;
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
        //initialize a round at the start, after that, rounds only triggered using events
    }

    public void PlayerRespawn()
    {
        //use health?.Invoke() to invoke this method when health falls to 0 or below
        _player.transform.position = respawnPositions[Random.Range(0, respawnPositions.Length)].position;
    }

    
}
