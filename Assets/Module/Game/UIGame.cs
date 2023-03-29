using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Game;
using Module.Game.Battle;
using Module.Game.Map;
using Module.Game.Slot;
using Module.WorldMap;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

using Block = Infra.Model.Game.Block;
using State = Infra.Model.Game.State;

namespace Module.Game
{
    internal class UIGame : MonoBehaviour
    {
        #region Variables
        private GameManager GameManager { get; set; }

        #region Button
        public void B_Spin() => Spin();
        public void B_Map() => Map();
        public void B_Option() => Option();

        public void B_ResultPopupConfirm() => End();
        #endregion

        #region Game
        
        private GameData GameData { get; set; }
    
        #endregion
        #region Game Component

        public UIMap uiMap;
        public GameObject resultPopupGameObject;
            
        /// <summary>
        /// 각종 아티펙트, 블럭 설정 및 게임 이벤트가 종료될 때까지 Ture
        /// </summary>
        private bool Setting { get; set; }
        
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

        public UIBattle uIBattle;
        public List<Unit> UnitList { get; set; } = new List<Unit>();
        public List<Enemy> EnemyList { get; set; } = new List<Enemy>();

        #endregion

        #region Artifact
        public Transform artefactListTr;
        public GameObject artefactPrefab;
        private List<Artefact> ArtifactList { get; set; } = new();
        #endregion
        #endregion
        #endregion


        #region Game
        /// <summary>
        /// 씬로드 후 수행되는 첫 이벤트.
        /// </summary>
        private void Start()
        {
            Setting = true;

            GameManager = GameManager.Instance;
            
            // Init(GameManager.GameData, GameManager.DungeonType);
            uiMap.Init(this);
            
            StartCoroutine(nameof(Main));
        }
        
        private void Init(GameData data)
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
            uIBattle.Init(this);
            
            // 블럭 효과 초기화 (by artefact and enemy)
            BlockEvents = new BlockEvents((e) => { });
            
            // 등등 초기화
            resultPopupGameObject.SetActive(false);
        }

        private IEnumerator Main()
        {
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
                    EndGame();
                    yield break;
                }
                
                // 적의 턴
                foreach (var enemy in EnemyList.Where(e=> e.CanAction()))
                {
                    enemy.Execute();;
                }
            }


            yield return null;
        }

        private void EndGame()
        {
            // 보상 정산
            
            
            
            // 팝업에 표시
            resultPopupGameObject.SetActive(true);
            
            // 팝업 내 보상 결과 리스트에 표시 필요 <= 
        }
        
        private void End()
        {
            //GameManager
        }
        #endregion

        #region Spin Event
        
        private void Spin()
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

        #region Map


        private void Map()
        {
            
        }

        #endregion
        
        #region Option

        private void Option()
        {
            
        }
        
        

        #endregion

    }
}
