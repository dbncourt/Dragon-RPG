using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float walkMoveStopRadius;
    [SerializeField]
    private float attackMoveStopRadius;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination;
    Vector3 cameraForward;
    Vector3 clickPoint;

    private bool isInDirectMode;

    public PlayerMovement()
    {
        walkMoveStopRadius = 0.2f;
        attackMoveStopRadius = 5.0f;
        isInDirectMode = false;
    }

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
        cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isInDirectMode = !isInDirectMode;
            currentDestination = transform.position;
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

    private void WalkToDestination()
    {
        Vector3 playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude >= 0.0f)
        {
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
        
    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.hit.point;
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    {
                        currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
                        break;
                    }
                case Layer.Enemy:
                    {
                        currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
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

        WalkToDestination();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, clickPoint);
        Gizmos.DrawSphere(currentDestination, 0.15f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(clickPoint, 0.1f);

        Gizmos.color = new Color(255f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }

    private Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }
}

