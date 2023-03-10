using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
	public Agent[] agents;
	public LayerMask layerMask;

	int index = 0;

	void Update()
	{
		if (Input.GetKey(KeyCode.Alpha1)) index = 1;
		if (Input.GetKey(KeyCode.Alpha2)) index = 2;

		if (Input.GetMouseButton(0))
		{
			Debug.Log("SPAWN");
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, 200, layerMask))
			{
				Instantiate(agents[index], hitInfo.point, Quaternion.identity);
			}
		}
	}
}
