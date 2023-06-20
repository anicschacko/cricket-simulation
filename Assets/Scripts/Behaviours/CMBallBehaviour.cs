using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;

public class CMBallBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject ballSelectPanel;
    [SerializeField] private GameObject ballGridPanel;
    [SerializeField] private TextMeshProUGUI ballSelectedPrompt;


    private Dictionary<int, GameObject> ballPanels;
    private GameConfigScriptable gameConfig;
    private string currentSelectedBallType;
    private int currentSelectedBallGrid;

    void DefineBallPanels()
    {
        ballPanels = new Dictionary<int, GameObject>();
        ballPanels.Add(1, ballSelectPanel);
        ballPanels.Add(2, ballGridPanel);
        ballPanels.Add(3, ballSelectedPrompt.gameObject);
    }

    void Awake()
    {
        DefineBallPanels(); 
    }

    public void Init(bool isAI, GameConfigScriptable gameConfig)
    {
        Reset();
        this.gameConfig = this.gameConfig ?? gameConfig;
        
        if (!this.gameObject.activeInHierarchy)
            this.gameObject.SetActive(true);
        
        if(isAI)
            AIPlayer();
        else
            ToggleBallPanel(1);
    }

    private void Reset()
    {
        currentSelectedBallType = "";
        currentSelectedBallGrid = 0;
    }

    public void BallTypeSelected(int id)
    {
        currentSelectedBallType = gameConfig.GetBallType(id);
        ToggleBallPanel(2);
    }

    public void BallGridSelected(int grid)
    {
        currentSelectedBallGrid = grid;
        ToggleBallPanel(3);
        ballSelectedPrompt.text = $"{currentSelectedBallType} ball {currentSelectedBallGrid}";

        Utils.WaitForSeconds(1, () => 
        {
            ToggleBallPanel(0);
            GameEvents.OnBallPhaseComplete?.Invoke();
        });
    }

    public void AITurn()
    {
        AIPlayer();
    }

    private void AIPlayer()
    {
        int rand;
        if(string.IsNullOrEmpty(currentSelectedBallType))
        {
            rand = UnityEngine.Random.Range(0, gameConfig.ballTypes.Count);
            currentSelectedBallType = gameConfig.ballTypes[rand].ballType;
        }

        var grids = gameConfig.dividePitchInto.x * gameConfig.dividePitchInto.y;
        rand = UnityEngine.Random.Range(1, (int)grids + 1);
        BallGridSelected(rand);
    }

    private void ToggleBallPanel(int index)
    {
        for (int i = 1 ; i <= ballPanels.Count; i++)
        {
            ballPanels[i].SetActive(index == i && index != 0);
        }
        this.gameObject.SetActive(index != 0);
    }
}
