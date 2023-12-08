using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int bloodVials;

    public float healthMod;
    public float movementSpeedMod;
    public float damageMod;
    public float reloadSpeedMod;
    public int shotMultiplier;

    public int[] levels = new int[5];

    private void Start()
    {
        if(Instance != null) { Destroy(this); }
        else { Instance = this; }

        GetStats();
    }

    public void GetStats()
    {
        healthMod = 0.3f * levels[0];
        movementSpeedMod = 0.3f * levels[1];
        damageMod = 0.3f * levels[2];
        reloadSpeedMod = 0.3f * levels[3];
        shotMultiplier = 1 * levels[4];
    }

    public void LevelUp(int index)
    {
        levels[index] += 1;
    }
}
