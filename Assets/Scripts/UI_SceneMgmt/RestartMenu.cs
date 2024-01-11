using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Events;

public class RestartMenu : MonoBehaviour
{
    public static RestartMenu Instance;

    public GameObject restartMenu;
    public GameObject mainCanvas;
    private GameObject _player;
    public AudioMixer audioMixer;

    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) { Instance = this; }
        //initialize player stuff
        _player = GameObject.Find("Player");
        //sub to events it will need to hear
        _player.GetComponent<Player>().PlayerDead += ActivateRestartMenu;
        RoundManager.GameOverEvent += ActivateRestartMenu;
    }

    private void OnDestroy()
    {
        RoundManager.GameOverEvent -= ActivateRestartMenu;
    }

    public void ActivateRestartMenu()
    {
        GetComponent<AudioSource>().Play();
        mainCanvas.SetActive(false);
        restartMenu.SetActive(true);

        audioMixer.DOKill();
        audioMixer.SetFloat("masterFilterFreq", 140);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        //time scale stop

        //play sfx + music

        //some kind of background fade

        //animate the lettering?
    }

    public void RestartGame()
    {
        mainCanvas.SetActive(true);
        restartMenu.SetActive(false);
        Time.timeScale = 1.0f;
        audioMixer.DOSetFloat("masterFilterFreq", 22000, 2f);
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        //restart the scene
        //destroy objects
        SceneManager.UnloadSceneAsync(1);
        SceneManager.LoadScene(1);
    }

}
