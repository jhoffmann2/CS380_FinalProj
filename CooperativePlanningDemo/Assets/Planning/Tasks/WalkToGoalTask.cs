using System;
using Planning.Operations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Planning.Tasks
{
  [Serializable] public class WalkToGoalTask : Task
  {
    [SerializeField] private Transform goal = null;

    protected override Func<Blackboard, bool>[] GetPreConditions()
    {
      return new Func<Blackboard, bool>[]
      {
        blackboard => blackboard.GetBool("DoorOpen")
      };
    }

    protected override IOperation GetOperation()
    {
      return new NavMeshToOperation(goal.position);
    }

    protected override Action<Blackboard>[] GetEffects()
    {
      return new Action<Blackboard>[]
      {
        blackboard => blackboard.Set("Location", goal.position)
      };
    }
  }
}