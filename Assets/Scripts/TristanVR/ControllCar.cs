// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class ControllCar : MonoBehaviour
// {
//     private PlayerInput inputmanager;
//     public List<WheelCollider> throttleWheels;
//     public List<WheelCollider> steeringWheels;
//     public float strengthCoefficient = 200000f
//     public float maxTurn = 20f;

//     void Start()
//     {
//         inputmanager = GetComponent<PlayerInput>();
//     }

//     void FixedUpdate()
//     {
//         foreach (WheelCollider wheel in throttleWheels)
//         {
//             wheel.motorTorque = strengthCoefficient * Time.deltaTime * inputmanager.Acceleration;
//             wheel.wheelDampingRate = inputmanager.wheelDamping;
//         }

//         foreach (WheelCollider wheel in steeringWheels)
//         {
//             wheel.steerAngle = maxTurn * inputmanager.Steering;
//             wheel.wheelDampingRate = inputmanager.wheelDamping
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllCar : MonoBehaviour
{
    private PlayerInput inputManager;
    public List<WheelCollider> throttleWheels;
    public List<WheelCollider> steeringWheels;
    public float strengthCoefficient = 200000f;
    public float maxTurn = 20f;

    void Start()
    {
        inputManager = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        // Assuming inputManager has been properly set up with the corresponding input actions
        float acceleration = inputManager.actions["Acceleration"].ReadValue<float>();
        float steering = inputManager.actions["Steering"].ReadValue<float>();
        float wheelDamping = inputManager.actions["WheelDamping"].ReadValue<float>();

        foreach (WheelCollider wheel in throttleWheels)
        {
            wheel.motorTorque = strengthCoefficient * acceleration * Time.deltaTime;
            wheel.wheelDampingRate = wheelDamping;
        }

        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxTurn * steering;
            wheel.wheelDampingRate = wheelDamping;
        }
    }
}
