using System;
using Planning.Operations;
using UnityEngine;

namespace Planning.Tasks
{
  public class WalkToButtonTask : Task
  {
    public WalkToButtonTask(GoapAgent agent, Transform button) : base(
      new Func<Blackboard, bool>[] { },
      new NavigateToOperation(agent, button.position),
      new Action<Blackboard>[]
      {
        blackboard => blackboard.Set("Location", button.position)
      }
    )
    {
    }
  }
}