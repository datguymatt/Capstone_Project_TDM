using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    public bool isPaused = false;
    public static PauseMenuManager Instance;

    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private Canvas audioSettingsCanvas;
    
    private void Start()
    {
        if(Instance == null) { Instance = this; }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        pauseCanvas.gameObject.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
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
        settingsCanvas.gameObject.SetActive(false);
        audioSettingsCanvas.gameObject.SetActive(true);
    }

    public void CloseAudioSettings()
    {
        audioSettingsCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
