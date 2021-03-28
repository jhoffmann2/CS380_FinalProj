using System;
using Planning.Operations;
using UnityEngine;

namespace Planning.Tasks
{
  [Serializable] public class WaitForDoorTask : Task
  {
    [SerializeField] private Transform[] buttons = new Transform[2];
    [SerializeField] private Transform goal;

    protected override Func<Blackboard, bool>[] GetPreConditions()
    {
      bool AtLocation(Blackboard blackboard, Vector3 pos)
      {
        Vector3 position = (Vector3)blackboard.Get("Location");
        return Vector3.Distance(pos, position) < 1f;
      }
      
      return new Func<Blackboard, bool>[]
      {
        blackboard =>
          AtLocation(blackboard, buttons[0].position) ||
          AtLocation(blackboard, buttons[1].position)
      };
    }

    protected override IOperation GetOperation()
    {
      return new WaitForPathOperation(goal.position);
    }

    protected override Action<Blackboard>[] GetEffects()
    {
      return new Action<Blackboard>[]
      {
        blackboard => blackboard.SetBool("DoorOpen", true)
      };
    }
  }
}