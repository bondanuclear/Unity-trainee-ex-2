using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] int width = 3;
    [SerializeField] int height = 3;
    [SerializeField] Transform background;
    [SerializeField] Node nodePrefab;
    Helper helper;
    
    [Inject]
    private void Construct(Helper _helper)
    {
        helper = _helper;
    }
    public void GenerateGrid()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Node instantiatedNode = Instantiate(nodePrefab, new Vector2(i - 1, j - 1), Quaternion.identity, transform);
                helper.FillDictionary(instantiatedNode.transform.position, instantiatedNode);        
                //helper.nodesDict[instantiatedNode.transform.position] = instantiatedNode;
            }
        }
        
        GameObject instantiatedBackground = Instantiate(background.gameObject, new Vector2(0, 0), Quaternion.identity);
        //Debug.Log($"Helper is here, check value = {helper.HelperValue} and {helper.nodesDict.Count}");
    }
    
}
