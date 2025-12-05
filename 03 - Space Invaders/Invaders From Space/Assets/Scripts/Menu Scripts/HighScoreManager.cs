using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField intialInput;
    [SerializeField] private TextMeshProUGUI initial1;
    [SerializeField] private TextMeshProUGUI initial2;
    [SerializeField] private TextMeshProUGUI initial3;
    [SerializeField] private TextMeshProUGUI initial4;
    [SerializeField] private TextMeshProUGUI initial5;
    [SerializeField] private TextMeshProUGUI score1;
    [SerializeField] private TextMeshProUGUI score2;
    [SerializeField] private TextMeshProUGUI score3;
    [SerializeField] private TextMeshProUGUI score4;
    [SerializeField] private TextMeshProUGUI score5;

    private int lastGameScore;

    private List<ScoreCard> highScoreList;

    private void Awake()
    {
        lastGameScore = PlayerPrefsManager.GetLastGameScore();
        highScoreList = new List<ScoreCard>(PlayerPrefsManager.GetHighScores());
        UpdateTable();
        intialInput.gameObject.SetActive(false);
    }

    private void Start()
    {
        if(lastGameScore > highScoreList[highScoreList.Count - 1].score)
        {
            intialInput.gameObject.SetActive(true);
        }
        else
        {
            intialInput.gameObject.SetActive(false);
        }
    }

    public void OnEndEdit()
    {
        highScoreList[4].score = lastGameScore;
        highScoreList[4].initials = intialInput.text;
        ReOrderTable();
        UpdateTable();
        PlayerPrefsManager.SetHighScores(highScoreList);
        intialInput.gameObject.SetActive(false);
    }

    private void ReOrderTable()
    {
        ScoreCard tempScoreCard;
        for (int i = 4; i > 0; i--)
        {
            if (highScoreList[i].score <= highScoreList[i-1].score)
            {
                continue;
            }
            else
            {
                tempScoreCard = highScoreList[i-1];
                highScoreList[i-1] = highScoreList[i];
                highScoreList[i] = tempScoreCard;
            }
        }
    }

    private void UpdateTable()
    {
        initial1.text = highScoreList[0].initials;
        initial2.text = highScoreList[1].initials;
        initial3.text = highScoreList[2].initials;
        initial4.text = highScoreList[3].initials;
        initial5.text = highScoreList[4].initials;
        score1.text = "" + highScoreList[0].score;
        score2.text = "" + highScoreList[1].score;
        score3.text = "" + highScoreList[2].score;
        score4.text = "" + highScoreList[3].score;
        score5.text = "" + highScoreList[4].score;
    }
}
