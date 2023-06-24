using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Infra.Model.Data;
using Infra.Model.Game;
using Infra.Model.Resource;
using Module.Game.Battle;
using Module.Game.Event.Message;
using Module.Game.Map;
using Module.Game.Slot;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Artefact = Infra.Model.Game.Artefact;
using Unit = Infra.Model.Game.Unit;

namespace Module.Game
{
    internal class UIGame : BaseMonoBehaviour, IEventController
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
        
        #region Artefact
        public Transform artefactListTr;
        public GameObject artefactPrefab;
        
        private List<Artefact> ArtefactList { get; set; } = new();
        #endregion
        
        public Spot CurrentSpot { get; set; }
        #endregion
        
        /// <summary>
        /// 씬로드 후 수행되는 첫 이벤트.
        /// </summary>
        private void Start()
        {
            GameManager = GameManager.Instance;
            
            // 재활용 구현된 UI 클래스
            uiMap.Init(this);
            uIBattle.Init(this);
            uIMessageBox.Init(this);
            uIActionSelector.Init(this);
            uiReward.Init(this);
            uiSlot.Init(this);
            
            Init();
        }

        public void Init()
        {
            if (this == null)
            {
                return;
            }
            
            GameData = GameManager.Instance.GameData;

            #region 화면 설정
            SlotService = new SlotService();

            // Slot 배치
            BlockList.Clear();
            BlockList = GameData.UnitList.SelectMany(u => u.HasBlocks).ToList();
            uiSlot.CreateSlot(GameData.SlotWidth, GameData.SlotHeight);

            // Artefact 배치
            ArtefactList.Clear();
            ArtefactList = GameData.ArtefactList;
            foreach (var item in ArtefactList)
            {
                var obj = Instantiate(artefactPrefab, artefactListTr);
                obj.GetComponent<UIArtefact>().Set(item);
            }
            
            // 블럭 효과 초기화 (by artefact and enemy)
            BlockEvents = new BlockEvents((e) => { });
            
            // 화면
            targetSelectGameObject.SetActive(false);

            #endregion
            
            Map();
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

        /// <summary>
        /// 이벤트 종료 시 수행
        /// </summary>
        public void EndEvent(bool isClear = true)
        {
            if (isClear)
            {
                CurrentSpot.UpdateState(SpotState.Clear);
                GameData.Map.UpdateStateRest(CurrentSpot.Depth);
                Map(true);
                uiMap.UpdateMap();

                // 클리어 
                if (CurrentSpot.ChildSpots == null)
                {
                    GameData.DungeonClear();
                    GameManager.DungeonClear();
                }
            }
            else
            {
                GameManager.ClearFail();
            }
        }


        public void SetView()
        {
            
        }

        public void SpinEvent(Action<Block[,]> nextAction = null)
        {
            StartCoroutine(nameof(Spin) , nextAction);
        }

        public void Reward(IEnumerable<Reward> rewards, Action confirmAction = null) => uiReward.Set(rewards, confirmAction);


        public void ExecuteActionSelector(Action nextAction, Func<bool> checkFunc = null)
            => uIActionSelector.Show(nextAction, checkFunc);

        public IEnumerable<Unit> UnitList => GameData.UnitList;

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
            yield return uiSlot.SpinAnimation();
            
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
        IEnumerable<UIEntity> _targets = null;
        public void SelectBattleEntity(TargetType targetType, Action<BattleEntity> action = null)
        {
            BattleEntity entity = null;
            SelectedAction = action;
            targetSelectGameObject.SetActive(true);

            _targets = null;
            switch (targetType)
            {
                case TargetType.Enemy :
                    _targets = uIBattle.UIEnemies;
                    break;
                case TargetType.Unit :
                    _targets = uIBattle.UIUnits;
                    break;
            }

            if (_targets != null)
            {
                foreach (var target in _targets)
                {
                    target.TargetMode(true);
                }
            }
        }


        public void SelectedUIEntityEvent(BattleEntity battleEntity)
        {
            foreach (var target in _targets)
            {
                target.TargetMode(false);
            }
            
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
            Map(false);
            ExecuteEvent(spot);
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
