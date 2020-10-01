using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;
using System.Linq;
using Doozy.Engine;
using Doozy.Engine.UI;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UiQuest : MonoBehaviour
{
    public UIButton ButtonQuest;
    public Image PlanetImage;
    public TextMeshProUGUI TextTimeQuest;
    public TextMeshProUGUI TextMoneyQuest;
    public UIButton StartQuest;
    public UIButton StartQuestAd;

    public UIView ViewQuestInfo;
    public bool IsOpen { get; set; }
   public Quest ActualQuest { get; set; }

    private void Start()
    {
        ViewQuestInfo.Hide(true);
        ViewQuestInfo.gameObject.SetActive(false);
        StartQuest.Button.onClick.AddListener(StarGameNoAD);
        //LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }


    public void ShowInfoQuest()
    {
        UiQuest otherOpen = QuestManager.Instance.UiQuests.Where(uq => uq.IsOpen == true).FirstOrDefault();
        if (otherOpen)
        {
            otherOpen.ViewQuestInfo.Hide();
            otherOpen.ViewQuestInfo.gameObject.SetActive(false);
            otherOpen.IsOpen = false;
        }

       ViewQuestInfo.gameObject.SetActive(true);
        ViewQuestInfo.Show();
        IsOpen = true;
        //LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void LoadQuest(Quest quest)
    {
        ActualQuest = quest;
        PlanetImage.material = quest.Planet.PlanetPicture;
        switch (ActualQuest.Difficulty)
        {
            case QuestDifficulty.Easy:
                ButtonQuest.GetComponent<Image>().color = Color.green;    // new Color(36, 255, 0);
                break;
            case QuestDifficulty.Normal:
                ButtonQuest.GetComponent<Image>().color = Color.magenta;   //new Color(252, 131, 0);
                break;
            case QuestDifficulty.Hard:
                ButtonQuest.GetComponent<Image>().color = Color.red;   //new Color(255, 56, 0);
                break;
        }
        TextTimeQuest.text = TimeSpan.FromSeconds(quest.Duration).ToString(@"mm\:ss");
        TextMoneyQuest.text = UnityEngine.Random.Range(quest.Package.MoneyGain.x, quest.Package.MoneyGain.y).ToString();
    }

    void StarGameNoAD()
    {
        QuestManager.SelectedQuest = ActualQuest;
       
        SceneManager.LoadSceneAsync("Game 1");
        SceneManager.LoadSceneAsync("Planets", LoadSceneMode.Additive);

    }
    
}