using System;
using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    //round manager is the global time keeper and event signal for the flow of night/round cycles
    public static RoundManager _instance;

    //global round related
    public static int enemiesLeft = 0;
    public static int roundCounter = 0;

    //day cycle events
    //end events are essentially transition events between one stage and another, allowing for time buffers, visual sequences and other things to be triggered in between the start points
    public static Action TransitionToNightStart;
    public static Action NightStart; // this used to be 'RoundStart' which is essentially the same thing - a round is now defined as a Night
    public static Action TransitionToDayStart;
    public static Action DayStart;
    public static Action TransitionToDuskStart;
    public static Action DuskStart;
    
    //other static round events
    public static Action EnemyKilled;
    public static Action EnemySpawned;
    public static Action GameOverEvent;

    //durations for round/night cycle
    public static float transitionToNightDuration = 5;
    public static float transitionToDayDuration = 5;
    public static float dayDuration = 5;
    public static float transitionToDuskDuration = 6;
    public static float duskDuration = 6;

    //spawn related
    public Transform[] enemySpawnPositions;

    //round vars
    [Header("Round Variables")]
    public int totalRounds = 5;
    public int roundEnemyStartCount = 20;
    public float enemySpawnRate = 5f;
    private int enemiesSpawned = 0;

    //diffulty scaling management
    [Header("Difficulty Scaling")]
    public float difficultyModifier = 1.2f;
    

    //enemy prefab for spawning
    public GameObject enemyPrefab;

    //public
    public AudioManager audioManager;

    void Start()
    {
        if (_instance != null)
        {
            Debug.LogError("Already an instance of " + this.gameObject.name);
        }
        else
        {
            _instance = new RoundManager();
        }
        audioManager = FindObjectOfType<AudioManager>(); // remove this later - audio manager should get it's triggers to play sounds based on events only
        //very start of the game, night starts - after a beginning of game intro time
        ResetRoundParameters();
        Night();
        
    }

    private void ResetRoundParameters()
    {
        roundCounter = 0;
        enemiesLeft = 0;
    }

    public void Night()
    {
        enemiesSpawned = 0;
        if (roundCounter < totalRounds)
        {
            roundCounter++;
            //adjust diffculity scaling values if NOT first round
            if (roundCounter != 1)
            {
                //multiply by difficulty modifier
                roundEnemyStartCount += (int)(roundEnemyStartCount * difficultyModifier);
                //need a better difficulty modification process
                enemySpawnRate = (int)(enemySpawnRate * difficultyModifier);
            }
            //set values
            Debug.Log("number of enemies left is " + enemiesLeft);

            //start the round loop after stopping any existing ones
            StopAllCoroutines();
            StartCoroutine(NewNightRoundClock());
        }
    }

    public IEnumerator NewNightRoundClock()
    {
        if (roundCounter == 1)
        {
            yield return new WaitForSeconds(2f);
        }

        // difficulty modifier will change how quick the cooldown lasts  - roundCoolDownTime -= 2;
        //UI listener queues visual signal of start
        NightStart?.Invoke();
        while (enemiesSpawned < roundEnemyStartCount)
        {
            //do spawn until the player kills all enemies
            yield return new WaitForSeconds(enemySpawnRate);
            SpawnEnemy();
        }
    }

    public IEnumerator TransitionToDay()
    {
        StopCoroutine(NewNightRoundClock());
        TransitionToDayStart?.Invoke();
        yield return new WaitForSeconds(transitionToDayDuration);
        StartCoroutine(Day());
    }

    public IEnumerator Day()
    {
        StopCoroutine(TransitionToDay());
        DayStart?.Invoke();
        yield return new WaitForSeconds(dayDuration);
        StartCoroutine(TransitionToDusk());
    }

    public IEnumerator TransitionToDusk()
    {
        StopCoroutine(Day());
        TransitionToDuskStart?.Invoke();
        yield return new WaitForSeconds(transitionToDuskDuration);
        StartCoroutine(Dusk());
    }

    public IEnumerator Dusk()
    {
        StopCoroutine(TransitionToDusk());
        DuskStart?.Invoke();
        yield return new WaitForSeconds(transitionToDuskDuration);
        Night();

    }
    

    //Enemy Specifics
    public void KillEnemy()
    {
        enemiesLeft--;
        EnemyKilled?.Invoke();
        if (enemiesLeft == 0 && enemiesSpawned == roundEnemyStartCount)
        {
            ////event signals the end of a 'Night' round, and it's transition period to Daytime Starts
            //TransitionToDayStart?.Invoke();
            if (roundCounter < totalRounds)
            {
                StartCoroutine(TransitionToDay());
            } else
            {
                GameOver();
            }
            
        }
    }

    public void SpawnEnemy()
    {
            enemiesLeft++;
            enemiesSpawned++;
            EnemySpawned?.Invoke();
            var enemy = Instantiate(enemyPrefab, enemySpawnPositions[UnityEngine.Random.Range(0, enemySpawnPositions.Length)].position, enemySpawnPositions[UnityEngine.Random.Range(0, enemySpawnPositions.Length)].rotation);
        
    }
    public void GameOver()
    {
        GameOverEvent?.Invoke();
    }
}
