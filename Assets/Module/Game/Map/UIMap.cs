using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Infra.Model.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Map
{
    internal class UIMap : MonoBehaviour
    {
        #region Variables
        private GameManager GameManager { get; set; }
        private UIGame UIGame { get; set; }
        
        private List<UISpot> SpotList { get; set; }
        
        #region External 
        public GameObject spotPrefab;
        public Transform content;
        #endregion
        #endregion
        
        public void Init(UIGame game)
        {
            GameManager ??= GameManager.Instance;
            UIGame = game;
            
            if (content.childCount == 0)
            {
                GenerateMap(GameManager.GameData.Map);
            }
        }

        private void GenerateMap(Spot firstSpot)
        {
            SpotList = new List<UISpot>();
            
            // 깊이별 맵 생성
            // key = depth, value = list
            var dict = new Dictionary<int, List<Spot>>();
            dict.Add(1, new List<Spot>(){firstSpot});
            
            int depth = 2;
            while(dict[depth -1].First().ChildSpots != null)
            {
                dict.Add(depth, new());
                foreach (var parent in dict[depth - 1])
                {
                    foreach (var child in parent.ChildSpots)
                    {
                        if (!dict[depth].Contains(child))
                        {
                            dict[depth].Add(child);
                        }
                    }
                }
                depth++;
            } 
 
            // 오브젝트 생성
            int space = 100; // Spot간 간격
            foreach (var key in dict.Keys)
            {
                var obj = Instantiate(new GameObject(), content);
                
                var c1 = obj.AddComponent<HorizontalLayoutGroup>();
                c1.spacing = space;
                c1.childControlHeight = false;
                c1.childControlWidth = false;

                var c2 = obj.AddComponent<ContentSizeFitter>();
                c2.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                c2.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                foreach (var spot in dict[key])
                {
                    // [TODO] 굳이 Spot을 참조시킬 필요는 없다. 단순 인덱스만 해도 가능하나, 참조가 왜 더 좋을지 생각해 볼 필요가 있음
                    var child = Instantiate(spotPrefab, obj.transform);
                    var uiSpot = child.GetComponent<UISpot>();
                    uiSpot.Init(this, spot);
                    SpotList.Add(uiSpot);
                }
            }
        }

        public void UpdateMap()
        {
            SpotList.ForEach(s => s.Refresh());
        }

        public void SelectSpot(Spot spot)
        {
            
        }
    }
}
