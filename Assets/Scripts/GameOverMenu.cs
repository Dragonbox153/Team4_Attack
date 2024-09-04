using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScore;


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
        highScore.text = ScoreBoard.Inst._HISCORE.ToString();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnBack();
        }
    }
}
