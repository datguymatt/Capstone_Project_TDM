using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewUIManager : MonoBehaviour
{
    public bool isPaused = false;
    public static NewUIManager Instance;
    public Player player;
    [SerializeField] private Slider healthbar;

    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas settingsCanvas;
    
    private void Start()
    {
        if(Instance == null) { Instance = this; }

        player = GameObject.Find("Player").GetComponent<Player>();

        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthbar.value = player.playerHealth.GetHealth() / player.playerHealth.GetMaxHealth();
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
