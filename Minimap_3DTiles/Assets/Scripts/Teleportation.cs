using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform pointB; // Assign the GameObject of location B in the Inspector

    /*    void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) // Assuming the player has the tag "Player"
            {
                Debug.Log("enter");
                TeleportToB(other.transform);

            }
        }*/

    public void TeleportToB(Transform objectToTeleport)
    {
        if (pointB != null && objectToTeleport != null)
        {
            Debug.Log("Teleporting player to pointB");
            objectToTeleport.position = pointB.position;
        }
        else
        {
            Debug.LogWarning("Destination or object to teleport is null.");
        }
    }
}
