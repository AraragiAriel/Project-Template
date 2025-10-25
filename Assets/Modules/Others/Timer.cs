using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Action OnCountdownStart;
    public Action<float> OnTimerChange;
    public Action OnCountdownEnd;

    private float _timer;
    private float timer{
        get => _timer;
        set{
            _timer = Mathf.Max(value, 0f);
            OnTimerChange?.Invoke(_timer);
            if(Mathf.Approximately(_timer, 0f)){
                OnCountdownEnd?.Invoke();
                StopCountdown();
            }
        }
    }

    private Coroutine countDown;

    private void Start(){
    }

    public void StartCountdown(float duration){
        if(countDown != null)
            StopCountdown();

        timer = duration;
        OnCountdownStart?.Invoke();
        countDown = StartCoroutine(CountdownCo());
    }

    public void StopCountdown(){
        if(countDown != null){
            StopCoroutine(countDown);
            countDown = null;
        }
    }

    private IEnumerator CountdownCo(){
        while(timer > 0f){
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
        }
    }
}
