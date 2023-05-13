using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Infra.Model.Data;
using Infra.Model.Game;
using Module.Game.Battle;
using Module.Game.Event.Message;
using Module.Game.Map;
using Module.Game.Slot;
using UnityEngine;
using UnityEngine.Serialization;
using Artefact = Infra.Model.Game.Artefact;
using Unit = Infra.Model.Game.Unit;

namespace Module.Game
{
    internal class UIGame : MonoBehaviour, IEventController
    {
        #region Variables
        private GameManager GameManager { get; set; }
        public GameData GameData { get; private set; }
        
        #region Button
        public void B_Spin() => CanDoSpin = true;
        public void B_Map() => Map();
        public void B_Option() => Option();
        #endregion

        public UIBattle uIBattle;
        public UIMap uiMap;
        public UIOption uiOption;
        public UIReward uiReward;
        public UIMessageBox uIMessageBox;
        
        #region Slot
        public UISlot uiSlot;
        public UIActionSelector uIActionSelector;
        public SlotService SlotService { get; set; }
        private List<Block> BlockList { get; set; } = new ();
        
        public bool CanDoSpin { get; set; } = false;
        
        private BlockEvents BlockEvents { get; set; } 
        #endregion
        
        #region Artifact
        public Transform artefactListTr;
        public GameObject artefactPrefab;
        
        private List<Artefact> ArtifactList { get; set; } = new();
        #endregion
        
        public Spot CurrentSpot { get; set; }
        #endregion
        
        /// <summary>
        /// 씬로드 후 수행되는 첫 이벤트.
        /// </summary>
        private void Start()
        {
            GameManager = GameManager.Instance;

            // 초기화
            uiMap.Init(this);
            uIBattle.Init(this);
            uIActionSelector.Init(this);
            Init();
            
            Map();
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
            
            // 블럭 효과 초기화 (by artefact and enemy)
            BlockEvents = new BlockEvents((e) => { });
            
            
            // 화면
            targetSelectGameObject.SetActive(false);

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

        public void EndEvent(string none = null)
        {
            
        }


        public void SetView()
        {
            
        }

        public void SpinEvent(Action<Block[,]> nextAction = null)
        {
            StartCoroutine(nameof(Spin) , nextAction);
        }

        public void Reward(Reward reward) => uiReward.Init(reward);


        public void ExecuteActionSelector(Action nextAction, Func<bool> checkFunc = null)
            => uIActionSelector.Show(nextAction, checkFunc);
        #endregion
        

        #region Spin Event
        private IEnumerator Spin(Action<Block[,]> callback = null)
        {
            yield return new WaitUntil(() => CanDoSpin);
            CanDoSpin = false;

            int slotWidth = GameData.SlotWidth;
            int slotHeight = GameData.SlotHeight;
            // 블럭 지정 및 설정
            var list = SlotService.GetRandomBlock(BlockList, slotWidth, slotHeight, BlockEvents).ToList();
            uiSlot.SetBlocks(list);
            
            // 스핀 효과 적용 *Show Animation
            uiSlot.SpinAnimation();
            
            // 결과 전달
            if (callback != null)
            {
                var blocks = new Block[slotHeight, slotWidth];
                for (int i = 0, cnt = list.Count(); i < cnt; i++)
                {
                    blocks[i / slotWidth, i % slotWidth] = list[i];
                }
                callback?.Invoke(blocks);
            }
            
            yield return null;
        }
        #endregion

        #region Target Selected Event

        public GameObject targetSelectGameObject;

        private Action<BattleEntity> SelectedAction { get; set; }
        /// <summary>
        /// 대상을 지정하기 위한 세팅
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public void SelectBattleEntity(TargetType targetType, Action<BattleEntity> action = null)
        {
            BattleEntity entity = null;
            SelectedAction = action;
            targetSelectGameObject.SetActive(true);

            IEnumerable<UIEntity> targets = null;
            switch (targetType)
            {
                case TargetType.Enemy :
                    targets = uIBattle.UIEnemies;
                    break;
                case TargetType.Unit :
                    targets = uIBattle.UIUnits;
                    break;
            }

            if (targets != null)
            {
                foreach (var target in targets)
                {
                    target.TargetMode(true);
                }
            }
        }


        public void SelectedUIEntityEvent(BattleEntity battleEntity)
        {
            SelectedAction?.Invoke(battleEntity);
            SelectedAction = null;
            CancelSelectEntity();
        }

        private void CancelSelectEntity()
        {
            foreach (var uiEntity in uIBattle.UIEnemies.Concat(uIBattle.UIUnits))
            {
                uiEntity.TargetMode(false);
            }
            
            targetSelectGameObject.SetActive(false);
        }
        #endregion
        #endregion
        
        #region Map-Related
        /// <summary>
        /// UIMap에서 맵 선택 이벤트 발생시 수행
        /// </summary>
        public void SelectMap(Spot spot)
        {
            CurrentSpot = spot;
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
