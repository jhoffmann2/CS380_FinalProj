using System;
using Planning.Operations;
using UnityEngine;

namespace Planning.Tasks
{
  public class WaitForDoorTask : Task
  {
    static bool AtLocation(Blackboard blackboard, Vector3 pos)
    {
      Vector3 position = (Vector3)blackboard.Get("Location");
      return Vector3.Distance(pos, position) < 1f;
    }
    
    public WaitForDoorTask(GoapAgent agent, Transform [] buttons) : base(
      new Func<Blackboard, bool>[]
      {
        blackboard => 
          AtLocation(blackboard, buttons[0].position) || 
          AtLocation(blackboard, buttons[1].position)
      },
      new IdleOperation(agent), 
      new Action<Blackboard>[]
      {
        blackboard => blackboard.SetBool("DoorOpen", true)
      }
    )
    {
      
    }
  }
}