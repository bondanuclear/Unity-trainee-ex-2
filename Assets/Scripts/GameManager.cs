using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{   
    
    enum GameState
    {
        GeneratingGrid, SpawnNumbers, PlayerInput, Moving, Lose
    }
    Vector2 moveDir = Vector2.zero;
    Helper helper;
    GridGenerator gridGenerator;
    NumberSpawner numberSpawner;
    PlayerInput playerInput;
    BlocksMover blocksMover;
    GameState gameState;

    [Inject]
    private void Construct(Helper _helper, GridGenerator _gridGenerator,
     NumberSpawner _numberSpawner, PlayerInput _playerInput, BlocksMover _blocksMover)
    {
        helper = _helper;    
        gridGenerator = _gridGenerator;
        numberSpawner = _numberSpawner;
        playerInput = _playerInput;
        blocksMover = _blocksMover;
    }
    private void OnEnable() {
        blocksMover.OnBlockMerged += ProcessSpawnNumbers;
        blocksMover.OnLoseStateCheck += ProcessLost;
    }
    private void OnDisable() {
        blocksMover.OnBlockMerged -= ProcessSpawnNumbers;
        blocksMover.OnLoseStateCheck -= ProcessLost;
    }
    void Start()
    {
        ChangeState(GameState.GeneratingGrid);
       
    }

    private void ChangeState(GameState newState)
    {
        gameState = newState;
        switch (gameState)
        {
            case GameState.GeneratingGrid:
                ProcessGenerateGrid();
                break;

            case GameState.SpawnNumbers:
                ProcessSpawnNumbers(0);
                break;

            case GameState.PlayerInput: break; 

            case GameState.Moving:
                ProcessMoveBlocks();
                break;

            case GameState.Lose:

            break;
        }
    }

    
    private void ProcessGenerateGrid()
    {
        gridGenerator.GenerateGrid();
        ChangeState(GameState.SpawnNumbers);
    }
    
    private void ProcessSpawnNumbers(int value)
    {
        numberSpawner.SpawnNumbers();
        ChangeState(GameState.PlayerInput);
    }
    private void ProcessMoveBlocks()
    {
        blocksMover.MoveBlocks(moveDir);
        ChangeState(GameState.PlayerInput);
    }
    
    private void ProcessLost()
    {
        this.enabled = false;
        ChangeState(GameState.Lose);
        
    }
    private void Update() {
        if(gameState != GameState.PlayerInput) return;
        //Debug.Log("updating");
     
        //Debug.Log($"Waiting for input! Move input is {moveDir}");
        moveDir = playerInput.GetPlayerInput();
        //Debug.Log($"Input found! You want to move {moveDir}");
        // if player performed input
        if(moveDir != Vector2.zero)
            ChangeState(GameState.Moving);
                
        
        
    }
}


