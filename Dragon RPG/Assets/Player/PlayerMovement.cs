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

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            print("Cursor raycast hit" + cameraRaycaster.hit.collider.gameObject.name.ToString());
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
        } else
        {
            vectorMovement = Vector3.zero;
        }
        m_Character.Move(vectorMovement, false, false);
    }
}
