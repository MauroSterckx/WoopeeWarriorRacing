using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class CarAgent : Agent
{
    public Transform targetWaypoint;
    public WaypointContainer waypointContainer;
    public float speed = 10f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (waypointContainer == null)
        {
            Debug.LogError("WaypointContainer is not assigned in the inspector.");
        }
    }

    public override void OnEpisodeBegin()
    {
        // Reset car position and velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.localPosition = new Vector3(Random.Range(-1f, 1f), 0.5f, Random.Range(-1f, 1f));

        // Reset target waypoint
        SetRandomWaypoint();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add the car's position and velocity as observations
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.z);

        // Add the target waypoint's position as an observation
        if (targetWaypoint != null)
        {
            Vector3 toWaypoint = targetWaypoint.localPosition - transform.localPosition;
            sensor.AddObservation(toWaypoint);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Convert actions to control signals
        float forwardAmount = actions.ContinuousActions[0];
        float turnAmount = actions.ContinuousActions[1];

        // Apply movement
        Vector3 forward = transform.forward * forwardAmount * speed * Time.fixedDeltaTime;
        Vector3 turn = transform.up * turnAmount * 100f * Time.fixedDeltaTime;
        rb.AddForce(forward, ForceMode.VelocityChange);
        rb.AddTorque(turn, ForceMode.VelocityChange);

        // Reward for getting closer to the waypoint
        if (targetWaypoint != null)
        {
            float distanceToWaypoint = Vector3.Distance(transform.localPosition, targetWaypoint.localPosition);
            if (distanceToWaypoint < 1.0f)
            {
                SetReward(1.0f);
                SetRandomWaypoint();
            }
            else
            {
                SetReward(-0.01f);
            }
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Provide manual control for testing
        var continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Vertical");
        continuousActions[1] = Input.GetAxis("Horizontal");
    }

    private void SetRandomWaypoint()
    {
        if (waypointContainer != null && waypointContainer.waypoints.Count > 0)
        {
            int randomIndex = Random.Range(0, waypointContainer.waypoints.Count);
            targetWaypoint = waypointContainer.waypoints[randomIndex];
        }
        else
        {
            Debug.LogError("WaypointContainer is not assigned or contains no waypoints.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wp"))
        {
            SetReward(1.0f);
            SetRandomWaypoint();
        }
    }
}
