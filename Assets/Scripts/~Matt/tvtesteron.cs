using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Tvtesteron : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public MeshRenderer tvRenderer;
    public AudioSource audioSource;
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
       if(Input.GetKeyDown(KeyCode.E))
        {
            audioSource.Stop();
            videoPlayer.Stop();
            tvRenderer.enabled = false;
        }
    }

    public void NightStart()
    {
        StartCoroutine(TvScare());
    }
    public IEnumerator TvScare()
    {
        yield return new WaitForSeconds(2);
        tvRenderer.enabled = true;
        audioSource.Play();
        videoPlayer.Play();
        
    }
}
