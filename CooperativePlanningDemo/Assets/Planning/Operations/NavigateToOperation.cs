using UnityEngine;

namespace Planning.Operations
{
  public class NavigateToOperation : IOperation
  {
    private Vector3 goal;
    private Vector3 start;
    private float t;

    public NavigateToOperation(Vector3 goal)
    {
      this.goal = goal;
    }

    public void Init(GoapAgent agent)
    {
      Debug.Log("Navigating to: " + goal);
      start = agent.transform.position;
      goal.y = start.y;
      t = 0;
    }

    public void Update(GoapAgent agent)
    {
      agent.transform.position = Vector3.Lerp(start, goal, t);
      t += Time.deltaTime / 2f;
    }

    public void Exit(GoapAgent agent)
    {
    }

    public bool IsComplete(GoapAgent agent)
    {
      return t >= 1f;
    }
  }
}