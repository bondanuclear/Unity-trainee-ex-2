using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WinState : MonoBehaviour
{
    private const int WIN_NUMBER = 2048;
    [SerializeField] GameObject winPanel;
    BlocksMover blocksMover;
    [Inject]
    private void Construct(BlocksMover _blocksMover)
    {
        blocksMover = _blocksMover;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        blocksMover.OnBlockMerged += CheckWinState;
    }
    void OnDisable()
    {
        blocksMover.OnBlockMerged -= CheckWinState;
    }
    public void CheckWinState(int value)
    {
        if(value == WIN_NUMBER)
        {
            winPanel.SetActive(!winPanel.activeSelf);
            Debug.Log(" You WON! ");
        } 
        else
        {
            Debug.Log(" keep moving on! ");
        }
    }

    
}
