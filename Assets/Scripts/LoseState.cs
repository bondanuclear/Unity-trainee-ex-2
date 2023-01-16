using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoseState : MonoBehaviour
{
    [SerializeField] GameObject losePanel;
    BlocksMover blocksMover;
    [Inject]
    private void Construct(BlocksMover _blocksMover)
    {
        blocksMover = _blocksMover;
    }
    private void OnEnable() {
        blocksMover.OnLoseStateCheck += ShowLosePanel;
    }
    private void OnDisable()
    {
        blocksMover.OnLoseStateCheck -= ShowLosePanel;
    }
    private void ShowLosePanel()
    {
        losePanel.SetActive(!losePanel.activeSelf);
        
    }

    



}
