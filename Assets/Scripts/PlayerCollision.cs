using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement PlayerMovement;

    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.tag == "Obstacle")
        {
            Time.timeScale = 0.3f;
            PlayerMovement.enabled = false;
        }

    }
  
}
