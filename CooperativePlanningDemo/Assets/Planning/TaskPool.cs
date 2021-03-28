using System;
using System.Collections.Generic;
using UnityEngine;

namespace Planning
{
  [Serializable] 
  public class TaskPool
  {
    [SerializeField] private Task[] tasks;

    public void SetTasks(Task[] list)
    {
      tasks = list;
    }

    public Task[] GetTasks()
    {
      return tasks;
    }

    public Task GetBestTask(Blackboard state, List<Func<Blackboard, bool>> goals)
    {
      Task output = null;
      float bsf = float.MaxValue;
      foreach (Task task in tasks)
      {
        if (task.CanUse())
        {
          float weight = task.Weight(state, goals);
          if (weight < bsf)
          {
            bsf = weight;
            output = task;
          }
        }
      }
      return output;
    }
  }
}