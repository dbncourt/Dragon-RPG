using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    public Player()
    {
        maxHealthPoints = 100.0f;
    }

    // Use this for initialization
    void Start()
    {
        currentHealthPoints = maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
