using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera firstCamera;
    public Camera secondCamera;

    void Start()
    {
        // Activate the default VR camera and deactivate the others
        firstCamera.enabled = true;
        secondCamera.enabled = false;
    }

    void Update()
    {
        // Example: Switch cameras on a key press (for demonstration purposes)
        if (Input.GetKeyDown(KeyCode.C))
        {
            firstCamera.enabled = !firstCamera.enabled;
            secondCamera.enabled = !secondCamera.enabled;
        }
    }
}
