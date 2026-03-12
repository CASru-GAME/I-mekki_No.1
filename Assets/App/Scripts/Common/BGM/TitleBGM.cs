using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }
    public void Play()
    {
        audioSource.Play();
    }
}
