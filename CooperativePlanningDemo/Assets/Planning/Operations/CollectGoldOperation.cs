using UnityEngine;

namespace Planning.Operations
{
  public class CollectGoldOperation : IOperation
  {
    public void Init(GoapAgent agent)
    {
      Debug.Log("Collecting Gold");
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