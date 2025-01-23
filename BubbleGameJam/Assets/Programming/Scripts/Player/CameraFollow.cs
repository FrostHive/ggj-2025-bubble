using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    [SerializeField] private Vector3 offset; // Offset between the camera and player
    [SerializeField] private float smoothSpeed = 0.125f; // Smooth damping for camera movement

    private Vector3 velocity = Vector3.zero; // Reference velocity for SmoothDamp

    private void LateUpdate()
    {
        if (playerTransform == null) return;

        // Calculate the target position with offset
        Vector3 targetPosition = playerTransform.position + offset;

        //// Smoothly move the camera towards the target position
        //transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }
}
