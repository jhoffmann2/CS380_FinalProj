using System;
using System.Collections.Generic;

namespace Planning
{
  public static class GoapPlanner
  {
    public static Stack<Task> Plan(List<Func<Blackboard, bool>> goals, Blackboard state, TaskPool taskPool)
    {
      Stack<Blackboard> stateStack = new Stack<Blackboard>();
      stateStack.Push(new Blackboard(state));
      
      Stack<List<Func<Blackboard, bool>>> goalStack = new Stack<List<Func<Blackboard, bool>>>();
      goalStack.Push(goals);
      
      Stack<Task> taskStack = new Stack<Task>();
      int loopCounter = 0;
      while (!GoalsSatisfied(goalStack.Peek(), stateStack.Peek()) && loopCounter < 100)
      {
        ++loopCounter;
        Task newTask = taskPool.GetBestTask(stateStack.Peek(), goalStack.Peek());
        while (newTask == null)
        {
          taskStack.Pop().Unuse();
          stateStack.Pop();
          goalStack.Pop();
          newTask = taskPool.GetBestTask(stateStack.Peek(), goalStack.Peek());
        }
        taskStack.Push(newTask);
        newTask.Use();
        
        Blackboard newState = new Blackboard(stateStack.Peek());
        newTask.ApplyEffects(newState);
        stateStack.Push(newState);

        // comprise list of unmet goals
        List<Func<Blackboard, bool>> newGoals = new List<Func<Blackboard, bool>>();
        foreach (var goal in goals)
          if (!goal(newState))
            newGoals.Add(goal);
        foreach (var goal in newTask.preconditions)
          if (!goal(newState))
            newGoals.Add(goal);
        
        goalStack.Push(newGoals);
        
      }

      return taskStack;
    }

    private static bool GoalsSatisfied(List<Func<Blackboard, bool>> goals, Blackboard state)
    {
      foreach (Func<Blackboard,bool> goal in goals)
        if (!goal(state)) return false;
      return true;
    }
  }
}