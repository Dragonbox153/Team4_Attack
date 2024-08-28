using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public TMP_Text _currentScore_textBox;
    public TMP_Text _HiScore_textBox;

    public int PlayerScore = 0;
    public int _HISCORE = 0;

    public GameObject[] _lives_images = new GameObject[3];

    public static ScoreBoard Inst;
    private void Awake()
    {
        Inst = this;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("HiScore") != 0)
        {
            _HISCORE = PlayerPrefs.GetInt("HiScore");
            _HiScore_textBox.text = _HISCORE.ToString();
        }
        else
            _HiScore_textBox.text = "0";
    }

    public void AddScore(int ScoreToAdd)
    {
        PlayerScore += ScoreToAdd;

        _currentScore_textBox.text = PlayerScore.ToString();
    }

    public void CheckIfNewHiScore()
    {
        if(PlayerScore > _HISCORE)
        {
            PlayerPrefs.SetInt("HiScore", PlayerScore);
        }
    }

    public void LowerHealth(int SpriteToDrop)
    {
        _lives_images[SpriteToDrop].SetActive(false);
    }
}
