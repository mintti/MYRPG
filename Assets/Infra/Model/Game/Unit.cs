using System.Collections.Generic;
using Module;
using Module.Game;
using Module.Game.Battle;

namespace Infra.Model.Game
{
    internal class Unit : BattleEntity, IBattleEntity
    {
        #region Variables
        public int JobIndex { get; private set;}
        public List<Block> HasBlocks { get; set; } = new();
        #endregion

        public void SetBaseData(Data.Unit data)
        {
            JobIndex = data.JobIndex;

            foreach (var info in data.HasBlocks)
            {
                var block = Factory.GetBlock((JobIndex, info.Index, info.Level));
                block.ConnectCaster(this);
                HasBlocks.Add(block);
            }

            // 리소스 로드
            var jobBase = ResourceManager.Instance.Jobs[JobIndex];
            Name = jobBase.Name;
            MaxHp = jobBase.Hp;
            Hp = data.Hp;
            State = (data.Hp > 0) ? State.Alive : State.Die;
            Sprite = jobBase.Icon;
        }
        

        #region Common Battle Unit
        public bool IsCalled { get; set; }
        private Dictionary<string, ActionInfo> ActionInfoDict { get; set; } = new();
        private readonly Dictionary<string, ActionInfo> SkillBuffer = new();
        
        /// <summary>
        /// 스킬 게이지가 추가 된 순간, 행동으로 설정
        /// </summary>
        protected void CalledSkill(ActionInfo info)
        {
            string name = info.SkillName;
            // Check Skill Buffer. If not contained, add it in buffer.
            if (!SkillBuffer.ContainsKey(name))
            {
                SkillBuffer.Add(name, info);
            }

            // Add Action Info
            if (!ActionInfoDict.ContainsKey(name))
            {
                ActionInfoDict.Add(name, info);
            }
        }

        /// <summary>
        /// Battle Entity Override.
        /// 사용 가능한 스킬 리스트를 전달
        /// </summary>
        public override void Execute()
        {
            foreach (var action in ActionInfoDict.Values)
            {
                BattleController.UIActionSelector.AddAction(action);
            }
            
            ActionInfoDict.Clear();
        }
        #endregion
    }
}