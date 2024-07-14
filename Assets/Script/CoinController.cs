using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int coinPrice;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CashManager.instance.AddCoin(coinPrice);
            PlayGrabSound();
            Destroy(gameObject);
        }
    }
    private void PlayGrabSound()
    {
        AudioManager.instance.PlayAudio(AudioClipType.grabClip);

    }
}
