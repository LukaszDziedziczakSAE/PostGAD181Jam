using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject doorObj;
    [SerializeField] Vector3 closedPosition;
    [SerializeField] Vector3 openPosition;
    [SerializeField] float openingSpeed = 1.0f;
    [SerializeField] float closeingSpeed = 1.0f;

    List<Character> inRange = new List<Character>();

    public EState State { get; private set; }

    float startTime = Mathf.Infinity;
    float endTime
    {
        get
        {
            switch(State)
            {
                case EState.opening: return startTime + openingSpeed;
                case EState.closing: return startTime + closeingSpeed;

                default: return Time.time;
            }
        }
    }

    public enum EState
    {
        closed,
        open,
        closing,
        opening
    }

    private void Update()
    {
        switch (State)
        {
            case EState.closed:
                if (inRange.Count > 0)
                {
                    StartTransition();
                }
                break;

            case EState.open:
                if (inRange.Count <= 0)
                {
                    StartTransition();
                }
                break;

            case EState.closing:
                //Debug.Log("Closing " + Progress.ToString("F2"));
                doorObj.transform.localPosition = Vector3.Lerp(openPosition, closedPosition, Progress);
                
                if (Time.time >= endTime)
                {
                    SetClosed();
                }
                break;

            case EState.opening:
                //Debug.Log("Opening " + Progress.ToString("F2"));
                doorObj.transform.localPosition = Vector3.Lerp(closedPosition, openPosition, Progress);
                
                if (Time.time >= endTime)
                {
                    SetOpen();
                }
                break;
        
        }

    }

    public float Progress
    {
        get
        {
            switch (State)
            {
                case EState.opening:
                    return (openingSpeed - (endTime - Time.time) )/ openingSpeed;
                case EState.closing:
                    return (closeingSpeed - (endTime - Time.time) )/ closeingSpeed;

                default: return 1;
            }
        }
    }

    public void StartTransition()
    {
        if (State == EState.opening || State == EState.closing) return;

        if (State == EState.open)
        {
            State = EState.closing;
        }
        else if (State == EState.closed)
        {
            State = EState.opening;
        }

        startTime = Time.time;
    }

    private void SetClosed()
    {
        State = EState.closed;
        doorObj.transform.localPosition = closedPosition;
        //Debug.Log(name + " Closed");
    }

    private void SetOpen()
    {
        State = EState.open;
        doorObj.transform.localPosition = openPosition;
        //Debug.Log(name + " Opened");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character otherCharacter))
        {
            if (!inRange.Contains(otherCharacter)) inRange.Add(otherCharacter);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character otherCharacter))
        {
            if (inRange.Contains(otherCharacter)) inRange.Remove(otherCharacter);
        } 
    }

}
