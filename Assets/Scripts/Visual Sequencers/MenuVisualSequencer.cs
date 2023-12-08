using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
public class MenuVisualSequencer : MonoBehaviour
{

    public MenuManager _menuManager;
    public Camera _camera;
    public TextMeshProUGUI _titleText;

    //ui elements
    public GameObject afterClickUI;

    [Header("Start Intro Sequence variables")]
    public float introSeqDuration;

    [Header("Any Button Sequence variables")]
    public float anyButtonClickedSeqDuration;

    [Header("Start Game Sequence variables")]
    public float startGameSeqDuration;

    void Awake()
    {
        //subscribe to MenuManager Events
        _menuManager.IntroStarted += StartIntro;
        _menuManager.AnyButtonPrompt += AnyButtonPrompt;
        _menuManager.AnyButtonClicked += AnyButtonClicked;
        _menuManager.StartGameClicked += StartGameIntro;
    }

    //intro
    public void StartIntro()
    {
        StartCoroutine(StartIntroSeq());
    }

    private IEnumerator StartIntroSeq()
    {
        Debug.Log("Intro will play for " + introSeqDuration.ToString() + " seconds");
        yield return new WaitForSeconds(introSeqDuration);
        //when finished - call the menu manager that we are ready
        Debug.Log("intro is finished");
        _menuManager.StartAnyButtonPrompt();
    }

    //endintro

    //any button 
    public void AnyButtonPrompt()
    {
        //start light cycle animation - transition to dusk
   
    }
    public void AnyButtonClicked()
    {
        StartCoroutine(AnyButtonClickedSeq());
    }

    private IEnumerator AnyButtonClickedSeq()
    {
        yield return new WaitForSeconds(anyButtonClickedSeqDuration);
        afterClickUI.SetActive(true);
    }
    //end anybutton

    //start game
    public void StartGameIntro()
    {
        StartCoroutine(StartIntroSeq());
    }   

    private IEnumerator StartGameSeq()
    {
        yield return new WaitForSeconds(startGameSeqDuration);
    }

}
