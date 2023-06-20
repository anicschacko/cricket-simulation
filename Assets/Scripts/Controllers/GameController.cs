using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : AbstractController
{
    [SerializeField] private GameConfigScriptable gameConfig;
    [SerializeField] private CMMenuBehaviour menuBehaviour;
    [SerializeField] private CMBallBehaviour ballBehaviour;
    [SerializeField] private CMBatBehaviour batBehaviour;
    [SerializeField] private CMScoreBehaviour scoreBehaviour;

    private bool isBowlingAutomated = false;
    private int currentBallTypeId;
    private int ballCount = 0;
    private int runs = 0, wickets = 0;

    public override void FetchConfig()
    {
        
    }

    public override void AddListeners()
    {
        GameEvents.OnClickPlay += PlayButtonClicked;
        GameEvents.OnSelectedBallType += BallTypeSelected;
        GameEvents.OnSelectedBallGrid += BallGridSelected;
        GameEvents.OnSelectedRunsGrid += RunToScoreSelected;
        GameEvents.OnBallPhaseComplete += BallPhaseCompleted;
        GameEvents.OnBatPhaseComplete += BatPhaseCompleted;
    }

    private void PlayButtonClicked(bool bowlingAutomated)
    {
        isBowlingAutomated = bowlingAutomated;
        ballBehaviour.Init(bowlingAutomated, gameConfig);
        scoreBehaviour.Init(gameConfig);
    }

    private void BallTypeSelected(int id)
    {
        currentBallTypeId = id;
        ballBehaviour.BallTypeSelected(id);
    }

    private void BallGridSelected(int value)
    {
        ballBehaviour.BallGridSelected(value);
    }

    private void BallPhaseCompleted()
    {
        scoreBehaviour.UpdateRemaining(++ballCount);
        batBehaviour.Init(gameConfig);
    }

    private void RunToScoreSelected(int runs)
    {
        batBehaviour.RunsSelected(runs);
    }

    private void BatPhaseCompleted(int runsAccumalated, int wicketsDown, bool allOut)
    {
        if(runs < runsAccumalated || wickets < wicketsDown)
        {
            runs = runsAccumalated;
            wickets = wicketsDown;
            scoreBehaviour.UpdateScore(runs, wickets);
        }
        if(!allOut)
        {
            if(!isBowlingAutomated)
                ballBehaviour.BallTypeSelected(currentBallTypeId);
            else
                ballBehaviour.AITurn();
        }
        else
        {
            menuBehaviour.ShowMenu();
            scoreBehaviour.gameObject.SetActive(false);
        }
    }

    public override void RemoveListeners()
    {
        GameEvents.OnClickPlay -= PlayButtonClicked;
        GameEvents.OnSelectedBallType -= BallTypeSelected;
        GameEvents.OnSelectedBallGrid -= BallGridSelected;
        GameEvents.OnSelectedRunsGrid -= RunToScoreSelected;
        GameEvents.OnBallPhaseComplete -= BallPhaseCompleted;
        GameEvents.OnBatPhaseComplete -= BatPhaseCompleted;
    }

    public override void Init()
    {

    }
}
