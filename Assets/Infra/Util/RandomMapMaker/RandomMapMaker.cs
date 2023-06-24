using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using UnityEngine;
using Random = System.Random;

namespace Infra.Util.RandomMapMaker
{
    internal class RandomMapMaker
    {
        Random _random;
        public List<Spot> Generate(int finalDepth, int maxWidth)
        {
            int depth = 1;
            var firstSpot = new Spot(1, depth){State = SpotState.Do};
            var allCollection = new List<Spot>(){firstSpot};
            _random = new Random();
            
            // 뎁스별 Spot 갯수 설정
            var widthList = new List<int>();
            int sum = 0;
            int count;
            for (var i = 1; i < finalDepth - 1; i++)
            {
                count = _random.Next(2, maxWidth + 1);
                sum += count;
                widthList.Add(count);
            }
            
            // 첫 노드 연결
            var currentSpots = new List<Spot>();
            var nextSpots = new List<Spot>();
            int index = 2;
            depth = 2;
            for (int i = 0; i < widthList.First(); i++)
            {
                var spot = new Spot(index++, depth);
                currentSpots.Add(spot);
                allCollection.Add(spot);
                firstSpot.Connect(spot);
            }
            
            // 다음 노드 생성 및 연결
            depth = 3;
            for (int i = 0, cnt = widthList.Count -1 ; i < cnt; i++)
            {
                nextSpots.Clear();
                for (int j = 0; j < widthList[i]; j++)
                {
                    var spot = new Spot(index++, depth + i);
                    nextSpots.Add(spot);
                    allCollection.Add(spot);
                }
        
                ConnectEdge(currentSpots, nextSpots); // 연결
        
                currentSpots.Clear();
                nextSpots.ForEach(s => currentSpots.Add(s)) ;
            }
            
            // depth - 1 에 위치한 노드들을 마지막 노드에 연결
            var lastSpot = new Spot(index, finalDepth);
            currentSpots.ForEach(n => n.Connect(lastSpot));
            allCollection.Add(lastSpot);
            
            return allCollection;
        }
        
        private void ConnectEdge(List<Spot> Currents, List<Spot> Childs)
        {
            int pivot = 0;
            for (int i = 0, cnt = Currents.Count; i < cnt; i++)
            {
                int canBranchCount = _random.Next(pivot, Childs.Count);
        
                for (int j = pivot ; j <= canBranchCount; j++)
                {
                    Currents[i].Connect(Childs[j]);
                }

                if (canBranchCount < Childs.Count)
                    pivot = _random.Next(canBranchCount, canBranchCount + 1);
                else pivot = canBranchCount;
            }
        }
        #region interface
        // public T Generate<T>(int depth, int maxWidth) where T : INode, new()
        // {
        //     T firstNode = new T(){Index = 1};
        //     T lastNode = new T(){Index = depth};
        //     _random = new Random();
        //     
        //     // 뎁스별 Spot 갯수 설정
        //     var widthList = new List<int>();
        //     int sum = 0;
        //     int count;
        //     for (var i = 1; i < depth - 1; i++)
        //     {
        //         count = _random.Next(2, maxWidth + 1);
        //         sum += count;
        //         widthList.Add(count);
        //     }
        //     
        //     // 첫 노드 연결
        //     var currentNodes = new List<T>();
        //     var nextNodes = new List<T>();
        //     int index = 2;
        //     for (int i = 0; i < widthList.First(); i++)
        //     {
        //         var node = new T(){Index = index++};
        //         currentNodes.Add(node);
        //         firstNode.Connect(node);
        //     }
        //     
        //     // 다음 노드 생성 및 연결
        //     for (int i = 2; i < depth - 2; i++)
        //     {
        //         nextNodes.Clear();
        //         for (int j = 0; j < widthList[i]; j++)
        //         {
        //             nextNodes.Add(new T(){Index = index++});
        //         }
        //
        //         ConnectEdge(currentNodes, nextNodes); // 연결
        //
        //         currentNodes = nextNodes;
        //     }
        //     
        //     // depth - 1 에 위치한 노드들을 마지막 노드에 연결 
        //     currentNodes.ForEach(n => n.Connect(lastNode));
        //     
        //     return firstNode;
        // }
        //
        // private void ConnectEdge<T>(List<T> Currents, List<T> Childs) where T : INode
        // {
        //     int pivot = 0;
        //     for (int i = 0, cnt = Currents.Count; i < cnt; i++)
        //     {
        //         int canBranchCount = _random.Next(pivot, Childs.Count);
        //
        //         for (int j = pivot ; j <= canBranchCount; j++)
        //         {
        //             Currents[i].Connect(Childs[j]);
        //         }
        //
        //         pivot = canBranchCount;
        //
        //     }
        // }
        #endregion

        #region Base Class
        // public Node Generate(int depth, int maxWidth)
        // {
        //     Node firstNode = new Node(1);
        //     Node lastNode = new Node(depth);
        //     _random = new Random();
        //     
        //     // 뎁스별 Spot 갯수 설정
        //     var widthList = new List<int>();
        //     int sum = 0;
        //     int count;
        //     for (var i = 1; i < depth - 1; i++)
        //     {
        //         count = _random.Next(2, maxWidth + 1);
        //         sum += count;
        //         widthList.Add(count);
        //     }
        //     
        //     // 첫 노드 연결
        //     var currentNodes = new List<Node>();
        //     var nextNodes = new List<Node>();
        //     int index = 2;
        //     for (int i = 0; i < widthList.First(); i++)
        //     {
        //         var node = new Node(index++);
        //         currentNodes.Add(node);
        //         firstNode.Connect(node);
        //     }
        //     
        //     // 다음 노드 생성 및 연결
        //     for (int i = 2; i < depth - 2; i++)
        //     {
        //         nextNodes.Clear();
        //         for (int j = 0; j < widthList[i]; j++)
        //         {
        //             nextNodes.Add(new Node(index++));
        //         }
        //
        //         ConnectEdge(currentNodes, nextNodes); // 연결
        //
        //         currentNodes = nextNodes;
        //     }
        //     
        //     // depth - 1 에 위치한 노드들을 마지막 노드에 연결 
        //     currentNodes.ForEach(n => n.Connect(lastNode));
        //     
        //     return firstNode;
        // }
        //
        // private void ConnectEdge(List<Node> Currents, List<Node> Childs)
        // {
        //     int pivot = 0;
        //     for (int i = 0, cnt = Currents.Count; i < cnt; i++)
        //     {
        //         int canBranchCount = _random.Next(pivot, Childs.Count);
        //
        //         for (int j = pivot ; j <= canBranchCount; j++)
        //         {
        //             Currents[i].Connect(Childs[j]);
        //         }
        //
        //         pivot = canBranchCount;
        //
        //     }
        // }
        #endregion
        
    }
}