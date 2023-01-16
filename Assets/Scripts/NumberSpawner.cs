using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class NumberSpawner : MonoBehaviour
{
    static System.Random rnd = new System.Random();
    
    Vector2[] directions = {Vector2.left, Vector2.up, Vector2.right, Vector2.down};
    [SerializeField] NumberNode numberNode;
    Helper helper;
    [Inject]
    private void Construct(Helper _helper)
    {
        helper = _helper;
    } 
    public void SpawnNumbers()
    {
        int amountOfSpawnedNumbers = UnityEngine.Random.Range(2,4);
        var dictValues = helper.NodesDictValues();
        var freeSpots = dictValues.Where(s => s.NumberNode == null)
                                .OrderBy(s => rnd.Next())
                                .Take(amountOfSpawnedNumbers)
                                .ToList();
                               
        foreach (var spot in freeSpots)
        {

            var num = Instantiate(numberNode, spot.transform.position, Quaternion.identity);
            num.InitNumberNode(helper.GetNumberByValue(2));
            helper.SetNumberNode(spot.transform.position, num);
            //spot.NumberNode = num;
        }
        // if(freeSpots.Count == 1 || freeSpots.Count == 0)
        // {
        //     Debug.LogWarning(" careful, low space ");
        //     if(WillLoseState(dictValues))
        //     {
        //         Debug.LogWarning("YOU LOST");
        //         //OnLoseStateCheck?.Invoke();
        //     }
        // }
        //var filledSpots = dictValues.Where(s => s.NumberNode != null).ToList();
        //helper.PrintKeysValues();
    }

    // private bool WillLoseState(List<Node> dictValues)
    // {
    //     var filledSpots = dictValues.Where(s => s.NumberNode != null).ToList();
    //     Debug.Log(filledSpots.Count + " FILLED SPOTS COUNT. YOU HAVE REACHED 1 or 0 BLANK SPACES");
    //     foreach (var filledSpot in filledSpots)
    //     {
    //         for (int i = 0; i < directions.Length; i++)
    //         {
    //             Vector2 checkPos = (Vector2)filledSpot.transform.position + directions[i];
                
    //             if(!helper.HasKey(checkPos))
    //             {
    //                 Debug.LogWarning($"Nope, {checkPos} will not do it");
    //                 continue;
    //             } 
    //             if (filledSpot.NumberNode.Value == helper.GetNode(checkPos).NumberNode.Value)
    //             {
    //                 Debug.LogError($"oooh yeah, {filledSpot.transform.position} and {checkPos} are the same");
    //                 return false;
    //             }
                 
    //         }
    //     }
    //     return true;
    // }

    // private void Update() {
    //     if(Input.GetKeyDown(KeyCode.B))
    //     {
    //         SpawnNumbers();
    //     }
    // }
}

