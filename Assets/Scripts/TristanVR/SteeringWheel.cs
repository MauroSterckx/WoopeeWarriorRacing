using UnityEngine;
using UnityEngine.Events;

public class SteeringWheel : MonoBehaviour
{
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private OVRHand rightHand;
    [SerializeField] private OVRSkeleton rightHandSkeleton;

    public UnityEvent<float> OnWheelRotated;

    private float currentAngle = 0.0f;
    private bool isSelected = false;

    private void Update()
    {
        // Check if the right hand is making a pinch gesture
        if (rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            if (!isSelected)
            {
                OnSelectEntered();
            }
        }
        else
        {
            if (isSelected)
            {
                OnSelectExited();
            }
        }

        if (isSelected)
        {
            RotateWheel();
        }
    }

    private void OnSelectEntered()
    {
        isSelected = true;
        currentAngle = FindWheelAngle();
    }

    private void OnSelectExited()
    {
        isSelected = false;
        currentAngle = FindWheelAngle();
    }

    private void RotateWheel()
    {
        // Convert that direction to an angle, then rotation
        float totalAngle = FindWheelAngle();

        // Apply difference in angle to wheel
        float angleDifference = currentAngle - totalAngle;
        wheelTransform.Rotate(transform.forward, -angleDifference, Space.World);

        // Store angle for next process
        currentAngle = totalAngle;
        OnWheelRotated?.Invoke(angleDifference);
    }

    private float FindWheelAngle()
    {
        // Find the position of the index finger tip
        var indexTip = rightHandSkeleton.Bones[(int)OVRSkeleton.BoneId.Hand_IndexTip].Transform.position;
        Vector2 direction = FindLocalPoint(indexTip);
        return ConvertToAngle(direction);
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        // Convert the hand position to local space
        return transform.InverseTransformPoint(position).normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        // Use a consistent up direction to find the angle
        return Vector2.SignedAngle(Vector2.up, direction);
    }
}
