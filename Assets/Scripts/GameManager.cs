using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
     // Singleton instance
    [SerializeField] Text scoreText;
    private int score = 0;

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }

    private void Update()
    {
        UpdateScoring();
    }

    public void AddPoints(int points)
    {
        score += points;

        // add counting to text here
        UpdateScoring();
        Debug.Log("scoring added");
    }

    public void RemovePoints(int points)
    {
        score -= points;

        UpdateScoring();
        Debug.Log("score removed");
    }

    public int Scores
    {
        get {
            return score;
        }

        set {
            score = value;
            UpdateScoring();
        }

    } 

    private void UpdateScoring()
    {
        scoreText.text = score.ToString();
        // Debug.Log("toString");
    }

    // Other game management functions
}
