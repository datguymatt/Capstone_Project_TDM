using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip onHoverSFX;
    public AudioClip onHoverExitSFX;
    public AudioClip onClickSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnHover()
    {
        audioSource.PlayOneShot(onHoverSFX);
    }
}
