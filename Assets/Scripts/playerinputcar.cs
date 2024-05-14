using UnityEngine;

public class playerinputcar : MonoBehaviour
{
    public float Acceleration
    {
        get { return m_Acceleration; }
    }

    public float Steering
    {
        get { return m_Steering; }
    }

    private float m_Acceleration;
    private float m_Steering;

    private bool accelerating = false;
    private bool breaking = false;
    private bool turningLeft = false;
    private bool turningRight = false;

    void Update()
    {
        GetPlayerInput();

        if (accelerating)
        {
            m_Acceleration = 1f;
            // Extra functionaliteit indien nodig
        }
        else if (breaking)
        {
            m_Acceleration = -0.5f;
            // Extra functionaliteit indien nodig
        }
        else
        {
            m_Acceleration = 0f;
            // Extra functionaliteit indien nodig
        }

        if (turningLeft)
        {
            m_Steering = -1f;
            // Extra functionaliteit indien nodig
        }
        else if (!turningLeft && turningRight)
        {
            m_Steering = 1f;
            // Extra functionaliteit indien nodig
        }
        else
        {
            m_Steering = 0f;
            // Extra functionaliteit indien nodig
        }
    }

    private void GetPlayerInput()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            accelerating = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            accelerating = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
        {
            breaking = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickDown, OVRInput.Controller.RTouch))
        {
            breaking = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch))
        {
            turningLeft = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch))
        {
            turningLeft = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch))
        {
            turningRight = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch))
        {
            turningRight = false;
        }
    }
}
