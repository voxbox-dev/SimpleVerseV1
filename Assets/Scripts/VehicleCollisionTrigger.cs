using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Simpleverse
{
    public class VehicleCollisionTrigger : MonoBehaviour
    {
        // Assuming 'Vehicles' layer is set to layer 9 as per Spatial's allowed layers
        private const int VehiclesLayer = 9;

        private void OnTriggerEnter(Collider other)
        {
            // Check if the collided object is on the 'Vehicles' layer
            if (other.gameObject.layer == VehiclesLayer)
            {
                StartRace();
            }
        }

        private void StartRace()
        {
            // Your code to start the race
            Debug.Log("Race started!");
        }
    }

}
