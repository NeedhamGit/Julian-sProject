using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().resetPlayer();
            Destroy(gameObject);
        }
    }
}
