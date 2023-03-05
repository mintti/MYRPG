using System.Collections.Generic;
using Infra.Model.Resource;
using Unity.Jobs;
using UnityEngine;
using Color = System.Drawing.Color;

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
                new Job(1, "Warrior", Color.Blue),
                new Job(2, "Wizard", Color.Purple),
                new Job(3, "Archer", Color.Green),
                new Job(4, "Knight", Color.LightSalmon),
                new Job(5, "Priest", Color.Yellow),
            };
        }
    }
}
