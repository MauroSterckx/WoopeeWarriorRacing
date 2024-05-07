using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class carAgent : Agent
{
    Rigidbody rb;
    Transform target;
    float speed = 5f;
    float rotationSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Finish").transform;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(Random.Range(-8f, 8f), 0.5f, Random.Range(-8f, 8f));
        rb.velocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(target.position);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float forward = actionBuffers.ContinuousActions[0];
        float turn = actionBuffers.ContinuousActions[1];
        rb.AddForce(transform.forward * forward * speed);
        transform.Rotate(Vector3.up * turn * rotationSpeed);

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget < 1.5f)
        {
            SetReward(1f); // Reward for reaching the target
            EndEpisode();
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("checkpoint"))
        {
            SetReward(0.1f); // Reward for colliding with checkpoints
            Destroy(col.gameObject); // Destroy the checkpoint object
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }
}

