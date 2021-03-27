using System;
using Planning.Operations;
using UnityEngine;

namespace Planning.Tasks
{
  public class WalkToChestTask : Task
  {
    public WalkToChestTask(GoapAgent agent, Transform chest) : base(
      new Func<Blackboard, bool> []
      {
        blackboard => blackboard.GetBool("DoorOpen")
      },
      new NavigateToOperation(agent, chest.position), 
      new Action<Blackboard>[]
      {
        blackboard => blackboard.Set("Location", chest.position)
      }
    )
    {
    }
  }
}