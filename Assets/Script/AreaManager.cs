using System.Collections;
using UnityEngine;

public class AreaManager : MonoBehaviour
{

    public CameraSwitcher cameraSwitcher;

    //Sorted Areas
    [SerializeField] private GameObject enegryArea;
    [SerializeField] private GameObject wingArea;
    [SerializeField] private GameObject wings;
    [SerializeField] private GameObject ballArea;
    private bool isUserCompleteLevel;

    //Supporters
    public GameObject support1;
    public GameObject support2;

    public LockedUnitController lockedUnitController;
    // Start is called before the first frame update
    void Start()
    {

        LoadStateEnergy();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalKeeperArea"))
        {

            if (lockedUnitController.isPurchased)
            {

                cameraSwitcher.PerformCameraAction();
                StartCoroutine(ActivateEneryArea());
                other.enabled = false;
            }
        }
        if (other.CompareTag("EnergyDrinkArea"))
        {
            StartCoroutine(ActivateWingArea());

        }
        if (other.CompareTag("WingArea"))
        {

            if (lockedUnitController.isPurchased)
            {
                wings.SetActive(true);
                cameraSwitcher.PerformCameraAction();
                support2.SetActive(true);
                ballArea.SetActive(true);
                cameraSwitcher.BallAreaCamera();
                TinySauce.OnGameFinished(isUserCompleteLevel, lockedUnitController.score, "AllAreasCompleted");
            }
        }


    }
    IEnumerator ActivateEneryArea()
    {
        yield return new WaitForSeconds(2f);
        enegryArea.SetActive(true);
        support1.SetActive(true);

        PlayerPrefs.SetInt("EnergyAreaActive", 1); // Save state as 1 (true)

        lockedUnitController.SaveUnit();
    }

    IEnumerator ActivateWingArea()
    {
        yield return new WaitForSeconds(0.5f);
        wingArea.SetActive(true);


        PlayerPrefs.SetInt("WingAreaActive", 1); // Save state as 1 (true)

        lockedUnitController.SaveUnit();
    }

    void LoadStateEnergy()
    {
        int energyAreaActive = PlayerPrefs.GetInt("EnergyAreaActive", 0); // Default to 0 (false)

        if (energyAreaActive == 1)
        {
            enegryArea.SetActive(true);

        }
        else
        {
            enegryArea.SetActive(false);

        }

    }
    void LoadStateWing()
    {
        int wingAreaActive = PlayerPrefs.GetInt("WingAreaActive", 0); // Default to 0 (false)

        if (wingAreaActive == 1)
        {
            wingArea.SetActive(true);

        }
        else
        {
            wingArea.SetActive(false);

        }

    }

    public void PlayUnlockedSound()
    {
        AudioManager.instance.PlayAudio(AudioClipType.unlockedClip);
    }
}
