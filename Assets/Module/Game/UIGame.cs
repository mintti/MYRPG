using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using Infra.Model.Game;
using Module.Game.Slot;
using Module.WorldMap;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Block = Infra.Model.Data.Block;

namespace Module.Game
{
    public class UIGame : MonoBehaviour
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
        private PlayerData PlayerData { get; set; }
        
        

        #endregion
        #region Game Component

        public GameObject resultPopupGameObject;
            
        /// <summary>
        /// 각종 아티펙트, 블럭 설정 및 게임 이벤트가 종료될 때까지 Ture
        /// </summary>
        private bool Setting { get; set; }
        
        #region Slot
        public UISlot uiSlot;
        private List<Block> BlockList { get; set; } = new ();
        
        /// <summary>
        /// 스핀 버튼 선택 후, 스핀 이벤트가 종료될 때 까지 True.
        /// </summary>
        private bool Spinning { get; set; }
        
        /// <summary>
        /// 스핀 종료 플래그. 모든 스핀 이벤트가 종료되었을 때, True
        /// </summary>
        private bool SpinEndFlag { get; set; }
        #endregion

        #region Battle ( Unit )

        public List<CanvasScaler.Unit> UnitList { get; set; } = new List<CanvasScaler.Unit>();
        public List<Enemy> EnemyList { get; set; } = new List<Enemy>();

        #endregion

        #region Artifact
        public GameObject artifactListObject;
        private List<Artifact> ArtifactList { get; set; } = new();
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
            
            GameManager = FindObjectsOfType<GameManager>().First();

            Init(GameManager.PlayerData, GameManager.DungeonType);

            StartCoroutine(nameof(Main));
        }
        
        private void Init(PlayerData data, DungeonType dungeonType)
        {
            PlayerData = data;
            
            // Slot 배치
            BlockList.Clear();
            BlockList = PlayerData.UnitList.SelectMany(u => u.HasBlocks).ToList();
            
            // Artifact 배치
            ArtifactList.Clear();
            ArtifactList = PlayerData.ArtefactList;
            
            // Unit 배치
            UnitList.Clear();
            EnemyList.Clear();
            
            
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
                yield return SpinEndFlag;

                Setting = true;
                if (true)
                {
                    EndGame();
                    yield break;
                }
                
                // 적의 턴
                //EnemyList.ForEach(x=> );
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

            
            
            // 스핀 효과 적용
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
