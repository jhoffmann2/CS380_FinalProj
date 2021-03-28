using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Planning.Operations
{
    public class NavMeshToOperation : IOperation
    {
        private GoapAgent goapAgentagent;
        private NavMeshAgent navMeshAgent;
        private Vector3 goal;
        
        public NavMeshToOperation(GoapAgent agent, Vector3 goal)
        {
            this.goapAgentagent = agent;
            this.navMeshAgent = agent.transform.GetComponent<NavMeshAgent>();
            this.goal = goal;
        }

        public void Init()
        {
            navMeshAgent.SetDestination(goal);
        }

        public void Update()
        {

        }

        public void Exit()
        {
        }

        public bool IsComplete()
        {
            return !navMeshAgent.pathPending && !navMeshAgent.hasPath;
        }
    }
}