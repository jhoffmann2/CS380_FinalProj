using System;
using Planning.Operations;
using UnityEngine;

namespace Planning.Tasks
{
  public class CollectGoldTask : Task
  {
    static bool AtLocation(Blackboard blackboard, Vector3 pos)
    {
      Vector3 position = (Vector3)blackboard.Get("Location");
      return Vector3.Distance(pos, position) < 1f;
    }
    
    public CollectGoldTask(GoapAgent agent, Transform treasureChest) : base(
      new Func<Blackboard, bool> []
      {
        blackboard => AtLocation(blackboard, treasureChest.position)
      },
      new CollectGoldOperation(agent), 
      new Action<Blackboard>[]
      {
        blackboard => blackboard.SetBool("GoldCollected", true)
      }
    )
    {
    }
  }
}