using UnityEngine;


public enum AudioClipType { grabClip, shopClip, unlockedClip }
public class AudioManager : MonoBehaviour
{
    //Singleton
    public static AudioManager instance;
    [SerializeField] private AudioSource audioSource;
    public AudioClip grabClip, ShopClip, unlockedClip;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayAudio(AudioClipType clipType)
    {
        if (audioSource != null)
        {
            AudioClip audioClip = null;
            if (clipType == AudioClipType.grabClip)
            {
                audioClip = grabClip;
            }
            else if (clipType == AudioClipType.shopClip)
            {
                audioClip = ShopClip;
            }
            else if (clipType == AudioClipType.unlockedClip)
            {
                audioClip = unlockedClip;
            }
            audioSource.PlayOneShot(audioClip, 0.6f);
        }
    }
    public void StopBackgroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }
}
