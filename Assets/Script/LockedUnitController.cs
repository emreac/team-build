using TMPro;
using UnityEngine;

public class LockedUnitController : MonoBehaviour
{



    [Header("Settings")]
    [SerializeField] private int price;
    [SerializeField] private int ID;

    [Header("Objects")]
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameObject lockedUnit;
    [SerializeField] private GameObject unLockedUnit;
    public bool isPurchased;
    private string keyUnit = "keyUnit";
    public float score;


    // Start is called before the first frame update
    void Start()
    {
        priceText.text = price.ToString();
        LoadUnit();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPurchased)
        {
            UnlockUnit();
            TinySauce.OnGameFinished(score);
        }
    }
    //Private
    public void UnlockUnit()
    {
        if (CashManager.instance.TryBuyUnit(price))
        {
            PlayUnlockedSound();
            Unlock();
            SaveUnit();
            score += 10f;
        }
        //Check cash
        //If enough money, activate
    }
    private void Unlock()
    {

        isPurchased = true;
        lockedUnit.SetActive(false);
        unLockedUnit.SetActive(true);
    }

    public void SaveUnit()
    {
        string key = keyUnit + ID.ToString();
        PlayerPrefs.SetString(key, "saved");
    }
    private void LoadUnit()
    {
        string key = keyUnit + ID.ToString();
        string status = PlayerPrefs.GetString(key);

        if (status.Equals("saved"))
        {
            Unlock();
        }

    }
    private void PlayUnlockedSound()
    {
        AudioManager.instance.PlayAudio(AudioClipType.unlockedClip);
    }
}
