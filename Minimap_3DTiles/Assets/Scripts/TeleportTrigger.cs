using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public Transform playerObject; // Assign the player object in the Inspector
    public Teleportation teleportScript; // Assign the Teleportation script in the Inspector

    // Example method to be triggered by a button click or any other event
    public void TriggerTeleport()
    {
        if (teleportScript != null && playerObject != null)
        {
            teleportScript.TeleportToB(playerObject);
        }
    }
}
