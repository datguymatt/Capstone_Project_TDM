using UnityEngine;

public class FootstepAudioSystem : MonoBehaviour
{
    public AudioSource walkRunAudioSource;
    public AudioClip[] walkRunAudioClips;
    public AudioClip[] jumpClips;
    public AudioClip[] landClips;

    public float walkSpeed;

    private void Awake()
    {

    }
}
