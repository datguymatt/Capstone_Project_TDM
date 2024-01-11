using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Tvtesteron : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public MeshRenderer tvRenderer;
    public AudioSource audioSource;

    public bool isOn;
    public bool isFirstRound = true;
    // Start is called before the first frame update
    void Awake()
    {
        RoundManager.NightStart += NightStart;
    }

    private void OnDestroy()
    {
        RoundManager.NightStart -= NightStart;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.E) && isOn)
        {
            audioSource.Stop();
            videoPlayer.Stop();
            tvRenderer.enabled = false;
            isOn = false;
        }
       else if (Input.GetKeyDown(KeyCode.E) && !isOn)
        {
            audioSource.Play();
            videoPlayer.Play();
            tvRenderer.enabled = true;
            isOn = true;
        }
    }

    public void NightStart()
    {
        StartCoroutine(TvScare());
    }
    public IEnumerator TvScare()
    {
        if (isFirstRound)
        {
            yield return new WaitForSeconds(2);
            tvRenderer.enabled = true;
            audioSource.Play();
            videoPlayer.Play();
            isOn = true;
            isFirstRound = false;
        }
    }
}
