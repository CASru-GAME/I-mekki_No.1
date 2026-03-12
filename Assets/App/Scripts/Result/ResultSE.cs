using UnityEngine;

public class ResultSE : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip clearSE;
    [SerializeField] private AudioClip failSE;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(App.Common._Data._PlayerStatistics.GameCleared)
        {
            audioSource.PlayOneShot(clearSE);
        }
        else
        {
            audioSource.PlayOneShot(failSE);
        }
    }
}
