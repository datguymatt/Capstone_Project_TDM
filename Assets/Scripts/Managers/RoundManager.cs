using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager _instance;
    public static float DayDuration;
    public static float DuskDuration;
    public static float NightDuration;

    //spawn related
    public Transform[] respawnPositions;

    [Header("Global Round Variables")]
    //global round related
    public int roundCounter = 0;
    public int totalRounds = 5;
    public int enemiesLeft = 0;
    public int dayTimeDuration = 10;
    public int enemiesSpawned = 0;

    //old events system
    public Action RoundStart;
    public Action RoundEnd;
    public Action EnemyKilled;
    public Action EnemySpawned;
    public Action GameOverEvent;

    //new events system - plan
    public Action DayStart;
    public Action DayTransition;
    public Action DuskStart;
    public Action DuskTransition;
    public Action NightStart;
    public Action NightTransition;

    //diffulty scaling management
    [Header("Difficulty Scaling")]
    public float difficultyModifier = 1.2f;
    public int roundEnemyStartCount = 20;
    public float enemySpawnRate = 5f;

    //enemy prefab for spawning
    public GameObject enemyPrefab;

    //public
    public AudioManager audioManager;

    void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("Already an instance of " + this.gameObject.name);
        }
        else
        {
            _instance = new RoundManager();
        }
        InitializeRound();
        audioManager = FindObjectOfType<AudioManager>();
    }

    //set methods
    public void KillEnemy()
    {
        enemiesLeft--;
        EnemyKilled?.Invoke();
        if (enemiesLeft == 0 && enemiesSpawned == roundEnemyStartCount)
        {
            //UI listener queues visual signal of end
            RoundEnd?.Invoke();
            //FADE SOUND prototype - this should be subscribed to by audiomanager
            audioManager.musicNight.DOFade(0, 10);
            //start over with initializing
            InitializeRound();
        }
    }


    public void InitializeRound()
    {
        enemiesSpawned = 0;
        if (roundCounter < totalRounds)
        {
            // new round starts
            roundCounter++;
            //adjust diffculity scaling values if NOT first round
            if (roundCounter != 1)
            {
                //multiply by difficulty modifier
                roundEnemyStartCount = (int)(roundEnemyStartCount * difficultyModifier);
                enemySpawnRate = (int)(enemySpawnRate * difficultyModifier);
            }
            //set values
            Debug.Log("number of enemies left is " + enemiesLeft);

            //start the round loop after stopping any existing ones
            StopAllCoroutines();
            StartCoroutine(NewRoundLoop());
        }
        else
        {
            GameOver();
        }
    }

    public IEnumerator NewRoundLoop()
    {
        //start a new round loop of enemy spawns after a cooldown buffer which will get smaller each round
        if (roundCounter != 1)
        {
            yield return new WaitForSeconds(dayTimeDuration);
        }
        else
        {
            yield return new WaitForSeconds(1f);
        }
        
        // difficulty modifier will change how quick the cooldown lasts  - roundCoolDownTime -= 2;
        //UI listener queues visual signal of start
        RoundStart?.Invoke();
        while (enemiesSpawned < roundEnemyStartCount)
        {
            //do until the player kills all enemies
            yield return new WaitForSeconds(enemySpawnRate);
            SpawnEnemy();
        }
        
    }

    public void SpawnEnemy()
    {
            enemiesLeft++;
            enemiesSpawned++;
            EnemySpawned?.Invoke();
            var enemy = Instantiate(enemyPrefab, respawnPositions[UnityEngine.Random.Range(0, respawnPositions.Length)].position, respawnPositions[UnityEngine.Random.Range(0, respawnPositions.Length)].rotation);   
        
    }
    public void GameOver()
    {
        GameOverEvent?.Invoke();
        //put all the logic that will end the game and start a new sequence
    }
}
