using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class calculateScores : MonoBehaviour
{
    [SerializeField] private Transform startTransform, goalTransform;
    [SerializeField] private static float distance;

    //Scoring System Variables
    private static int currentScore, currentStreak;
    private static int defaultScore = 0,  defaultStreak = 0;
    private static float minDistance = 5f, mid1Distance = 7f, mid2Distance = 9f, mid3Distance = 11f, maxDistance = 13f;
    private static int[] streakMilestones = {10, 20, 30, 40, 50, 60, 70, 80, 90, 100};
    [SerializeField] private static int baseScoreValue = 5;
    private static int earnedCoins, coinMultiplier = 5;
    private static int defaultCoins;


    // Update is called once per frame
    void Update()
    {
        DetectPlayerDistanceToGoal();
    }

    private void DetectPlayerDistanceToGoal() 
    {
        distance = Vector3.Distance(startTransform.position, goalTransform.position);
    }

    public static void AddToStreakAndScore() 
    {
        int streakMultiplier;
        int scoreMultiplier; 

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

        //calculates streak count
        if (distance >= minDistance) currentStreak++;

        currentScore += baseScoreValue * scoreMultiplier * streakMultiplier;
    }

    public static void ResetStreakAndScore() 
    {
        float coinCalculation = currentScore/coinMultiplier;
        earnedCoins = (int)Mathf.Round(coinCalculation);
        // record earned coinds on shop script here
        earnedCoins = defaultCoins;
        currentStreak = defaultStreak;
        currentScore = defaultScore;
    }
}
