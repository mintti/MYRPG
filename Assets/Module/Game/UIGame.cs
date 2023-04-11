using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Infra.Model.Game;
using Module.Game.Event.Message;
using Module.Game.Map;
using Module.Game.Slot;
using UnityEngine;
using UnityEngine.Serialization;
using Artefact = Infra.Model.Game.Artefact;

namespace Module.Game
{
    internal class UIGame : MonoBehaviour, IEventController
    {
        #region Variables
        private GameManager GameManager { get; set; }
        private GameData GameData { get; set; }
        
        #region Button
        public void B_Spin() => CanDoSpin = true;
        public void B_Map() => Map();
        public void B_Option() => Option();
        #endregion
        
        public UIMap uiMap;
        public UIOption uiOption;
        [FormerlySerializedAs("uIReward")] public UIReward uiReward;
        public UIMessageBox uIMessageBox;
        
        #region Slot
        public UISlot uiSlot;
        private SlotService SlotService { get; set; }
        private List<Block> BlockList { get; set; } = new ();
        
        public bool CanDoSpin { get; set; } = false;
        
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
        
        /// <summary>
        /// 씬로드 후 수행되는 첫 이벤트.
        /// </summary>
        private void Start()
        {
            GameManager = GameManager.Instance;

            uiMap.Init(this);
            Init();
        }

        private void Init()
        {
            GameData = GameManager.Instance.GameData;

            #region 화면 설정

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

            #endregion
        }

        #region GameView/Event-Related
        public void ExecuteEvent(Spot spot)
        {
            var spotEvent = spot.Event;
            var item = Factory.EventFactory(this, spotEvent);
            item.Execute();
        }
        
        #region IEventController
        public IMessageBox MessageBox => uIMessageBox;

        public void StartEvent()
        {
            
        }

        public void EndEvent(string none = null)
        {
            
        }


        public void SetView()
        {
            
        }

        public void SpinEvent(Action nextAction = null)
        {
            StartEvent();
        }

        public void Reward(Reward reward) => uiReward.Init(reward);
        #endregion
        

        #region Spin Event
        private IEnumerable Spin(Action callback = null)
        {
            yield return new WaitUntil(() => CanDoSpin);
            CanDoSpin = false;
            
            // 블럭 지정 및 설정
            var seq = SlotService.GetRandomBlock(BlockList, GameData.SlotWidth, GameData.SlotHeight, BlockEvents);
            uiSlot.SetBlocks(seq);
            
            // 스핀 효과 적용 *Show Animation

            callback?.Invoke();
            yield return null;
        }
        #endregion
        #endregion
        
        #region Map-Related
        /// <summary>
        /// UIMap에서 맵 선택 이벤트 발생시 수행
        /// </summary>
        public void SelectMap(Spot spot)
        {
            ExecuteEvent(spot);
            Map(false);
        }
        #endregion


        #region Default Function
        private void Map(bool state = true)
        {
            uiMap.gameObject.SetActive(state);
        }

        private void Option(bool state = true)
        {
            uiOption.gameObject.SetActive(state);
        }
        #endregion

    }
}
