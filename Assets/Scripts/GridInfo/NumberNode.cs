using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NumberNode : MonoBehaviour
{
    public int Value {get; set;}
    [SerializeField] TextMeshPro NumberText;
    [SerializeField] SpriteRenderer numberRenderer;
    public NumbersPool numbersPool {get; private set;}
    public void InitNumberNode(in Numbers number)
    {
        //Debug.Log($"{number.numberColor} {number.value}");
        Value = number.value;
        NumberText.text = number.value.ToString();
        numberRenderer.color = number.numberColor;
    }
    public void SetPool(NumbersPool pool)
    {
        numbersPool = pool;
    }
    public void Release()
    {
        numbersPool.ReturnToPool(this);
    }
}
