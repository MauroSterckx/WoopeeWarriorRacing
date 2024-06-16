using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CarAgent : Agent
{
    public Transform Target;
    public float speedMultiplier = 0.1f;
    private Rigidbody rb;
    private bool hitwall = false;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody component found on this GameObject");
        }
    }

    public override void OnEpisodeBegin()
    {
        // Reset the position and orientation if the agent has fallen
        if (this.transform.localPosition.y < 0)
        {
            this.transform.localPosition = new Vector3(-5.21f, 0.35f, 17.45f);
            this.transform.localRotation = Quaternion.identity;
            rb.velocity = Vector3.zero; // Reset velocity
            rb.angularVelocity = Vector3.zero; // Reset angular velocity
        }

        // Reset the hitwall flag
        hitwall = false;

        // Move the target to a new random position
        Target.localPosition = new Vector3(Random.value * 8 - 4, 0.5f, Random.value * 8 - 4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add the agent's position and the target's position as observations
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(Target.localPosition);

        // Add the distance to the target as an observation
        sensor.AddObservation(Vector3.Distance(this.transform.localPosition, Target.localPosition));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WALL"))
        {
            hitwall = true;
            Debug.Log("AUW");
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rb.AddForce(controlSignal * speedMultiplier, ForceMode.VelocityChange);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);

        // Target reached
        if (distanceToTarget < 0.5f)
        {
            SetReward(30.0f);
            EndEpisode();
        }
        // Collision with wall
        else if (hitwall)
        {
            SetReward(-10f);
            EndEpisode();
        }
        // Fell off platform
        else if (distanceToTarget > 30f)
        {
            SetReward(-10f);
        }
        // Fell below platform
        else if (this.transform.localPosition.y < 0)
        {
            SetReward(-10f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxis("Vertical");
        continuousActions[1] = Input.GetAxis("Horizontal");
    }
}
