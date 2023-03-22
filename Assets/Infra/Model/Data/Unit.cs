using System.Collections.Generic;
using Module;

namespace Infra.Model.Data
{
    internal class Unit
    {
        public int JobIndex { get; }
        
        public List<(int Index, int Grade, int Level)> HasBlocks { get; set; }
        
        public int Hp { get; set; }

        public Unit(int jobIndex)
        {
            JobIndex = jobIndex;

            // 기본 데이타 설정
            HasBlocks = new List<(int Index, int Grade, int Level)>();
            for (var i = 0; i < 5; i++)
                HasBlocks.Add((1, 1, 1));

            Hp = ResourceManager.Instance.Jobs[jobIndex].Hp;
        }

    }
}