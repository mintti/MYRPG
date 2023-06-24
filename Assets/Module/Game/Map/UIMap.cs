using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Module.Game.Map
{
    internal class UIMap : BaseMonoBehaviour 
    {
        #region Variables
        public UIGame UIGame { get; set; }
        private List<UISpot> SpotList { get; set; }
        
        #region External 
        public GameObject spotPrefab;
        public Transform content;
        #endregion
        #endregion
        
        public void Init(UIGame game)
        {
            UIGame = game;
            GenerateMap(GameManager.Instance.GameData.Map);
        }
        
        List<(HorizontalLayoutGroup layoutGroup, ContentSizeFitter fileter)> controlList = new ();
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
                var obj = new GameObject();
                obj.transform.SetParent(content);
                
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
                
                controlList.Add((c1, c2));
            }
         
            gameObject.SetActive(true);
            StartCoroutine(DrawLine());
        }

        IEnumerator DrawLine()
        {
            yield return new WaitForSeconds(0.1f);
            // 라인 생성을 위해 이동
            var cttFilter = content.GetComponent<ContentSizeFitter>();
            var cttLayGroup = content.GetComponent<VerticalLayoutGroup>();

            cttFilter.enabled = false;
            cttLayGroup.enabled = false;
            foreach (var pair in controlList)
            {
                pair.fileter.enabled = false;
                pair.layoutGroup.enabled = false;
            }

            SpotList.ForEach(ui => ui.transform.parent = content);
            controlList.ForEach(item => Destroy(item.fileter.gameObject));
            controlList.Clear();
            
            // Spot간 라인 생성
            SpotList.ForEach(x=> x.CreateLine(SpotList));

            yield return null;
        }

        public void UpdateMap()
        {
            foreach (var spot in SpotList)
            {
                
            }
            
            SpotList.ForEach(s => s.Refresh());
        }
    }
}
