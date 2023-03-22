using System.Collections.Generic;
using System.Linq;
using Infra.Model.Data;
using UnityEngine;
using Random = System.Random;

namespace Infra.Util.RandomMapMaker
{
    public class RandomMapMaker
    {
        Random _random;
        public Spot Generate(int depth, int maxWidth)
        {
            var firstSpot = new Spot(1);
            var lastSpot = new Spot(depth);
            _random = new Random();
            
            // 뎁스별 Spot 갯수 설정
            var widthList = new List<int>();
            int sum = 0;
            int count;
            for (var i = 1; i < depth - 1; i++)
            {
                count = _random.Next(2, maxWidth + 1);
                sum += count;
                widthList.Add(count);
            }
            
            // 첫 노드 연결
            var currentSpots = new List<Spot>();
            var nextSpots = new List<Spot>();
            int index = 2;
            for (int i = 0; i < widthList.First(); i++)
            {
                var spot = new Spot(index++);
                currentSpots.Add(spot);
                firstSpot.Connect(spot);
            }
            
            // 다음 노드 생성 및 연결
            for (int i = 2; i < depth - 2; i++)
            {
                nextSpots.Clear();
                for (int j = 0; j < widthList[i]; j++)
                {
                    nextSpots.Add(new Spot(index++));
                }
        
                ConnectEdge(currentSpots, nextSpots); // 연결
        
                currentSpots = nextSpots;
            }
            
            // depth - 1 에 위치한 노드들을 마지막 노드에 연결 
            currentSpots.ForEach(n => n.Connect(lastSpot));
            
            return firstSpot;
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
        
                pivot = canBranchCount;
        
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