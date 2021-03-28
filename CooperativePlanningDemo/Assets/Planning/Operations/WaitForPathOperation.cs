using UnityEngine;
using UnityEngine.AI;

namespace Planning.Operations
{
  public class WaitForPathOperation : IOperation
  {
    private NavMeshAgent navMeshAgent;
    private Vector3 goal;
    private NavMeshPath path;
    
    public WaitForPathOperation(Vector3 goal)
    {
      this.goal = goal;
    }

    public void Init(GoapAgent agent)
    {
      Debug.Log("Waiting For Path");
      navMeshAgent = agent.GetComponent<NavMeshAgent>();
      navMeshAgent.CalculatePath(goal, path);
    }

    public void Update(GoapAgent agent)
    {
    }

    public void Exit(GoapAgent agent)
    {
      
    }

    public bool IsComplete(GoapAgent agent)
    {
      return navMeshAgent.hasPath;
    }
  }
}