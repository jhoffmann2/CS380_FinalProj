using UnityEngine;

namespace Planning.Operations
{
  public class NavigateToOperation : IOperation
  {
    private Vector3 goal;
    private Vector3 start;
    private GoapAgent agent;
    private float t;

    public NavigateToOperation(GoapAgent agent, Vector3 goal)
    {
      this.goal = goal;
      this.agent = agent;
    }

    public void Init()
    {
      Debug.Log("Navigating to: " + goal);
      start = agent.transform.position;
      goal.y = start.y;
      t = 0;
    }

    public void Update()
    {
      agent.transform.position = Vector3.Lerp(start, goal, t);
      t += Time.deltaTime / 2f;
    }

    public void Exit()
    {
    }

    public bool IsComplete()
    {
      return t >= 1f;
    }
  }
}