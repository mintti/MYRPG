using System.Collections.Generic;

namespace Infra.Util.RandomMapMaker
{
    // public interface INode
    // {
    //     public int Index { get; set; }
    //     
    //     public List<INode> ChildNodes { get; set; }
    //
    //     public void Connect(INode child)
    //     {
    //         ChildNodes ??= new List<INode>();
    //         ChildNodes.Add(child);
    //     }
    // }
    
    public class Node
    {
        public int Index { get; protected set; }
        
        public List<Node> ChildNodes { get; set; }
    
        public Node(int index)
        {
            Index = index;
        }
        
    
        public void Connect(Node child)
        {
            ChildNodes ??= new List<Node>();
            ChildNodes.Add(child);
        }
    }
}