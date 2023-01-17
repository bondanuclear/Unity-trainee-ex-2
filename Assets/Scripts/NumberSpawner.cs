using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class NumberSpawner : MonoBehaviour
{
    static System.Random rnd = new System.Random();
    [SerializeField] int numberToSpawn = 2;
    [SerializeField] NumberNode numberNode;
    Helper helper;
    NumbersPool numbersPool;
    [Inject]
    private void Construct(Helper _helper, NumbersPool _numbersPool)
    {
        helper = _helper;
        numbersPool = _numbersPool;
    } 
    public void SpawnNumbers()
    {
        Debug.Log("spawning...");
        int amountOfSpawnedNumbers = UnityEngine.Random.Range(2,4);
        var dictValues = helper.NodesDictValues();
        var freeSpots = dictValues.Where(s => s.NumberNode == null)
                                .OrderBy(s => rnd.Next())
                                .Take(amountOfSpawnedNumbers)
                                .ToList();
                               
        foreach (var spot in freeSpots)
        {
            Debug.Log($"Pool is here {numbersPool.transform.position}");
            NumberNode pooledObject = numbersPool.GetFromPool();
            pooledObject.transform.position = spot.transform.position;
            
            //var pooledObject = Instantiate(numberNode, spot.transform.position, Quaternion.identity);
            pooledObject.InitNumberNode(helper.GetNumberByValue(numberToSpawn));
            helper.SetNumberNode(spot.transform.position, pooledObject);
            //spot.NumberNode = num;
        }
        
    }

   

    // private void Update() {
    //     if(Input.GetKeyDown(KeyCode.B))
    //     {
    //         SpawnNumbers();
    //     }
    // }
}

