using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float maxHealthPoints;
    private float currentHealthPoints;

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
    }

    void Start()
    {
        currentHealthPoints = maxHealthPoints;
    }

    void Update()
    {

    }
}
