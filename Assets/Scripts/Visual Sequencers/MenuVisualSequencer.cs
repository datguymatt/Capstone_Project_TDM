using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MenuVisualSequencer : MonoBehaviour
{

    public MenuManager _menuManager;

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

    
    public void StartIntro()
    {
        StartCoroutine(StartIntroSeq());
    }
    public void AnyButtonPrompt()
    {
        StartCoroutine(AnyButtonPromptSeq());
    }

    public void AnyButtonClicked()
    {
        StartCoroutine(AnyButtonClickedSeq());
    }

    public void StartGameIntro()
    {
        StartCoroutine(StartIntroSeq());
    }

    //timed sequences
    private IEnumerator StartIntroSeq()
    {
        yield return new WaitForSeconds(introSeqDuration);
        //when finished - call the menu manager that we are ready
        _menuManager.StartAnyButtonPrompt();
    }

    private IEnumerator AnyButtonPromptSeq()
    {
        yield return new WaitForSeconds(introSeqDuration);
    }

    private IEnumerator AnyButtonClickedSeq()
    {
        yield return new WaitForSeconds(anyButtonClickedSeqDuration);
    }

    private IEnumerator StartGameSeq()
    {
        yield return new WaitForSeconds(startGameSeqDuration);
    }

}
