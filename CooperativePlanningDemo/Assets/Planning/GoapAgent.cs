using System;
using System.Collections.Generic;
using Planning.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Planning
{
  public class GoapAgent : MonoBehaviour
  {
    public Text text;
    
    [SerializeField] private TaskPool taskPool = new TaskPool();
    private Blackboard state;
    private Stack<Task> tasks;
    private string agentName = null;
    private void Start()
    {
      InitializeBlackboard();
      var goals = new List<Func<Blackboard, bool>>
      {
        blackboard => blackboard.GetBool("GoalCollected")
      };
      tasks = GoapPlanner.Plan(goals, state, taskPool);
      if(tasks.Count > 0)
        tasks.Peek().operation.Init(this);

      agentName = text.text.ToString();
    }

    private void Update()
    {
      if (tasks.Count == 0)
        return;
      
      var curTask = tasks.Peek();
      if (curTask.operation.IsComplete(this))
      {
        curTask.operation.Exit(this);
        tasks.Pop();
        if(tasks.Count > 0)
          tasks.Peek().operation.Init(this);
        return;
      }
      
      curTask.operation.Update(this);
      text.text = agentName + " " + curTask.TaskName;
    }

    private void InitializeBlackboard()
    {
      state = new Blackboard();
      state.Set("Location", transform.position);
      state.SetBool("Button1Presssed", false);
      state.SetBool("Button2Presssed", false);
      state.SetBool("DoorOpen", false);
      state.SetBool("GoalCollected", false);
    }
  }
}