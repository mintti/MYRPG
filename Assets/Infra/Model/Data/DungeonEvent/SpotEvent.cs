using System.ComponentModel;
using Infra.Model.Resource;

namespace Infra.Model.Data
{
    /// <summary>
    /// 맵에 대표적으로 표시될 수 있는 아이콘
    /// </summary>
    internal enum SpotEventType
    {
        None,
        Battle,
        Elite,
        Boss,
        Rest,
        Artefact,
        Event
    }
    
    /// <summary>
    /// Spot에서 발생하는 이벤트 정보
    /// </summary>
    internal class SpotEvent
    {
        public SpotEventType Type { get; set; }
        
        public int Index { get;}

        public SpotEvent()
        {
            
        }

        public SpotEvent(int index)
        {
            Index = index;
        }
    }
}