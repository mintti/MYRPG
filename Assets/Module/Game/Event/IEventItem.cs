using UnityEngine;

namespace Module.Game.Event
{
    /// <summary>
    /// 던전에서 발생하는 이벤트들의 아이템
    /// </summary>
    internal interface IEventItem
    {
        void UpdateUIGame(UIGame uiGame);
        void Execute();
    }
}