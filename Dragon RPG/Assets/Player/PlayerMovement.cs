using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkMoveStopRadius;

    ThirdPersonCharacter m_Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
    Vector3 m_CamForward;

    private bool isInDirectMode;

    public PlayerMovement()
    {
        isInDirectMode = false;
    }

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
        m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isInDirectMode = !isInDirectMode;
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
        Vector3  m_Move = v * m_CamForward + h * Camera.main.transform.right;

        m_Character.Move(m_Move, false, false);
    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            switch (cameraRaycaster.layerHit)
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
        m_Character.Move(vectorMovement, false, false);
    }
}

