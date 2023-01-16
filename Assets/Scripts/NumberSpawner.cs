using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class NumberSpawner : MonoBehaviour
{
    static System.Random rnd = new System.Random();
    [SerializeField] NumberNode numberNode;
    Helper helper;
    [Inject]
    private void Construct(Helper _helper)
    {
        helper = _helper;
    } 
    public void SpawnNumbers()
    {
        int amountOfSpawnedNumbers = Random.Range(2,4);
        var dictKeys = helper.nodesDict.Values;
        var freeSpots = dictKeys.Where(s => s.NumberNode == null)
                                .OrderBy(s => rnd.Next())
                                .Take(amountOfSpawnedNumbers)
                                .ToList();
        foreach (var spot in freeSpots)
        {

            var num = Instantiate(numberNode, spot.transform.position, Quaternion.identity);
            num.InitNumberNode(helper.GetNumberByValue(2));
            spot.NumberNode = num;
        }
        // foreach (var item in helper.nodesDict)
        // {
        //     Debug.Log($"{item.Key} Value: {item.Value.NumberNode}");
        // }
    }
    
    // private void Update() {
    //     if(Input.GetKeyDown(KeyCode.B))
    //     {
    //         SpawnNumbers();
    //     }
    // }
}

