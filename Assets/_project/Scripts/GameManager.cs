using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public Quest SelectedQuest;
    public event Action<float> UpdateQuest;
    public event Action OnReadyEndQuest;
    public event Action OnEndQuest;

    private float _chronoQuest;
    private float _durationQuest;
    private float _progressTimeQuest=0f;


    private void Awake()
    {
        Instance = Instance ?? this;
        SelectedQuest = QuestManager.SelectedQuest;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetupQuest();


    }



    // Update is called once per frame
    void Update()
    {
        TimeQuest();
    }


    void SetupQuest()
    {
        _durationQuest = SelectedQuest.Duration;

    }

    void TimeQuest()
    {
        _chronoQuest += Time.deltaTime;
        _progressTimeQuest = _chronoQuest / _durationQuest;
        UiManager.Instance.UpdateSlider(_progressTimeQuest);
        if (_progressTimeQuest > 0.8f)
        {
            OnReadyEndQuest?.Invoke();
        }
    }
}
