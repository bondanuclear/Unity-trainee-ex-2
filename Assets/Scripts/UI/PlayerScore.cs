using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;
    BlocksMover blocksMover;
    [Inject]
    private void Construct(BlocksMover _blocksMover)
    {
        blocksMover = _blocksMover;
    }
    private void Awake() {
        scoreText.text = $"Score: {score}";
    }
    private void OnEnable() {
        blocksMover.OnBlockMerged += IncreaseScore;
    }
    private void OnDisable()
    {
        blocksMover.OnBlockMerged -= IncreaseScore;
    }
    private void IncreaseScore(int value)
    {
        score += 1;
        scoreText.text = $"Score: {score}";
    }
}
