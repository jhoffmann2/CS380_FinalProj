using UnityEngine;

namespace Planning.Operations
{
  public class CollectGoalOperation : IOperation
  {
    public void Init(GoapAgent agent)
    {
      Debug.Log("Collecting Goal");
    }

    public void Update(GoapAgent agent)
    {}

    public void Exit(GoapAgent agent)
    {}

    public bool IsComplete(GoapAgent agent)
    {
      return true;
    }
  }
}