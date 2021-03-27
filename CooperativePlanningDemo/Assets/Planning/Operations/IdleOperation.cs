using UnityEngine;

namespace Planning.Operations
{
  public class IdleOperation : IOperation
  {
    public IdleOperation(GoapAgent agent)
    {
    }

    public void Init()
    {
      Debug.Log("Waiting For Door");
    }

    public void Update()
    {
      
    }

    public void Exit()
    {
      
    }

    public bool IsComplete()
    {
      return true;
    }
  }
}