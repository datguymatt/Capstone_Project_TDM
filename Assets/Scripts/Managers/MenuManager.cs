using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // instance singleton
    public static MenuManager _instance;

    //audio manager
    public AudioManager audioManager;

    //UI
    public TextMeshProUGUI statusMessage;
    public GameObject pressAnyKeyUI;
    

    //state
    //intro is supposed to be night turning into daytime, starts with that
    public bool isIntro = true;
    //after intro, starts the 'press any key to proceed' sequence, which is day time until a key is pressed, where it transitions to dusk
    public bool anyKeyClicked = false;
    //after clicked, it's dusk

    //state events
    public Action IntroStarted;
    public Action AnyButtonPrompt;
    public Action AnyButtonClicked;
    public Action StartGameClicked;



    void Start()
    {
        if (_instance != null)
        {
            Debug.LogError("Already an instance of " + this.gameObject.name);
        }
        else
        {
            _instance = new MenuManager();
        }
        IntroStarted?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape") && !isIntro)
        {
            ExitGame();
        }
        if (Input.anyKey && !isIntro && !anyKeyClicked)
        {
            anyKeyClicked = true;
            pressAnyKeyUI.SetActive(false);
            
            //queue subscribers to event
            AnyButtonClicked?.Invoke();
            //PLAYSOUND
            audioManager.PlaySFXAudio("vampire-laugh");
            audioManager.PlaySFXAudio("click-ghosts");
        }
    }
    //this is called by the visual sequencer when it's intro is finished
    public void StartAnyButtonPrompt()
    {
        //activate that text
        pressAnyKeyUI.SetActive(true);
        //set state
        isIntro = false;
        //kick off the 'press any key to proceed' visual sequencer
        AnyButtonPrompt?.Invoke();
        
    }

    //start game section
    public void StartGame()
    {
        audioManager.PlaySFXAudio("start-ghosts");
        //queue subscribers to event
        StartGameClicked?.Invoke();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    //Exit game if button 
    public void ExitGame()
    {
        Application.Quit();
    }
}
