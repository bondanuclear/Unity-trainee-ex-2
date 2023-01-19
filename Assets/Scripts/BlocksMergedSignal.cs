using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BlocksMergedSignal 
{
    static System.Random rnd = new System.Random();
    [SerializeField] int numberToSpawn = 2;
    Helper helper;

    NumbersPool numbersPool;
    public BlocksMergedSignal(Helper _helper, NumbersPool _numbersPool)
    {
        numbersPool = _numbersPool;
        helper = _helper;
    }
    public void SayHello()
    {
        Debug.Log("HELLO FROM NEW SPAWNER AHAHA");
    }
    public void SpawnNumbers()
    {
        int amountOfSpawnedNumbers = UnityEngine.Random.Range(2, 4);
        var dictValues = helper.NodesDictValues();
        var freeSpots = dictValues.Where(s => s.NumberNode == null)
                                .OrderBy(s => rnd.Next())
                                .Take(amountOfSpawnedNumbers)
                                .ToList();

        foreach (var spot in freeSpots)
        {
            Debug.Log("BLOCKS MERGED SIGNAL IS NOW SPAWNING AHAHAHA");    
            NumberNode pooledObject = numbersPool.GetFromPool();
            pooledObject.transform.position = spot.transform.position;
            pooledObject.InitNumberNode(helper.GetNumberByValue(numberToSpawn));
            helper.SetNumberNode(spot.transform.position, pooledObject);
        }

    }
}
