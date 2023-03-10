using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Steering
{
    public static Vector3 Seek(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (target.transform.position - agent.transform.position));

        return force;
    }

    public static Vector3 flee(Agent agent, GameObject target)
    {
        Vector3 force = CalculateSteering(agent, (agent.transform.position - target.transform.position));

        return force;
    }

    public static Vector3 CalculateSteering(Agent agent, Vector3 direction)
    {
        Vector3 ndirection = direction.normalized;
        Vector3 desired = ndirection * agent.movement.maxSpeed;
        Vector3 steer = desired - agent.movement.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.movement.maxForce);

        return force;
    }

	public static Vector3 Wander(AutonAgent agent)
	{
		// randomly adjust angle +/- displacement 
		agent.wanderAngle = agent.wanderAngle + Random.Range(-
	   agent.wanderDisplacement, agent.wanderDisplacement);
		// create rotation quaternion around y-axis (up) 
		Quaternion rotation = Quaternion.AngleAxis(agent.wanderAngle, Vector3.up);
		// calculate point on circle radius 
		Vector3 point = rotation * (Vector3.forward * agent.wanderRadius);
		// set point in front of agent at distance length 
		Vector3 forward = agent.transform.forward * agent.wanderDistance;

        Debug.DrawRay(agent.transform.position, forward + point, Color.red);

		Vector3 force = CalculateSteering(agent, forward + point);

		return force;
	}

    public static Vector3 Cohesion(Agent agent, GameObject[] neighbors)
    {
        Vector3 center = Vector3.zero;
        foreach (GameObject neighbor in neighbors) 
        {
            center += neighbor.transform.position;
        }
        center /= neighbors.Length;

        Vector3 force = CalculateSteering(agent, center - agent.transform.position);

        return force;
    }

    public static Vector3 Separation(Agent agent, GameObject[] neighbors, float radius)
    {
        return Vector3.zero;

    }public static Vector3 Alignment(Agent agent, GameObject[] neighbors)
    {
        return Vector3.zero;
    }
}
