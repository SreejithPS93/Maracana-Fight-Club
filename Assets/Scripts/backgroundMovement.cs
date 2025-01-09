using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class backgroundMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public GameObject bg;
    private Vector3 initialOffset;
    void Start()
    {
        initialOffset = transform.position + player.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        /*try
        {
          

        }
        catch { }*/
        Vector3 newBackgroundPosition = -player.position + initialOffset;

        // Restrict movement to the horizontal axis
        newBackgroundPosition.y = transform.position.y; // Keep the Y position fixed
        newBackgroundPosition.z = transform.position.z; // Keep the Z position fixed

        transform.position = newBackgroundPosition;
    }
}
