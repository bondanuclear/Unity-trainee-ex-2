using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class NumberSpawner : MonoBehaviour
{
    static System.Random rnd = new System.Random();
    [SerializeField] int numberToSpawn = 2;
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
        int amountOfSpawnedNumbers = UnityEngine.Random.Range(2,4);
        var dictValues = helper.NodesDictValues();
        var freeSpots = dictValues.Where(s => s.NumberNode == null)
                                .OrderBy(s => rnd.Next())
                                .Take(amountOfSpawnedNumbers)
                                .ToList();
                               
        foreach (var spot in freeSpots)
        {
           
            NumberNode pooledObject = numbersPool.GetFromPool();
            pooledObject.transform.position = spot.transform.position;
            pooledObject.InitNumberNode(helper.GetNumberByValue(numberToSpawn));
            helper.SetNumberNode(spot.transform.position, pooledObject);
        }
        
    }


}

