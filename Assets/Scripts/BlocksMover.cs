using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BlocksMover : MonoBehaviour
{
    Helper helper;
    [Inject]
    private void Construct(Helper _helper)
    {
        helper = _helper;    
    }
    public void MoveBlocks(Vector2 direction)
    {
        var dictValues = helper.nodesDict.Values;
        var filledNodes = dictValues.Where(n => n.NumberNode != null)
                                    .OrderBy(n => n.transform.position.x)
                                    .ThenBy(n => n.transform.position.y)
                                    .ToList();
        if(direction == Vector2.up || direction == Vector2.right) filledNodes.Reverse();                                   
        Debug.Log(filledNodes.Count + " FILLED NODES COUNT");
        foreach(var block in filledNodes)
        {
            Node nextBlock;
            Node currBlock = block;
            Vector2 newBlockPos = (Vector2)block.transform.position + direction;
            do{
                
                if(helper.nodesDict.TryGetValue(newBlockPos, out nextBlock))
                {
                    if(nextBlock.NumberNode != null)
                    {
                        if (nextBlock.NumberNode.Value == currBlock.NumberNode.Value)
                        {
                            Debug.Log("merging logic ");
                            
                        }
                        else if (nextBlock.NumberNode.Value != currBlock.NumberNode.Value)
                        {
                            Debug.Log("Can't move forward");
                            
                        }
                    }
                    
                    currBlock.NumberNode.transform.position = nextBlock.transform.position;
                    helper.nodesDict[nextBlock.transform.position].NumberNode = currBlock.NumberNode;
                    helper.nodesDict[(Vector2)nextBlock.transform.position - direction].NumberNode = null; 
                    currBlock = nextBlock;
                    Debug.Log($"current block {currBlock.transform.position} ");
                    newBlockPos += direction;
                    Debug.Log("new block pos " + newBlockPos);
                }    

            }
            while(nextBlock != null);
        }                            
        
        foreach (var item in helper.nodesDict)
        {
            Debug.Log($"{item.Key} Value: {item.Value.NumberNode}");
        }
    }

    public void MoveOneBlock() /// test function
    {
        // Vector2 newBlockPos = (Vector2)block.transform.position + direction;
        // helper.nodesDict.TryGetValue(newBlockPos, out var nextBlock);

        // if (nextBlock != null)
        // {
        //     //Debug.Log(nextBlock.transform.position + " next block position");

        //     if (nextBlock.NumberNode != null)
        //     {
        //         if (block.NumberNode.Value == nextBlock.NumberNode.Value)
        //         {
        //             Destroy(block.NumberNode.gameObject);
        //             block.NumberNode = null;
        //             helper.nodesDict[nextBlock.transform.position]
        //                     .NumberNode
        //                     .InitNumberNode(helper.GetNumberByValue(nextBlock.NumberNode.Value * 2));
        //             continue;

        //         }
        //         else if (block.NumberNode.Value != nextBlock.NumberNode.Value)
        //         {
        //             Debug.Log("YOU SHALL NOT PASS !");
        //             continue;
        //         }
        //     }
        //     block.NumberNode.transform.position = nextBlock.transform.position;

        //     helper.nodesDict[nextBlock.transform.position].NumberNode = block.NumberNode;

        //     block.NumberNode = null;
    }
    public void Test() /// test function
    {
        // Node nextBlock;
        // Node backUpNode = block;
        // Vector2 newBlockPos = (Vector2)block.transform.position + direction;
        // do
        // {
        //     helper.nodesDict.TryGetValue(newBlockPos, out nextBlock);
        //     if (nextBlock != null)
        //     {

        //         if (nextBlock.NumberNode != null)
        //         {

        //             if (backUpNode.NumberNode.Value == nextBlock.NumberNode.Value)
        //             {
        //                 //use object pool instead
        //                 // Destroy(block.NumberNode.gameObject);
        //                 // block.NumberNode = null;
        //                 // helper.nodesDict[nextBlock.transform.position]
        //                 //         .NumberNode
        //                 //         .InitNumberNode(helper.GetNumberByValue(nextBlock.NumberNode.Value * 2));
        //                 continue;

        //             }
        //             else if (backUpNode.NumberNode.Value != nextBlock.NumberNode.Value)
        //             {
        //                 Debug.Log("YOU SHALL NOT PASS !");
        //                 continue;
        //             }
        //         }
        //         Debug.Log("i was here");
        //         backUpNode.NumberNode.transform.position = nextBlock.transform.position;

        //         helper.nodesDict[nextBlock.transform.position].NumberNode = backUpNode.NumberNode;

        //         helper.nodesDict[(Vector2)nextBlock.transform.position - direction].NumberNode = null;
        //         backUpNode = nextBlock;
        //         newBlockPos += direction;
        //         Debug.Log("new block pos " + newBlockPos);
        //     }
        // } while (nextBlock != null);
    }
}
