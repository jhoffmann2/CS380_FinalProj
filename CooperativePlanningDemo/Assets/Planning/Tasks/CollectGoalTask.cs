using System;
using Planning.Operations;
using UnityEngine;

namespace Planning.Tasks
{
  [Serializable] public class CollectGoalTask : Task
  {
    [SerializeField] private Transform treasureChest;

    protected override Func<Blackboard, bool>[] GetPreConditions()
    {
      bool AtLocation(Blackboard blackboard, Vector3 pos)
      {
        Vector3 position = (Vector3)blackboard.Get("Location");
        return Vector3.Distance(pos, position) < 1f;
      }
      
      return new Func<Blackboard, bool>[]
      {
        blackboard => AtLocation(blackboard, treasureChest.position)
      };
    }

    protected override IOperation GetOperation()
    {
      return new CollectGoalOperation();
    }

    protected override Action<Blackboard>[] GetEffects()
    {
      return new Action<Blackboard>[]
      {
        blackboard => blackboard.SetBool("GoalCollected", true)
      };
    }
  }
}