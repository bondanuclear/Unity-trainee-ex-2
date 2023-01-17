using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BlocksMover : MonoBehaviour
{
   
    public event Action<int> OnBlockMerged;
    public event Action OnLoseStateCheck;
    Vector2[] directions = { Vector2.left, Vector2.up, Vector2.right, Vector2.down};
    NumbersPool numbersPool;
    Helper helper;
    [Inject]
    private void Construct(Helper _helper, NumbersPool _numbersPool)
    {
        helper = _helper;
        numbersPool = _numbersPool;
    }
    public void MoveBlocks(Vector2 direction)
    {
        var dictValues = helper.NodesDictValues();
        var filledNodes = dictValues.Where(n => n.NumberNode != null)
                                    .OrderBy(n => n.transform.position.x)
                                    .ThenBy(n => n.transform.position.y)
                                    .ToList();
        if (direction == Vector2.up || direction == Vector2.right) filledNodes.Reverse();
        // Debug.Log(filledNodes.Count + " FILLED NODES COUNT");
        foreach (var block in filledNodes)
        {
            Node nextBlock;
            Node currBlock = block;
            Vector2 newBlockPos = (Vector2)block.transform.position + direction;
            do
            {

                if (helper.GetDictionary().TryGetValue(newBlockPos, out nextBlock))
                {
                    if (nextBlock.NumberNode != null)
                    {
                        if (nextBlock.NumberNode.Value == currBlock.NumberNode.Value)
                        {
                            Debug.Log("merging logic ");

                            //Destroy(currBlock.NumberNode.gameObject);
                            currBlock.NumberNode.Release();
                            OnBlockMerged?.Invoke(nextBlock.NumberNode.Value);
                            helper.SetNumberNode(currBlock.transform.position, null);
                            helper.GetAndInitNumberNode(nextBlock.transform.position, nextBlock.NumberNode.Value * 2);
                            
                            break;
                        }
                        else if (nextBlock.NumberNode.Value != currBlock.NumberNode.Value)
                        {
                            Debug.Log("Can't move forward");
                            break;
                        }
                    }
                    currBlock.NumberNode.transform.position = nextBlock.transform.position;
                    helper.SetNumberNode(nextBlock.transform.position, currBlock.NumberNode);
                    helper.SetNumberNode((Vector2)nextBlock.transform.position - direction, null);
                    currBlock = nextBlock;
                    newBlockPos += direction;
                }

            }
            while (nextBlock != null);
        }
        // var filledChecker = dictValues.Where(n => n.NumberNode != null)
        //                             .OrderBy(n => n.transform.position.x)
        //                             .ThenBy(n => n.transform.position.y)
        //                             .ToList();
        CheckLoseState(filledNodes);

        //helper.PrintKeysValues();
    }

    private void CheckLoseState(List<Node> filledNodes)
    {
        var freeBlocks = helper.NodesDictValues().Where(b => b.NumberNode == null).ToList();
        if (freeBlocks.Count == 1 || freeBlocks.Count == 0)
        {
            Debug.LogWarning(" careful, low space ");
            if (WillLoseState(filledNodes))
            {
                OnLoseStateCheck?.Invoke();
            }
        }
    }

    private bool WillLoseState(List<Node> filledBlocks)
    { 
        foreach (var filledSpot in filledBlocks)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                Vector2 checkPos = (Vector2)filledSpot.transform.position + directions[i];

                if (!helper.HasKey(checkPos))
                {
                    continue;
                }
                Debug.Log($"i am checking {checkPos}");
                if(helper.GetNode(checkPos).NumberNode == null) 
                {
                    continue;
                } else
                {
                    Debug.Log($"GONNA COMPARE {filledSpot.NumberNode.Value}");
                    Debug.Log($"WITH {helper.GetNode(checkPos).NumberNode.Value}");
                }
                
                if (filledSpot.NumberNode.Value == helper.GetNode(checkPos).NumberNode.Value)
                {
                    return false;
                }
            }
        }
        return true;
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
    
}
