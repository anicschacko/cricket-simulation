using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CMBatBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject runsSelectPanel;
    [SerializeField] private TextMeshProUGUI scoreCard;


    private Dictionary<int, GameObject> batPanels;
    private GameConfigScriptable gameConfig;

    private int currentRunsSelected = 0, runsAccumalated = 0, wicketsDown = 0;
    private bool isOut = false, isMiss = false;

    void Awake()
    {
        batPanels = new Dictionary<int, GameObject>();
        batPanels.Add(1, runsSelectPanel);
        batPanels.Add(2, scoreCard.gameObject);
    }

    public void Init(GameConfigScriptable gameConfig)
    {
        this.gameConfig = gameConfig;
        if (!this.gameObject.activeInHierarchy)
            this.gameObject.SetActive(true);
        ToggleBatPanels(1);
    }

    public void RunsSelected(int runs)
    {
        isOut = false; isMiss = false;
        currentRunsSelected = runs;
        CalculateScoringOdds();
        UpdateUI((allOut) =>
        {
            Utils.WaitForSeconds(1, () => 
            {
                ToggleBatPanels(0);
                GameEvents.OnBatPhaseComplete?.Invoke(runsAccumalated, wicketsDown, allOut);
                if(allOut)
                    runsAccumalated = wicketsDown = 0;
            });
        });
    }

    private void UpdateUI(Action<bool> allOut)
    {
        ToggleBatPanels(2);
        if (isOut)
        {
            scoreCard.text = $"OUT!,\n{++wicketsDown} wicket down";
            if(wicketsDown == gameConfig.totalBatsmen)
            {
                scoreCard.text = $"GAME OVER!\nALL WICKETS DOWN";
                allOut?.Invoke(true);
            }
            else
                allOut?.Invoke(false);
        }
        else if (isMiss)
        {
            scoreCard.text = $"MISSED";
            allOut?.Invoke(false);
        }
        else
        {
            scoreCard.text = $"RUNS SCORED : {currentRunsSelected}\nTOTAL SCORE : {runsAccumalated}";
            if(runsAccumalated == gameConfig.toWin)
            {
                scoreCard.text = $"TARGET REACHED!\nCONGRATULATIONS!!";
                allOut?.Invoke(true);
            }
            else
                allOut?.Invoke(false);
        }
    }

    private void CalculateScoringOdds()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 1f);
        float weightage = gameConfig.GetWeightage(currentRunsSelected);

        if(randomNumber <= weightage)
            runsAccumalated += currentRunsSelected;
        else
        {
            CheckIsOutOrMiss();
        }

        void CheckIsOutOrMiss()
        {
            randomNumber = UnityEngine.Random.Range(0f, 1f);
            float outWeightage = gameConfig.GetOutAndMissWeightage().x;

            if(randomNumber < outWeightage)
                isOut = true;
            else
                isMiss = true;
        }
    }

    private void ToggleBatPanels(int index)
    {
        for(int i = 1; i <= batPanels.Count; i++)
        {
            batPanels[i].SetActive(index == i);
        }
        this.gameObject.SetActive(index != 0);
    }
}
