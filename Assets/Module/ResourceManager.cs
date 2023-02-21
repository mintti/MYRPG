using System.Collections.Generic;
using Infra.Model.Resource;
using Unity.Jobs;
using UnityEngine;

namespace Module
{
    /// <summary>
    /// 전체 리소스 관리
    /// </summary>
    public static class ResourceManager
    {
        private static List<Job> _jobList;
        public static List<Job> JobList
        {
            get
            {
                if(_jobList == null)
                    TestInit();
                return _jobList;
            }
            private set => _jobList = value;
        }
        
        
        
        private static void TestInit()
        {
            JobList = new List<Job>()
            {
                new Job(1, "Warrior"),
                new Job(2, "Wizard"),
                new Job(3, "Archer"),
                new Job(4, "Knight"),
                new Job(5, "Priest"),
            };
        }
    }
}
