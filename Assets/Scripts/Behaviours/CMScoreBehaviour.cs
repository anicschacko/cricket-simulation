using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CMScoreBehaviour : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private Text remaining;
    [SerializeField] private TextMeshProUGUI finalCard;


    private int runs, wickets, ballCount;
    private GameConfigScriptable gameConfig;
    public void Init(GameConfigScriptable gameConfig)
    {
        runs = wickets = ballCount = 0;
        this.gameConfig = this.gameConfig ?? gameConfig;
        this.gameObject.SetActive(true);
        UpdateScore();
        UpdateRemaining();
    }

    public void UpdateScore(int runs = 0, int wickets = 0)
    {
        this.runs = runs;
        this.wickets = wickets;
        score.text = $"SCORE : {runs}/{wickets}";
    }

    public void UpdateRemaining(int ballCount = 0)
    {
        remaining.text = $"|| TO WIN: {gameConfig.toWin - runs} || FROM: {gameConfig.TotalBalls - ballCount} Balls ||";
    }

    public void ShowMessage(string msg)
    {
        finalCard.text = msg;
    }
}
