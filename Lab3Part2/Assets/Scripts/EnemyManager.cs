using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public static UnityEvent changeDirectionLeft;
    public static UnityEvent changeDirectionRight;

    // Start is called before the first frame update
    void Start()
    {
        if (changeDirectionLeft == null)
        {
            changeDirectionLeft = new UnityEvent();
        }

        if (changeDirectionRight == null)
        {
            changeDirectionRight = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
