using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NumbersPool : MonoBehaviour
{
    [SerializeField] int poolSize = 15;
    [SerializeField] NumberNode number;
    
    private Stack<NumberNode> numberStack;
    //public List<NumberNode> testList = new List<NumberNode>();
    private void Awake() {
        SetUp();
        
    }
   

    private void SetUp()
    {
        numberStack = new Stack<NumberNode>();
        NumberNode num = null;
        for(int i = 0; i < poolSize; i++)
        {
            
            //testList.Add(number);
            num = Instantiate(number, this.transform);
            num.numbersPool = this;
            num.gameObject.SetActive(false);
            numberStack.Push(num);
        }
    }
    public NumberNode GetFromPool()
    {
        // if out of numbers
        if(numberStack.Count <= 0)
        {
            var num = Instantiate(number, this.transform);
            num.numbersPool = this;
            return num;
        }
        NumberNode pooledObject = numberStack.Pop();
        pooledObject.gameObject.SetActive(true);
        pooledObject.GetComponent<ParticleSystem>().Play();
        return pooledObject;

    }
    private void PrintStack()
    {
        for(int i = 0; i < numberStack.Count; i++)
        {
            Debug.Log(i + "Number! : ");
        }
    }
    public void ReturnToPool(NumberNode numberNode)
    {
        numberStack.Push(numberNode);
        numberNode.gameObject.SetActive(false);
    }
    private void Update() {
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     GetFromPool();
        // }
    }
}
