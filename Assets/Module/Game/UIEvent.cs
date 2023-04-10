using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Infra.Model.Game;
using Module.Game.Event.Message;
using Module.Game.Slot;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Artefact = Infra.Model.Game.Artefact;
using Block = Infra.Model.Game.Block;
using State = Infra.Model.Game.State;


namespace Module.Game
{
    internal class UIEvent : MonoBehaviour, IEventController
    {
        #region Variables
        private UIGame UIGame { get; set; }
        private GameData GameData { get; set; }
        
        /// <summary>
        /// 각종 아티펙트, 블럭 설정 및 게임 이벤트가 종료될 때까지 Ture
        /// </summary>
        private bool Setting { get; set; }

        #region External
        public GameObject rewardPopupGameObject;

        
        [FormerlySerializedAs("uIMessage")] [FormerlySerializedAs("uIMultipleChoice")] [FormerlySerializedAs("uIDestractor")] public UIMessageBox uIMessageBox;

        #endregion
        #region Slot
        public UISlot uiSlot;
        private SlotService SlotService { get; set; }
        private List<Block> BlockList { get; set; } = new ();
        
        /// <summary>
        /// 스핀 버튼 선택 후, 스핀 이벤트가 종료될 때 까지 True.
        /// </summary>
        private bool Spinning { get; set; }
        
        /// <summary>
        /// 스핀 종료 플래그. 모든 스핀 이벤트가 종료되었을 때, True
        /// </summary>
        private bool SpinEndFlag { get; set; }
        
        private BlockEvents BlockEvents { get; set; } 
        #endregion
        
        #region Battle
        public List<Unit> UnitList { get; set; } = new List<Unit>();
        public List<Enemy> EnemyList { get; set; } = new List<Enemy>();

        #endregion

        #region Artifact
        public Transform artefactListTr;
        public GameObject artefactPrefab;
        private IEventController _eventControllerImplementation;
        private List<Artefact> ArtifactList { get; set; } = new();
        #endregion
        #endregion

        public void Init(UIGame uiGame)
        {
            UIGame = uiGame;
            GameData = GameManager.Instance.GameData;
            Setting = true;
            
            // InitBattle(GameManager.GameData, GameManager.DungeonType);
        }

        #region IEventController

        public IMessageBox IMessageBox => uIMessageBox;
        public void StartEvent()
        {
            
        }

        public void EndEvent()
        {
            
        }


        public void SetView()
        {
            
        }

        public void SpinEvent()
        {
            
        }
        
        public void Reward()
        {
            
        }
        #endregion
        public void SetEvent(Spot spot)
        {
            
            var spotEvent =  spot.Event;
            switch (spotEvent.Type)
            {
                case SpotEventType.Battle :
                    StartCoroutine(nameof(BattleCoroutine), (spotEvent as BattleEvent));
                    break;
                case SpotEventType.Event :
                    
                    break;
                case SpotEventType.Boss :
                    
                    break;
                default:
                    break;
            }
        }
        
        private void OnRewardPopup(Reward reward)
        {
            rewardPopupGameObject.SetActive(true);
            
            // 리워드 내용 표시
        }
        
        #region Battle/Spin
  
        private void InitBattle(GameData data)
        {
            GameData = data;
            SlotService = new SlotService();
            
            // Slot 배치
            BlockList.Clear();
            BlockList = GameData.UnitList.SelectMany(u => u.HasBlocks).ToList();
            uiSlot.Init(this, GameData.SlotWidth, GameData.SlotHeight);
            
            // Artifact 배치
            ArtifactList.Clear();
            ArtifactList = GameData.ArtefactList;            
            foreach (var item in ArtifactList)
            {
                var obj = Instantiate(artefactPrefab, artefactListTr);
                obj.GetComponent<UIArtefact>().Set(item);
            }
            
            // Battle View Setting
            UnitList.Clear();
            EnemyList.Clear();
            
            // 블럭 효과 초기화 (by artefact and enemy)
            BlockEvents = new BlockEvents((e) => { });
        }
        
        private IEnumerator BattleCoroutine(BattleEvent be)
        {
            if (be == null) yield break;
            
            // [TODO] 초기화 로직
            var waitUntil = new WaitUntil(() => SpinEndFlag);
            
            // [TODO] 스테이지 아티펙트 효과 활성화
            while (true)
            {
                SpinEndFlag = false;
                
                // [TODO] 턴 아티펙트 효과 활성화
                

                Setting = false;
                
                // Spin 대기
                yield return new WaitUntil(() => SpinEndFlag);

                Setting = true;
                if (EnemyList.All(e => e.State == State.Die))
                {
                    var reward = GetBattleReword(be);
                    OnRewardPopup(reward);                    
                    break;
                }
                
                // 적의 턴
                foreach (var enemy in EnemyList.Where(e=> e.CanAction()))
                {
                    enemy.Execute();;
                }
            }


            yield return null;
        }
        
        private IEnumerable BossCoroutine()
        {
            
            yield break;
        }
        private Reward GetBattleReword(BattleEvent be)
        {
            return new Reward();
        }

        #endregion
        
        #region Other Event
        
        private IEnumerable EventCoroutine()
        {
            
            yield break;
        }
        private IEnumerable ArtifactCoroutine()
        {
            
            
            yield break;
        }
        
        private IEnumerable RestCoroutine()
        {
            
            yield break;
        }

        #endregion
        
        
        #region Spin Event
        public void Spin()
        {
            if (Setting || Spinning) return;

            Spinning = true;
            
            // 블럭 지정 및 설정
            var seq = SlotService.GetRandomBlock(BlockList, GameData.SlotWidth, GameData.SlotHeight, BlockEvents);
            uiSlot.SetBlocks(seq);
            
            // 스핀 효과 적용 *Show Animation
            
            Spinning = false;
            SpinEndFlag = true;
        }
        #endregion
    }
}
