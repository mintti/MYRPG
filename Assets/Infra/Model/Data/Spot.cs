using System;
using System.Collections.Generic;

namespace Infra.Model.Data
{
    internal enum SpotState
    {
        None,
        Clear,
        Do,
    }
    internal class Spot
    {
        public int Index { get; protected set; }
        
        public int Depth { get; set; }
        public SpotState State { get; set; } 
     
        public List<Spot> ChildSpots { get; set; }
       
        public SpotEvent Event { get; set; }

        #region Initializer
        public Spot(int index, int depth)
        {
            Index = index;
            Depth = depth;
        }
    
        /// <summary>
        /// 다음 노드를 연결
        /// </summary>
        /// <param name="child">자식 노드</param>
        public void Connect(Spot child)
        {
            ChildSpots ??= new List<Spot>();
            ChildSpots.Add(child);
        }
        #endregion

        /// <summary>
        /// 클리어한 Spot의 자식들에 접근 가능하도록 업데이트
        /// </summary>
        /// <param name="state"></param>
        public void UpdateState(SpotState state)
        {
            State = state;

            switch (state)
            {
                case SpotState.Clear :
                    ChildSpots.ForEach(spot => spot.UpdateState(SpotState.Do));
                    break;
            }
        }

        /// <summary>
        /// 같은 레벨의 나머지 Spot은 접근 불가능 하도록 업데이트
        /// </summary>
        public void UpdateStateRest(int clearDepth)
        {
            if (Depth == clearDepth)
            {
                if (State == SpotState.Do)
                {
                    State = SpotState.None;
                }
            }
            else if (Depth < clearDepth)
            {
                ChildSpots.ForEach(s => s.UpdateStateRest(clearDepth));
            }
        }
    }
}