using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlatforms : MonoBehaviour
{
    public static int playerColor = 1;

    private int blueLayer;
    private int redLayer;

    private Collider platformCollider;

    void Start()
    {
        platformCollider = GetComponent<Collider>();
        //set layer variables
        blueLayer = LayerMask.NameToLayer("midground-blue");
        redLayer = LayerMask.NameToLayer("midground-red");
        //debugging (couldnt tell you how long i sat here with a miss spelled layer)
        if (blueLayer == -1 || redLayer == -1)
        {
            Debug.LogError("stuff is here");
        }
        UpdatePlatformLayer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerColor = playerColor * -1;
        }
        UpdatePlatformLayer();
    }

    void UpdatePlatformLayer()
    {
        if (playerColor == 1)
        {
            gameObject.layer = redLayer;
            platformCollider.isTrigger = true;
        }
        else if (playerColor == -1)
        {
            gameObject.layer = blueLayer;
            platformCollider.isTrigger = false;
        }
    }
}
