using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class MenuVisualSequencer : MonoBehaviour
{

    public MenuManager _menuManager;
    public Camera mainCamera;
    public TextMeshProUGUI _titleText;
    public Transform _titleTransform;

    //moon intensity
    public Light moonLight;
    public Material moonMaterial;
    public Color moonfadedColor;
    //Daynight
    public MenuDayNightController _menuDayNightController;

    //ui elements
    public GameObject afterClickUI;


    [Header("Credit Intro Variables")]
    public Transform tdmCredit;
    public Transform nameCredit;



    [Header("Start Intro Sequence Variables")]
    public float introSeqDuration;

    [Header("Any Button Sequence Variables")]
    public float anyButtonClickedSeqDuration;

    [Header("Start Game Sequence Variables")]
    public float startGameSeqDuration;

    void Awake()
    {
        //subscribe to MenuManager Events
        _menuManager.IntroStarted += StartIntro;
        _menuManager.AnyButtonPrompt += AnyButtonPrompt;
        _menuManager.AnyButtonClicked += AnyButtonClicked;
        _menuManager.StartGameClicked += StartGameIntro;
        moonMaterial.color = Color.white;

    }

    //intro
    public void StartIntro()
    {
        StartCoroutine(StartIntroSeq());
    }

    private IEnumerator StartIntroSeq()
    {
        //start the visual sequence
        //1- lighting + Camera start to move down and rotate
        //camera start - Vector3(1438, 729, 119.400002)
        //camera end - Vector3(49,9,94)
        mainCamera.transform.DOMove(new Vector3(49, 9, 94), introSeqDuration).SetEase(Ease.InOutSine);
        _menuDayNightController.SwitchToDayTime(introSeqDuration);
        //2-credit zoom in
        tdmCredit.DOScale(new Vector3(4f, 4f, 4f), 3).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(3);
        tdmCredit.DOScale(new Vector3(0, 0, 0), 2).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(2);
        moonLight.DOIntensity(1.1f, 3f);
        moonMaterial.DOColor(moonfadedColor, 7f);

        nameCredit.DOScale(new Vector3(1.42999995f, 1.42999995f, 1.42999995f), 3).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(3);
        nameCredit.DOScale(new Vector3(0, 0, 0), 2).SetEase(Ease.InQuart);
        yield return new WaitForSeconds(2);
        //3-acctivate title, zoom in Vector3(1.78999984,1.78999984,1.78999984)
        _titleTransform.DOScale(new Vector3(1.78999984f, 1.78999984f, 1.78999984f), 10).SetEase(Ease.InOutSine);

        Debug.Log("Intro will play for " + introSeqDuration.ToString() + " seconds");
        yield return new WaitForSeconds(10f);
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
        _menuDayNightController.SwitchToDuskTime(10f);
    }
    //end anybutton

    //start game
    public void StartGameIntro()
    {
        StartCoroutine(StartGameSeq());
    }   

    private IEnumerator StartGameSeq()
    {
        _menuDayNightController.SwitchToNightTime(startGameSeqDuration);
        yield return new WaitForSeconds(startGameSeqDuration);
        _menuManager.LoadGame();
    }

}
