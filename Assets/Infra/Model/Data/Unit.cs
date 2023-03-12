﻿using System.Collections.Generic;

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
        }
        
    }
}