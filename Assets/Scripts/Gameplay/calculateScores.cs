using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class calculateScores : MonoBehaviour
{
    [SerializeField] private GameObject start, goal;
    private Transform startTransform, goalTransform;
    [SerializeField] private float distance;
    [SerializeField] private string goalTag = "goal", startTag = "object";

    //Scoring System Variables
    private static int currentScore, currentStreak;
    private static int defaultScore = 0,  defaultStreak = 0;
    private float minDistance = 5f, mid1Distance = 7f, mid2Distance = 9f, mid3Distance = 11f, maxDistance = 13f;
    private int[] streakMilestones = {10, 20, 30, 40, 50, 60, 70, 80, 90, 100};
    [SerializeField] private static int baseScoreValue = 5;
    private static int streakMultiplier, scoreMultiplier; 
    private static bool minDistReached = false;

    //shop variables
    private int earnedCoins, defaultCoins, currentCoinCount; 
    private int coinMultiplier = 5;


    void Start() 
    {
        goal = GameObject.FindWithTag(goalTag);
        start = GameObject.FindWithTag(startTag);
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayerDistanceToGoal();
    }

    private void DetectPlayerDistanceToGoal() 
    {
        distance = Vector3.Distance(startTransform.position, goalTransform.position);
    }

    private void GetObjectTransforms() 
    {
        goal = GameObject.FindWithTag(goalTag);
        start = GameObject.FindWithTag(startTag);
        goalTransform = goal.transform;
        startTransform = start.transform;
    }

    private void CalculateStreakScoreMultiplier() 
    {
        //applies a score modifier based on the current streak count from the goal
        if (currentStreak >= streakMilestones[9]) streakMultiplier = 10;
        else if (currentStreak >= streakMilestones[8]) streakMultiplier = 9;
        else if (currentStreak >= streakMilestones[7]) streakMultiplier = 8;
        else if (currentStreak >= streakMilestones[6]) streakMultiplier = 7;
        else if (currentStreak >= streakMilestones[5]) streakMultiplier = 6;
        else if (currentStreak >= streakMilestones[4]) streakMultiplier = 5;
        else if (currentStreak >= streakMilestones[3]) streakMultiplier = 4;
        else if (currentStreak >= streakMilestones[2]) streakMultiplier = 3;
        else if (currentStreak >= streakMilestones[1]) streakMultiplier = 2;
        else streakMultiplier = 1;

        //applies a score modifier based on distance from the goal
        if (distance >= maxDistance) scoreMultiplier = 5;
        else if (distance >= mid3Distance) scoreMultiplier = 4;
        else if (distance >= mid2Distance) scoreMultiplier = 3;
        else if (distance >= mid1Distance) scoreMultiplier = 2;
        else scoreMultiplier = 1;

        if (distance >= minDistance) minDistReached = true;
        else minDistReached = false;

    }

    public static void AddToStreakAndScore() 
    {
        currentScore += baseScoreValue * scoreMultiplier * streakMultiplier;
        
        //calculates streak count
        if (minDistReached) currentStreak++;
    }

    public static void ResetStreakAndScore() 
    {
        Analytics.CustomEvent("Session Streak", new Dictionary<string, object> {
            { "Total Score", currentScore },
            { "Current Streak", currentStreak }
        });
        
        currentStreak = defaultStreak;
        currentScore = defaultScore;
    }

    private void AddCoins() 
    {
        currentCoinCount = PlayerPrefs.GetInt("Currency", 0);
        float coinCalculation = currentScore/coinMultiplier;
        earnedCoins = (int)Mathf.Round(coinCalculation);
        
        currentCoinCount += earnedCoins;
        PlayerPrefs.SetInt("Currency", currentCoinCount);
        earnedCoins = defaultCoins;
    }
}
