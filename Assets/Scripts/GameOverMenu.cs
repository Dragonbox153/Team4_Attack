using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScore;
    [SerializeField] TMP_Text PlayerScore;
    [SerializeField] TMP_Text WinCondition;
    [SerializeField] TMP_Text WinCondition2;
    [SerializeField] TMP_Text LoseCondition;

    public static GameOverMenu instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
       this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    public void OnBack()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if(ScoreBoard.Inst.gameObject != null)
        {
            highScore.text = ScoreBoard.Inst._HISCORE.ToString();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnBack();
        }
    }

    public void SetupUITextForGameEnd()
    {
        //Debug.Log("hishocre = " + ScoreBoard.Inst._HISCORE + "  playerscore = " + ScoreBoard.Inst.PlayerScore + "  and is highscore > player score = " + (ScoreBoard.Inst._HISCORE < ScoreBoard.Inst.PlayerScore));
        PlayerScore.text = "Players Score : " + ScoreBoard.Inst.PlayerScore.ToString();

        if (ScoreBoard.Inst._HISCORE > ScoreBoard.Inst.PlayerScore)
        {
            LoseCondition.gameObject.SetActive(true);
        }
        else
        {
            WinCondition.gameObject.SetActive(true);
            WinCondition2.gameObject.SetActive(true);
        }
    }
}
