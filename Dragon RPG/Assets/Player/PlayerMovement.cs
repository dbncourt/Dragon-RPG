using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkMoveStopRadius;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
    Vector3 cameraForward;

    private bool isInDirectMode;

    public PlayerMovement()
    {
        isInDirectMode = false;
    }

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isInDirectMode = !isInDirectMode;
            currentClickTarget = transform.position;
        }

        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }

    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3  movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    {
                        currentClickTarget = cameraRaycaster.hit.point;
                        break;
                    }
                case Layer.Enemy:
                    {
                        print("Not moving to enemy");
                        break;
                    }
                case Layer.RaycastEndStop:
                    break;
                default:
                    {
                        print("SHOULDN'T BE HERE");
                        break;
                    }
            }
        }

        Vector3 playerToClickPoint = currentClickTarget - transform.position;
        Vector3 vectorMovement;

        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {
            vectorMovement = currentClickTarget - transform.position;
        }
        else
        {
            vectorMovement = Vector3.zero;
        }
        thirdPersonCharacter.Move(vectorMovement, false, false);
    }
}

