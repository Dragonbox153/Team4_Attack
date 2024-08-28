using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //These variables take inspector refference to objects
    public SpriteRenderer BGSpriteRenderer;
    public GameObject Player;

    //Amount of time elapsed since game started
    float _baseTimeElapsed = 44.23f;

    //Angle at whihc the BG png should be
    float DayNightCyclePNG_angle = 44.23f;

    //Determines speed of the day night cycle speed
    public float DayNightCycleSpeedDelta;
    
    //How long the tide change transtion is going to be
    public float TideChangeDuration = 2f;

    //what the current tide level is
    public float CurrentTideLevel = -2.5f;

    public bool movingUp = false;
    public bool movingDown = false;

    public Vector3 BufferPosition = Vector3.zero; 

    //Refference to all the tide type
    public Vector3 HighTide = new Vector3(0, 2.5f, 0);
    public Vector3 MidTide = new Vector3(0, 0, 0);
    public Vector3 LowTide = new Vector3(0, -2.5f, 0);

    //Singleton
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _baseTimeElapsed += Time.deltaTime * DayNightCycleSpeedDelta;

        DayNightCyclePNG_angle = _baseTimeElapsed;

        BGSpriteRenderer.transform.eulerAngles = new Vector3(0,0,DayNightCyclePNG_angle);

        if(Mathf.Ceil(DayNightCyclePNG_angle) == 90)
        {
            movingUp = true;
            StartCoroutine(ChangeTide(LowTide, MidTide));
        }

        if (Mathf.Ceil(DayNightCyclePNG_angle) == 180)
        {
            movingUp = true;
            StartCoroutine(ChangeTide(MidTide, HighTide));
        }

        if (Mathf.Ceil(DayNightCyclePNG_angle) == 270)
        {
            movingDown = true;
            StartCoroutine(ChangeTide(HighTide, MidTide));
        }

        if (Mathf.Ceil(DayNightCyclePNG_angle) == 360)
        {
            movingDown = true;
            DayNightCyclePNG_angle = 0;
            StartCoroutine(ChangeTide(MidTide, LowTide));
        }
    }



    IEnumerator ChangeTide(Vector3 A, Vector3 B)
    {
        float tidechangeElapsedTime = 0;
        while (tidechangeElapsedTime < TideChangeDuration && Player != null)
        {
            float t = tidechangeElapsedTime / TideChangeDuration;
            BufferPosition = Vector3.Lerp(A, B, t);
            Player.transform.position = new Vector3(Player.transform.position.x, BufferPosition.y, 0);
            CurrentTideLevel = Player.transform.position.y;
            tidechangeElapsedTime += Time.deltaTime;
            yield return null;
        }
        if (Player != null)
        {
            movingUp = false;
            movingDown = false;
            Player.transform.position = new Vector3(Player.transform.position.x, B.y, 0);
            CurrentTideLevel = B.y;
        }
    }

    
    /*
     
    TRASNSITION PERIOD FROM ONE BLOCK TO OTHER
     
    HOW IS THIS GOING TO WORK ?

    WE NEED TO DEFINE THE SRTARR OF A BLOCK AND WHEN WE START TRANSITIONING INTO THE NEXT

    IT WILL TAKE A VERY SMALL BRIEF SECOND TO TRANSITION TO THE NEXT AND THEN 
     
    I GUESS THE BG TRANSITION PERIOD CAN BE BE CONSISTENT 
    WHILE THE TIDE CHANGE CAN BE FAST
    
    */
}
