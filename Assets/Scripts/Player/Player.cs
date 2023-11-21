
using UnityEngine;

public class Player : MonoBehaviour
{
    //audio
    public AudioManager audioManager;


    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
}
