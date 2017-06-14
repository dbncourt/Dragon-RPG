using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float maxHealthPoints;
    [SerializeField]
    private float attackRadius = 4.0f;

    private float currentHealthPoints;
    private GameObject player;
    private AICharacterControl aiCharacterControl;

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    public Enemy()
    {
        maxHealthPoints = 100.0f;
        player = null;
        aiCharacterControl = null;
    }

    void Start()
    {
        currentHealthPoints = maxHealthPoints;

        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, aiCharacterControl.transform.position);
        if (distanceToPlayer <= attackRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }
    }
}
