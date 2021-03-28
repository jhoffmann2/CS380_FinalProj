using UnityEngine;

namespace Planning.Operations
{
  public class IdleOperation : IOperation
  {

    public void Init(GoapAgent agent)
    {
      Debug.Log("Waiting For Door");
    }

    public void Update(GoapAgent agent)
    {
      
    }

    public void Exit(GoapAgent agent)
    {
      
    }

    public bool IsComplete(GoapAgent agent)
    {
      return true;
    }
  }
}