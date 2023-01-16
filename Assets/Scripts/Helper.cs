using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public int HelperValue {get; set;} = 4; // test property
    //public List<Node> gridNodes = new List<Node>();
    private Dictionary<Vector2, Node> nodesDict = new Dictionary<Vector2, Node>();
    [SerializeField] Numbers[] numbersColors;
    public Numbers GetNumberByValue(int value)
    {
        return numbersColors.FirstOrDefault(g => value == g.value);
    }
    public bool HasKey(Vector2 coord)
    {
        return nodesDict.ContainsKey(coord);   
    }

    public Node GetNode(Vector2 coord)
    {
        return nodesDict[coord];
    }
    public  Dictionary<Vector2, Node> GetDictionary()
    {
        return nodesDict;
    }
    public void FillDictionary(Vector2 coordinates, Node node)
    {
        this.nodesDict[coordinates] = node;
    }
    public void SetNumberNode(Vector2 coordinates, NumberNode numberNode)
    {
        nodesDict[coordinates].NumberNode = numberNode;
    }
    public List<Node> NodesDictValues()
    {
        return nodesDict.Values.ToList();
    }
    public void GetAndInitNumberNode(Vector2 coordinates, int value)
    {
        nodesDict[coordinates]
                        .NumberNode
                        .InitNumberNode(GetNumberByValue(value));
    }
    public void PrintKeysValues()
    {
        foreach (var item in nodesDict)
        {
            Debug.Log($"{item.Key} Value: {item.Value.NumberNode}");
        }
    }
}
[System.Serializable]
public struct Numbers
{
    public int value;
    public Color numberColor;
}