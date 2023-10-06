using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class tvtesteron : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public MeshRenderer tvRenderer;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.E))
        {
            tvRenderer.enabled = true;
            audioSource.Play();
            videoPlayer.Play();
        }
    }
}
