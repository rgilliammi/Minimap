using UnityEngine;

public class RenderToTexture : MonoBehaviour
{
    public Camera renderCamera;

    void Start()
    {
        // Deactivate the camera rendering initially
        renderCamera.enabled = false;
    }

    void Update()
    {
       
            // Toggle camera rendering on Space key press
            renderCamera.enabled = !renderCamera.enabled;
        
    }
}
