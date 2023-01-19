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
        GeneratingGrid, SpawnNumbers, PlayerInput, Moving, Lose, Win
    }

    private const int WINNING_NUMBER = 2048;
    Vector2 moveDir = Vector2.zero;
    GridGenerator gridGenerator;
    NumberSpawner numberSpawner;
    PlayerInput playerInput;
    BlocksMover blocksMover;
    //BlocksMergedSignal blocksMergedSignal;
    GameState gameState;

    [Inject]
    private void Construct(GridGenerator _gridGenerator,
     NumberSpawner _numberSpawner, PlayerInput _playerInput, BlocksMover _blocksMover)
    {   
        //blocksMergedSignal = _blocksMergedSignal;
        gridGenerator = _gridGenerator;
        numberSpawner = _numberSpawner;
        playerInput = _playerInput;
        blocksMover = _blocksMover;
    }
    private void OnEnable() {
        // check if we win
        blocksMover.OnBlockMerged += ProcessWinState;
        // check if we lose
        blocksMover.OnLoseStateCheck += ProcessLost;
        // spawn numbers if blocks merged
        blocksMover.OnHasMergedTrue += ProcessSpawnNumbers;   
    }
    private void OnDisable() {
        blocksMover.OnBlockMerged -= ProcessWinState;
        blocksMover.OnHasMergedTrue -= ProcessSpawnNumbers;
        blocksMover.OnLoseStateCheck -= ProcessLost;
       
    }
    void Start()
    {
        //blocksMergedSignal.SayHello();
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
                ProcessSpawnNumbers();
                break;

            case GameState.PlayerInput: break; 

            case GameState.Moving:
                ProcessMoveBlocks();
                break;
            case GameState.Lose:
                this.enabled = false;
                break;
            case GameState.Win:
                this.enabled = false;
                break;    
        }
    }

    
    private void ProcessGenerateGrid()
    {
        gridGenerator.GenerateGrid();
        ChangeState(GameState.SpawnNumbers);
    }
    
    private void ProcessSpawnNumbers()
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
        ChangeState(GameState.Lose); 
    }
    private void ProcessWinState(int value)
    {
        if(value == WINNING_NUMBER)
            ChangeState(GameState.Win);
       
    }
    private void Update() {
        
        if(gameState != GameState.PlayerInput) return;
        //Debug.Log($"Waiting for input! Move input is {moveDir}");
        //moveDir = playerInput.GetPlayerInput();
        //Debug.Log("Blocks should move in direction " + playerInput.GetMobileInput());
        //Debug.Log("move dir " + playerInput.BlocksDirection(playerInput.GetMobileInput()));
        moveDir = playerInput.BlocksDirection(playerInput.GetMobileInput());
        //Debug.Log($"Input found! You want to move {moveDir}");
        // if player performed input
        if(moveDir != Vector2.zero)
            ChangeState(GameState.Moving);
                
        
        
    }
}


