using System;
using System.Collections.Generic;

namespace Planning
{
  public class Task
  {
    public readonly IOperation operation;

    public readonly Func<Blackboard,bool>[] preconditions;
    private readonly Action<Blackboard>[] effects;
    private readonly int maxUses;
    private int currentUses = 0;

    public Task(
      Func<Blackboard,bool>[] preconditions, 
      IOperation operation, 
      Action<Blackboard>[] effects, 
      int maxUses = 0)
    {
      this.preconditions = preconditions;
      this.operation = operation;
      this.effects = effects;
      this.maxUses = maxUses;
    }

    public bool CanUse()
    {
      if (maxUses == 0)
        return true;
      return currentUses < maxUses;
    }

    public void Use()
    {
      if (maxUses != 0)
        ++currentUses;
    }
    
    public bool TryUse()
    {
      if (!CanUse())
        return false;
      Use();
      return true;
    }

    public void Unuse()
    {
      if (maxUses != 0)
        --currentUses;
    }

    public void ApplyEffects(Blackboard state)
    {
      foreach (Action<Blackboard> effect in effects)
        effect(state);
    }

    // scale weight of this task by the number of unsatisfied goals that will
    // exist after it runs to completeion
    public float Weight(Blackboard state, List<Func<Blackboard, bool>> goals)
    {
      // assume any unmet precondition will be added to the goals
      float total = 0f;
      foreach (Func<Blackboard,bool> precondition in preconditions)
        if (!precondition(state))
          total += 0.99f;
      
      // add any pre-existing goals that this task didn't meet
      Blackboard modifiedState = new Blackboard(state);
      ApplyEffects(modifiedState);
      foreach (Func<Blackboard,bool> goal in goals)
        if (!goal(modifiedState))
          total += 1f;
      
      return total;
    }
  }
}