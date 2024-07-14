using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    //public LockedUnitController lockedUnitController;
    public CinemachineVirtualCamera mainCamera;
    public CinemachineVirtualCamera actionCamera;
    public CinemachineVirtualCamera tutorCamera;
    public CinemachineVirtualCamera ballAreaCamera;

    private void Start()
    {
        // Ensure mainCamera is active initially
        mainCamera.gameObject.SetActive(true);
        StartTutorialCamera();
        actionCamera.gameObject.SetActive(false);
        actionCamera.gameObject.SetActive(false);
        ballAreaCamera.gameObject.SetActive(false);
    }

    public void PerformCameraAction()
    {

        // Switch to actionCamera
        StartCoroutine(SwitchCamerasAndWait());
    }

    public void StartTutorialCamera()
    {
        if (PlayerPrefs.GetInt("FunctionExecuted", 0) == 0)
        {
            // The function has not been executed before, so run it
            StartCoroutine(SwitchCamerasAndWaitStart());

            // Set the flag to indicate that the function has been executed
            PlayerPrefs.SetInt("FunctionExecuted", 1);
            PlayerPrefs.Save(); // Save the flag
        }


    }

    public void BallAreaCamera()
    {
        StartCoroutine(SwitchCamerasAndWaitBallArea());
    }
    private IEnumerator SwitchCamerasAndWait()
    {
        yield return new WaitForSeconds(0.5f);
        // Activate actionCamera and deactivate mainCamera
        mainCamera.gameObject.SetActive(false);
        actionCamera.gameObject.SetActive(true);

        // Wait for 1 second
        yield return new WaitForSeconds(3.0f);

        // Switch back to mainCamera
        actionCamera.gameObject.SetActive(false);

        mainCamera.gameObject.SetActive(true);
    }

    private IEnumerator SwitchCamerasAndWaitStart()
    {
        yield return new WaitForSeconds(0.5f);
        // Activate actionCamera and deactivate mainCamera
        mainCamera.gameObject.SetActive(false);
        tutorCamera.gameObject.SetActive(true);

        // Wait for 1 second
        yield return new WaitForSeconds(3.0f);

        // Switch back to mainCamera
        tutorCamera.gameObject.SetActive(false);

        mainCamera.gameObject.SetActive(true);
    }

    public IEnumerator SwitchCamerasAndWaitBallArea()
    {
        yield return new WaitForSeconds(0.5f);
        // Activate actionCamera and deactivate mainCamera
        mainCamera.gameObject.SetActive(false);
        ballAreaCamera.gameObject.SetActive(true);

        // Wait for 1 second
        yield return new WaitForSeconds(3.0f);

        // Switch back to mainCamera
        ballAreaCamera.gameObject.SetActive(false);

        mainCamera.gameObject.SetActive(true);
    }


}
