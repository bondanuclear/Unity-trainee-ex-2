using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class BlocksMover
{
   
    public event Action<int> OnBlockMerged;
    public event Action OnLoseStateCheck;
    public event Action OnHasMergedTrue;
    Vector2[] directions = { Vector2.left, Vector2.up, Vector2.right, Vector2.down};
    NumbersPool numbersPool;
    Helper helper;

    [Inject]
    public BlocksMover(Helper _helper, NumbersPool _numbersPool)
    {
        helper = _helper;
        numbersPool = _numbersPool;
    }
    public void MoveBlocks(Vector2 direction)
    {
        bool hasMerged = false;
        
        var dictValues = helper.NodesDictValues();
        List<Node> filledNodes = GetFilledNodes(dictValues);
        if (direction == Vector2.up || direction == Vector2.right) filledNodes.Reverse();
        // Debug.Log(filledNodes.Count + " FILLED NODES COUNT");
        foreach (var block in filledNodes)
        {
            Vector2 movePos = block.NumberNode.transform.position;
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
                            // Debug.Log("merging logic ");
                            movePos = currBlock.transform.position;
                            currBlock.NumberNode.Release();
                            helper.SetNumberNode(currBlock.transform.position, null);
                            helper.GetAndInitNumberNode(nextBlock.transform.position, nextBlock.NumberNode.Value * 2);
                            hasMerged = true;
                            // check if we win and increase score
                            OnBlockMerged?.Invoke(nextBlock.NumberNode.Value);
                            break;
                        }
                        else if (nextBlock.NumberNode.Value != currBlock.NumberNode.Value)
                        {
                            //Debug.Log("Can't move forward");
                            movePos = currBlock.transform.position;
                            break;
                        }
                    }
                    helper.SetNumberNode(nextBlock.transform.position, currBlock.NumberNode);
                    helper.SetNumberNode((Vector2)nextBlock.transform.position - direction, null);
                    currBlock = nextBlock;
                    newBlockPos += direction;
                    movePos = nextBlock.transform.position;
                }

            }
            while (nextBlock != null);
            //Debug.Log("Move Pos out of while" + movePos);
            if(currBlock.NumberNode != null)
                currBlock.NumberNode.transform.DOMove(movePos, 0.1f);
        }
        // if we merge - we spawn numbers only ONCE
        if (hasMerged)
        {
            OnHasMergedTrue?.Invoke();
        }

        var filledChecker = GetFilledNodes(dictValues);
        CheckLoseState(filledChecker);

    }

    private static List<Node> GetFilledNodes(List<Node> dictValues)
    {
        return dictValues.Where(n => n.NumberNode != null)
                                    .OrderBy(n => n.transform.position.x)
                                    .ThenBy(n => n.transform.position.y)
                                    .ToList();
    }

    private void CheckLoseState(List<Node> filledNodes)
    {
        var freeBlocks = helper.NodesDictValues().Where(b => b.NumberNode == null).ToList();
        // if we have no space
        // check whether we can move
        // if can't - we lose
        if (freeBlocks.Count == 0)
        {
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
                if(helper.GetNode(checkPos).NumberNode == null) 
                {
                    continue;
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
