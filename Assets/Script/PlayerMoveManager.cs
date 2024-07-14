using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMoveManager : MonoBehaviour
{
    public VariableJoystick joystick;
    public Canvas inputCanvas;
    public bool isJoystick;
    public CharacterController controller;
    public float moveSpeed;
    public float rotationSpeed;

    public Animator playerAC;

    private void Start()
    {
        EnableJoystickInput();
    }
    public void EnableJoystickInput()
    {
        isJoystick = true;
        inputCanvas.gameObject.SetActive(true);
    }

    private void Update()
    {


        if (isJoystick)
        {
            var movementDirection = new Vector3(joystick.Direction.x, 0f, joystick.Direction.y);
            controller.SimpleMove(movementDirection * moveSpeed);

            if (movementDirection.sqrMagnitude <= 0f)
            {
                playerAC.SetBool("Run", false);

                return;
            }
            playerAC.SetBool("Run", true);


            var targetDir = Vector3.RotateTowards(controller.transform.forward, movementDirection,
                rotationSpeed * Time.deltaTime, 0f);
            controller.transform.rotation = Quaternion.LookRotation(targetDir);
        }
    }
}
