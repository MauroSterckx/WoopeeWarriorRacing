using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors.Reflection;

public class RayAgent : Agent
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    private Rigidbody rb;
    private RayPerceptionSensorComponent3D raySensor;

    public float wallCollisionThreshold = 0.1f;
    private int wallCollisionCount = 0;

    private int lastWaypointIndex = -1;
    private int sameWaypointCount = 0;
    public int maxSameWaypointCount = 3; // Maximum aantal keren dat de agent dezelfde waypoint mag raken voordat hij strafpunten krijgt

    public GameObject[] waypoints;
    private HashSet<GameObject> hitWaypoints;

    private float episodeStartTime;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody component found on this GameObject");
        }

        raySensor = GetComponent<RayPerceptionSensorComponent3D>();
        if (raySensor == null)
        {
            Debug.LogError("No Ray Perception Sensor 3D component found on this GameObject");
        }

        hitWaypoints = new HashSet<GameObject>();
    }

    public override void OnEpisodeBegin()
    {
        // Reset the position and rotation of the agent
        transform.localPosition = new Vector3(-2.150918f, 0.5f, 16.93153f);
        transform.localRotation = Quaternion.Euler(0, -90f, 0);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        wallCollisionCount = 0;
        lastWaypointIndex = -1;
        sameWaypointCount = 0;
        episodeStartTime = Time.time;

        hitWaypoints.Clear();
        ReactivateWaypoints();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add observations from the Ray Perception Sensor
        RayPerceptionInput rayInput = raySensor.GetRayPerceptionInput();
        var rayOutput = RayPerceptionSensor.Perceive(rayInput);
        foreach (var rayOutputResult in rayOutput.RayOutputs)
        {
            sensor.AddObservation(rayOutputResult.HitFraction); // Distance to the object
            sensor.AddObservation(rayOutputResult.HasHit ? 1.0f : 0.0f); // Whether there was a hit
            sensor.AddObservation(rayOutputResult.HasHit ? rayOutputResult.HitTagIndex : -1); // The tag of the object
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2 (forward movement, rotation)
        float moveAction = actionBuffers.ContinuousActions[0];
        float rotationAction = actionBuffers.ContinuousActions[1];

        // Check if the agent is moving backwards
        if (moveAction < 0)
        {
            AddReward(-0.1f); // Penalize for moving backwards
        }

        // Move the agent forward
        rb.velocity = transform.forward * moveAction * speed * Time.deltaTime;

        // Rotate the agent
        transform.Rotate(Vector3.up * rotationAction * rotationSpeed * Time.deltaTime);

        // Check if the agent hits a wall
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.0f);
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("WALL"))
            {
                Debug.Log("Hit a wall");
                SetReward(-5f);
                EndEpisode();
            }
            else if (col.CompareTag("wp"))
            {
                if (!hitWaypoints.Contains(col.gameObject))
                {
                    Debug.Log("Hit a waypoint");
                    hitWaypoints.Add(col.gameObject);
                    SetReward(+0.1f);
                    col.gameObject.SetActive(false); // Deactivate the waypoint
                }
            }
            else if (col.CompareTag("Finish"))
            {
                if (hitWaypoints.Count == waypoints.Length)
                {
                    float timeTaken = Time.time - episodeStartTime;
                    SetReward(5f + (10f / timeTaken)); // Reward based on time taken to reach the finish
                    EndEpisode();
                }
                else
                {
                    Debug.Log("Reached finish without hitting all waypoints");
                    SetReward(-1f);
                    EndEpisode();
                }
            }
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical"); // Forward and backward
        continuousActionsOut[1] = Input.GetAxis("Horizontal"); // Rotation
    }

    private void ReactivateWaypoints()
    {
        Debug.Log("Reactivating waypoints");
        foreach (GameObject wp in waypoints)
        {
            wp.SetActive(true);
        }
    }
}
