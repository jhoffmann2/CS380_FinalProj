using System;
using Planning.Operations;
using UnityEngine;

namespace Planning.Tasks
{
  [Serializable] public class WalkToButtonTask : Task
  {
    [SerializeField] private Transform button;

    protected override Func<Blackboard, bool>[] GetPreConditions()
    {
      return new Func<Blackboard, bool>[] { };
    }

    protected override IOperation GetOperation()
    {
      return new NavMeshToOperation(button.position);
    }

    protected override Action<Blackboard>[] GetEffects()
    {
      return new Action<Blackboard>[]
      {
        blackboard => blackboard.Set("Location", button.position)
      };
    }

    protected override int GetMaxUses()
    {
      return 1;
    }
  }
}