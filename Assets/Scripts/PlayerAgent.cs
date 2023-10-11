using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PlayerAgent : Agent
{
    // [Tooltip("The platform to be moved around")]
    // public GameObject platform;

    private Vector3 startPosition;
    private PlayerController playerController;
    new private Rigidbody rigidbody;

    // Called once when the agent is first initialized
    public override void Initialize()
    {
        startPosition = transform.position;
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Called every time an episode begins. This is where we reset the challenge.
    public override void OnEpisodeBegin()
    {
        // Reset agent position, rotation
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));
        rigidbody.velocity = Vector3.zero;

        // // Reset platform position (5 meters away from the agent in a random direction)
        // platform.transform.position = startPosition + Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)) * Vector3.forward * 5f;
    }

    // Controls the agent with human input
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        bool jump = Input.GetKey(KeyCode.Space);

        // Convert the actions to Discrete choices (0, 1, 2)
        ActionSegment<int> actions = actionsOut.DiscreteActions;
        actions[0] = vertical >= 0 ? vertical : 2;
        actions[1] = horizontal >= 0 ? horizontal : 2;
        actions[2] = jump ? 1 : 0;
    }

    // React to actions coming from either the neural net or human input
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Punish and end episode if the agent strays too far
        if(Vector3.Distance(startPosition, transform.position) > 10f)
        {
            AddReward(-1f);
            EndEpisode();
        }

        // Convert actions from Discrete (0, 1, 2) to expected input values (-1, 0, +1) of the character controller
        float vertical = actions.DiscreteActions[0] <= 1 ? actions.DiscreteActions[0] : -1;
        float horizontal = actions.DiscreteActions[1] <= 1 ? actions.DiscreteActions[1] : -1;
        bool jump = actions.DiscreteActions[2] > 0;
        
        // playerController.ForwardInput = vertical;
        playerController.HorizontalInput = horizontal;
        playerController.JumpInput = jump;     
    }

    // Respond to entering a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // If the other object is a collectible, reward and end episode
        if (other.tag == "collectible")
        {
            AddReward(1f);
            EndEpisode();
        }
    }
}
