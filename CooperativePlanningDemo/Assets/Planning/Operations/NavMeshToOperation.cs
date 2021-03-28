using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Planning.Operations
{
    public class NavMeshToOperation : IOperation
    {
        private NavMeshAgent navMeshAgent;
        private Vector3 goal;
        
        public NavMeshToOperation(Vector3 goal)
        {
            this.goal = goal;
        }

        public void Init(GoapAgent agent)
        {
            navMeshAgent = agent.transform.GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(goal);
        }

        public void Update(GoapAgent agent)
        {

        }

        public void Exit(GoapAgent agent)
        {
        }

        public bool IsComplete(GoapAgent agent)
        {
            return !navMeshAgent.pathPending && !navMeshAgent.hasPath;
        }
    }
}