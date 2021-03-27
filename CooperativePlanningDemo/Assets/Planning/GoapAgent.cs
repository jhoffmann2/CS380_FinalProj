using System;
using System.Collections.Generic;
using Planning.Tasks;
using UnityEngine;

namespace Planning
{
  public class GoapAgent : MonoBehaviour
  {
    [SerializeField] private TaskPool taskPool = null;
    [SerializeField] private Transform[] buttonTransforms = new Transform[2];
    [SerializeField] private Transform treasureChest = null;
    private Blackboard state;
    private Stack<Task> tasks;

    private void Start()
    {
      InitializeBlackboard();
      InitializeTaskPool();
      var goals = new List<Func<Blackboard, bool>>
      {
        blackboard => blackboard.GetBool("GoldCollected")
      };
      tasks = GoapPlanner.Plan(goals, state, taskPool);
      if(tasks.Count > 0)
        tasks.Peek().operation.Init();
    }

    private void Update()
    {
      if (tasks.Count == 0)
        return;
      
      var curTask = tasks.Peek();
      if (curTask.operation.IsComplete())
      {
        curTask.operation.Exit();
        tasks.Pop();
        if(tasks.Count > 0)
          tasks.Peek().operation.Init();
        return;
      }
      
      curTask.operation.Update();
    }

    private void InitializeTaskPool()
    {
      taskPool.SetTasks(new Task[]
      {
        new WalkToButtonTask(this, buttonTransforms[0]), 
        new WalkToButtonTask(this, buttonTransforms[1]), 
        new WaitForDoorTask(this, buttonTransforms),
        new WalkToChestTask(this, treasureChest),
        new CollectGoldTask(this, treasureChest), 
      });
    }

    private void InitializeBlackboard()
    {
      state = new Blackboard();
      state.Set("Location", transform.position);
      state.SetBool("Button1Presssed", false);
      state.SetBool("Button2Presssed", false);
      state.SetBool("DoorOpen", false);
      state.SetBool("GoldCollected", false);
    }
  }
}