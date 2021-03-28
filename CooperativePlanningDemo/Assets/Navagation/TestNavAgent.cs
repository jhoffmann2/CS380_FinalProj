using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavAgent : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                NavMeshPath path = new NavMeshPath();
                if(agent.CalculatePath(hit.point, path))
                {
                    if(path.status == NavMeshPathStatus.PathComplete)
                        Debug.Log("Vaild Path");
                    else
                        Debug.Log("No Path");
                    
                }
            }
        }
    } 
}
