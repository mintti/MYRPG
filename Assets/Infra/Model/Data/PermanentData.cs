using System.Collections.Generic;
using System.ComponentModel;

namespace Infra.Model.Data
{
    /// <summary>
    /// 영구적인 플레이어 데이타
    /// - 해금 정보 등 포함
    /// </summary>
    internal class PermanentData : Singleton<PermanentData>
    {
        public PermanentData()
        {
            if (true) // 데이터가 없다면 Init();
            {
                Init();
                // save
            }
        }
        
        public Dictionary<UnlockType, bool[]> UnlockDict { get; private set; }

        private void Init()
        {
            UnlockDict = new()
            {
                {UnlockType.Job, new bool[6] {true, true, true, true, true, false}},
                {UnlockType.Artefact, new bool[1] {false}},
                {UnlockType.Dungeon, new bool[1] {false}},
                {UnlockType.Monster, new bool[1] {false}},
            };
        }

        public void Unlock(UnlockType type, int index) => UnlockDict[type][index] = true;
    }

    internal enum UnlockType
    {
        [Description("Job")]
        Job,
        [Description("Artefact")]
        Artefact,
        [Description("Dungeon")]
        Dungeon,
        [Description("Monster")]
        Monster,
    }
}