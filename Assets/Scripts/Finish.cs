using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    // This method is called when the collider attached to this object enters a collision
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Finish"
        if (collision.gameObject.CompareTag("Finish"))
        {
            // Load the scene named "endscene"
            SceneManager.LoadScene("endscene");
        }
    }

    // Alternatively, you can use OnTriggerEnter if the wall object has a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene("endScene");
        }
    }
}


