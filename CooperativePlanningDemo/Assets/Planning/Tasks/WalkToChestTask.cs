using System;
using Planning.Operations;
using UnityEngine;

namespace Planning.Tasks
{
  [Serializable] public class WalkToChestTask : Task
  {
    [SerializeField] private Transform chest;

    protected override Func<Blackboard, bool>[] GetPreConditions()
    {
      return new Func<Blackboard, bool>[]
      {
        blackboard => blackboard.GetBool("DoorOpen")
      };
    }

    protected override IOperation GetOperation()
    {
      return new NavigateToOperation(chest.position);
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