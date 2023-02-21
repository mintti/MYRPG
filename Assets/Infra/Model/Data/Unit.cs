using System.Collections.Generic;

namespace Infra.Model.Data
{
    public class Unit
    {
        public int JobIndex { get; }
        
        public List<Block> HasBlocks { get; set; }
        
        public int Hp { get; set; }

        public Unit(int jobIndex)
        {
            JobIndex = jobIndex;
            // 기본 데이타 설정
        }
        
    }
}