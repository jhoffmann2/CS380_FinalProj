using UnityEngine;

namespace Planning.Operations
{
  public class CollectGoldOperation : IOperation
  {
    public CollectGoldOperation(GoapAgent agent)
    {
    }

    public void Init()
    {
      Debug.Log("Collecting Gold");
    }

    public void Update()
    {}

    public void Exit()
    {}

    public bool IsComplete()
    {
      return true;
    }
  }
}