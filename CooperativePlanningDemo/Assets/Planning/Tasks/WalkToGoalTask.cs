using System;
using Planning.Operations;
using UnityEngine;

namespace Planning.Tasks
{
  [Serializable] public class WalkToGoalTask : Task
  {
    [SerializeField] private Transform chest = null;

    protected override Func<Blackboard, bool>[] GetPreConditions()
    {
      return new Func<Blackboard, bool>[]
      {
        blackboard => blackboard.GetBool("DoorOpen")
      };
    }

    protected override IOperation GetOperation()
    {
      return new NavMeshToOperation(chest.position);
    }

    protected override Action<Blackboard>[] GetEffects()
    {
      return new Action<Blackboard>[]
      {
        blackboard => blackboard.Set("Location", chest.position)
      };
    }
  }
}