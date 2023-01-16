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
    Helper helper;
    [Inject]
    private void Construct(Helper _helper)
    {
        helper = _helper;    
        
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

                            Destroy(currBlock.NumberNode.gameObject);
                            helper.SetNumberNode(currBlock.transform.position, null);
                            helper.GetAndInitNumberNode(nextBlock.transform.position, nextBlock.NumberNode.Value * 2);
                            OnBlockMerged?.Invoke(nextBlock.NumberNode.Value * 2);
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
                Debug.LogWarning("YOU LOST");
                OnLoseStateCheck?.Invoke();

            }
        }
    }

    private bool WillLoseState(List<Node> filledBlocks)
    {
      
        Debug.Log(filledBlocks.Count + " FILLED SPOTS COUNT. YOU HAVE REACHED 1 or 0 BLANK SPACES");
        foreach (var filledSpot in filledBlocks)
        {
            for (int i = 0; i < directions.Length; i++)
            {
                Vector2 checkPos = (Vector2)filledSpot.transform.position + directions[i];

                if (!helper.HasKey(checkPos))
                {
                    Debug.LogWarning($"Nope, {checkPos} will not do it");
                    continue;
                }
                if (filledSpot.NumberNode.Value == helper.GetNode(checkPos).NumberNode.Value)
                {
                    Debug.LogError($"oooh yeah, {filledSpot.transform.position} and {checkPos} are the same");
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
