using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;

public class PauseMenuManager : MonoBehaviour
{
    public bool isPaused = false;
    public static PauseMenuManager Instance;
    public AudioMixer audioMixer;

    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private Canvas audioSettingsCanvas;
    
    private void Start()
    {
        if(Instance == null) { Instance = this; }
    }

    public void PauseGame()
    {
        //audioMixer.DOSetFloat("masterFilterFreq", 250, 0.1f);
        audioMixer.DOKill();
        audioMixer.SetFloat("masterFilterFreq", 250);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        pauseCanvas.gameObject.SetActive(true);
        isPaused = true;
        
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        audioMixer.DOSetFloat("masterFilterFreq", 22000, 2f);
        Cursor.lockState = CursorLockMode.Locked;
        pauseCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
        audioSettingsCanvas.gameObject.SetActive(false);
        isPaused = false;
    }

    public void OpenSettings()
    {
        settingsCanvas.gameObject.SetActive(true);
        pauseCanvas.gameObject.SetActive(false);
    }

    public void CloseSettings()
    {
        pauseCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
    }

    public void OpenAudioSettings()
    {
        audioMixer.SetFloat("masterFilterFreq", 22000);
        settingsCanvas.gameObject.SetActive(false);
        audioSettingsCanvas.gameObject.SetActive(true);
    }

    public void CloseAudioSettings()
    {
        audioMixer.SetFloat("masterFilterFreq", 250);
        audioSettingsCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
