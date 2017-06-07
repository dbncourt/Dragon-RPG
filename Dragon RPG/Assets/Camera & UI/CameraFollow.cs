using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.LogError("No player to follow was found");
        }
    }

    void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}