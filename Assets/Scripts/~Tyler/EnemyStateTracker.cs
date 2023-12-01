using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStateTracker : MonoBehaviour
{
    private static EnemyStateTracker instance = null;
    public static EnemyStateTracker Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnemyStateTracker();
            }
            return instance;
        }
    }


    public UnityEvent enemyLeftAttack;

    private Dictionary<GameObject, string> enemyStates = new Dictionary<GameObject, string>();
    [SerializeField] private int maxAttackingEnemies;
    [SerializeField] private int currentAttackingEnemies;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        //Event subscription to increase or decrease the number of enemies that can attack at once for round difficulty or day/night cycle
    }


    public void UpdateEnemyState(GameObject enemyInstance, string newState)
    {
        if (!enemyStates.ContainsKey(enemyInstance))
        {
            enemyStates.Add(enemyInstance, newState);
        }
        else
        {
            enemyStates[enemyInstance] = newState;
        }
    }

    public int EnemiesInState(string enemyState)
    {
        int i = 0;
        foreach (string s in enemyStates.Values)
        {
            if (s == enemyState)
            {
                i++;
            }
        }
        return i;
    }

    public int EnemiesInState(string enemyState, string enemyState2)
    {
        int i = 0;
        foreach (string s in enemyStates.Values)
        {
            if (s == enemyState)
            {
                i++;
            }
            if (s == enemyState2)
            {
                i++;
            }
        }
        currentAttackingEnemies = i;
        return i;
    }

    public int GetMaxAttackingEnemies() { return maxAttackingEnemies; }

    private void UpdateMaxAttackingEnemies(int increment)
    {
        maxAttackingEnemies += increment;
    }
}
