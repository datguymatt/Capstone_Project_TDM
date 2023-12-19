using System.Collections;
using UnityEngine;

public class FootstepAudioSystem : MonoBehaviour
{
    public FootstepAudioSystem _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("Already an instance of " + this.gameObject.name);
        }
        else
        {
            _instance = new FootstepAudioSystem();
        }
    }

    public AudioSource leftRightFootAudioSource;

    public AudioClip[] leftFootstepConc;
    public AudioClip[] rightFootstepConc;
    public AudioClip[] leftFootstepGrass;
    public AudioClip[] rightFootstepGrass;

    public string groundType;
    private string footType = "left";

    public float walkSpeed = 0.3f;
    public float sprintMultiply = 1.2f;

    public bool isWalking = false;
    public bool isRunning = false;

    public LayerMask grassMask;
    public LayerMask concreteMask;
    public Transform groundCheck;
    private float groundCheckDistance = 0.4f;
    public void WalkSoundStart()
    {
        GroundMaterialCheck();
        if(!leftRightFootAudioSource.isPlaying)
        {
            StartCoroutine(WalkSound());
        }
    }

    public void WalkSoundStop()
    {
        StopCoroutine(WalkSound());
    }

    public IEnumerator WalkSound()
    {
        while(isWalking || isRunning)
        {
            if (isWalking)
            {
                leftRightFootAudioSource.PlayOneShot(GetRandomClip());
                yield return new WaitForSeconds(walkSpeed);
            }
            else if (isRunning)
            {
                leftRightFootAudioSource.PlayOneShot(GetRandomClip());
                yield return new WaitForSeconds(walkSpeed * sprintMultiply);
            }
            GroundMaterialCheck();
        }
        yield return null;
    }

    public AudioClip GetRandomClip()
    {
        switch(footType)
        {
            case "left":
                footType = "right";
                if (groundType == "concrete")
                {
                    return leftFootstepConc[UnityEngine.Random.Range(0, leftFootstepConc.Length)];

                } else if(groundType == "grass")
                {
                    return leftFootstepGrass[UnityEngine.Random.Range(0, leftFootstepGrass.Length)];
                } else
                {
                    Debug.LogError("no ground material type found for this audio clip");
                    return null;
                }
                
            case "right":
                footType = "left";
                if (groundType == "concrete")
                {
                    return rightFootstepConc[UnityEngine.Random.Range(0, rightFootstepConc.Length)];
                } else if (groundType == "grass")
                {
                    return rightFootstepGrass[UnityEngine.Random.Range(0, leftFootstepGrass.Length)];
                } else
                {
                    Debug.LogError("no ground material type found for this audio clip");
                    return null;
                }
                
        }
        return null;
    }

    public void GroundMaterialCheck()
    {
        if(Physics.CheckSphere(groundCheck.position, groundCheckDistance, grassMask))
        {
            groundType = "grass";
            if(Physics.CheckSphere(groundCheck.position, groundCheckDistance, concreteMask))
            {
                groundType = "concrete";
            }
        }
        else if(Physics.CheckSphere(groundCheck.position, groundCheckDistance, concreteMask))
        {
            groundType = "concrete";
        }
        else
        {
            groundType = "concrete";
        }
    }




}
