using System;
using Planning.Operations;
using UnityEngine;

namespace Planning.Tasks
{
  [Serializable] public class WalkToButtonTask : Task
  {
    [SerializeField] private Transform button = null;

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
    
    private const string taskName = "Walk To Button";
    protected override string GetTaskName()
    {
      return taskName;
    }
  }
}