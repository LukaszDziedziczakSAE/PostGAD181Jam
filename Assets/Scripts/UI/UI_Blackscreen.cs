using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Blackscreen : MonoBehaviour
{
    [SerializeField] float transitionTime;
    [SerializeField] Image image;
    EState state;
    float startTime;
    float progress => (Time.time - startTime) / transitionTime;

    public event Action OnFinish;

    public enum EState
    {
        black,
        clear,
        blackening,
        clearing
    }

    private void Start()
    {
        SetAlpha(1f);
        state = EState.black;
        Toggle();
    }

    private void Update()
    {
        if (state == EState.blackening)
        {
            SetAlpha(progress);
            if (progress >= 1)
            {
                state = EState.black;
                SetAlpha(1);
                OnFinish?.Invoke();
                OnFinish = null;
            }
        }

        else if (state == EState.clearing)
        {
            SetAlpha(1 - progress);
            if (progress >= 1)
            {
                state = EState.clear;
                SetAlpha(0);
                OnFinish?.Invoke();
                OnFinish = null;
            }
        }
    }

    public void Toggle()
    {
        if (state == EState.blackening || state == EState.clearing) return;

        if (state == EState.black)
        {
            state = EState.clearing;

            
        }

        else if (state == EState.clear)
        {
            state = EState.blackening;
        }

        startTime = Time.time;
    }

    private void SetAlpha(float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    public bool IsClear => state == EState.clear;

    public bool IsBlack => state == EState.black;

    public bool IsTransitioning => state == EState.blackening || state == EState.clearing;
}
