using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Reference to the camera
    public GameObject mainCamera;
        void Update()
        {
        // Calculate the position for the Canvas in front of the camera
        Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * 20f;
        targetPosition.y = mainCamera.transform.position.y - 1.0f; // Maintain same height as camera

        // Smoothly move the Canvas towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 20.0f);

        // Make the Canvas always face the camera
        transform.LookAt(mainCamera.transform);
    }
}