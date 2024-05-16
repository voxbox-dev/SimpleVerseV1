using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpatialSys.UnitySDK;

namespace Simpleverse
{
    /// <summary>
    /// Vehicle manager class
    /// This class is responsible for managing the vehicle object in the scene. There should only be one instance of this class in the scene.
    /// The class is responsible for spawning and respawning the vehicle object. 
    /// </summary>
    public class VehicleManager : MonoBehaviour
    {
        [SerializeField] private GameObject vehiclePrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private string spawnToatMessage = "Vehicle spawned";

        [SerializeField] private Transform respawnPoint;
        [SerializeField] private string respawnToastMessage = "Vehicle respawned at Garage";

        private static VehicleManager _instance;


        private GameObject vehicleObject = null;

        private void Awake()
        {
            Debug.Log("Awake called");
            if (_instance != null && _instance != this)
            {
                Debug.Log("Destroying duplicate instance of VehicleManager");
                Destroy(this);
            }
            else
            {
                Debug.Log("Setting instance of VehicleManager");
                _instance = this;
            }
        }

        void OnDestroy()
        {
            Debug.Log("OnDestroy called");
            Destroy(vehicleObject);
        }

        public void Spawn()
        {
            Debug.Log("Spawn called");
            if (vehicleObject == null)
            {
                Debug.Log("Spawning new vehicle");
                vehicleObject = Instantiate(vehiclePrefab, spawnPoint.position, spawnPoint.rotation);
            }
            else
            {
                Debug.Log("Moving existing vehicle to spawn point");
                vehicleObject.transform.position = spawnPoint.position;
                vehicleObject.transform.rotation = spawnPoint.rotation;
                vehicleObject.SetActive(true);
            }
            // show tost message
            SpatialBridge.coreGUIService.DisplayToastMessage(spawnToatMessage, 3.0f);
        }

        public void Respawn(Transform respawnPoint)
        {
            if (respawnPoint == null)
            {
                Debug.LogError("Respawn point is null - reverting to default respawn point");
                respawnPoint = spawnPoint;
                return;
            }
            Debug.Log("Respawn called");
            if (vehicleObject != null)
            {
                Debug.Log("Moving vehicle to respawn point");
                vehicleObject.transform.position = respawnPoint.position;
                // show tost message
                SpatialBridge.coreGUIService.DisplayToastMessage(respawnToastMessage, 3.0f);
            }

        }

    }
}
