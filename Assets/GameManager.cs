using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //These variables take inspector refference to objects
    public Transform BG;
    public GameObject Player;
    public GameObject Water;

    //Amount of time elapsed since game started
    float _baseTimeElapsed = 0;

    //Angle at whihc the BG png should be
    public float DayNightCyclePNG_angle = 0;

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

    public enum TimesOfDay
    {
        Morning, Evening, Night, Dawn
    }

    TimesOfDay CurrentTimeOfDay = TimesOfDay.Morning; 

    //Singleton
    public static GameManager Instance;
    private float angleTolerance = 2.0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _baseTimeElapsed += Time.deltaTime * DayNightCycleSpeedDelta;

        DayNightCyclePNG_angle = _baseTimeElapsed;

        BG.transform.eulerAngles = new Vector3(0,0,DayNightCyclePNG_angle);

        

        if(IsApproximately(DayNightCyclePNG_angle, 90))
        {
            movingUp = true;
            StartCoroutine(ChangeTide(LowTide, MidTide));
        }

        if (IsApproximately(DayNightCyclePNG_angle, 180))
        {
            movingUp = true;
            StartCoroutine(ChangeTide(MidTide, HighTide));
        }

        if (IsApproximately(DayNightCyclePNG_angle, 270))
        {
            movingDown = true;
            StartCoroutine(ChangeTide(HighTide, MidTide));
        }

        if (IsApproximately(DayNightCyclePNG_angle, 360))
        {
            movingDown = true;
            DayNightCyclePNG_angle = 0;
            _baseTimeElapsed = 0;
            StartCoroutine(ChangeTide(MidTide, LowTide));
        }
    }

    bool IsApproximately(float angle, float target)
    {
        return Mathf.Abs(angle - target) < angleTolerance;
    }

    IEnumerator ChangeTide(Vector3 A, Vector3 B)
    {
        float tidechangeElapsedTime = 0;
        while (tidechangeElapsedTime < TideChangeDuration && Player != null)
        {
            float t = tidechangeElapsedTime / TideChangeDuration;
            BufferPosition = Vector3.Lerp(A, B, t);
            Player.transform.position = new Vector3(Player.transform.position.x, BufferPosition.y, 0);
            Water.transform.position += new Vector3(0, BufferPosition.y, 0);
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
