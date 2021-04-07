using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Planning
{
  public abstract class Task : ScriptableObject
  {
    private IOperation operationCache;
    public IOperation operation => 
      operationCache ?? (operationCache = GetOperation());

    private Func<Blackboard,bool>[] preconditionsCache;
    public Func<Blackboard, bool>[] preconditions => 
      preconditionsCache ?? (preconditionsCache = GetPreConditions());

    private Action<Blackboard>[] effectsCache;
    private Action<Blackboard>[] effects => 
      effectsCache ?? (effectsCache = GetEffects());

    [SerializeField, Tooltip("Set to zero for infinite uses")] private int maxUses = 0;
    private int currentUses = 0;

    protected abstract Func<Blackboard, bool>[] GetPreConditions();
    protected abstract IOperation GetOperation();
    protected abstract Action<Blackboard>[] GetEffects();

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