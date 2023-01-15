using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public int HelperValue {get; set;} = 4; // test property
    //public List<Node> gridNodes = new List<Node>();
    public Dictionary<Vector2, Node> nodesDict = new Dictionary<Vector2, Node>();
    [SerializeField] Numbers[] numbersColors;
    public Numbers GetNumberByValue(int value)
    {
        return numbersColors.FirstOrDefault(g => value == g.value);
    }
    public void FillDictionary(Vector2 coordinates, Node node)
    {
        this.nodesDict[coordinates] = node;
    }
    // public void SetNumberNode(Node node)
    // {

    // }
}
[System.Serializable]
public struct Numbers
{
    public int value;
    public Color numberColor;
}