using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;
using System.Linq;
using Doozy.Engine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{

    public static QuestManager Instance;
    [Header("Quest")]

    public int MaxQuestAvaible;
    [MinMaxSlider(30, 60)]
    public Vector2Int RangeTimeEasy;
    [MinMaxSlider(45, 90)]
    public Vector2Int RangeTimeNormal;
    [MinMaxSlider(90, 120)]
    public Vector2Int RangeTimeHard;


    [Header("Package")]
    public List<Package> Packages;
    [Header("Client")]
    public List<Planet> Planets;

    List<Quest> AvaibleQuest { get; set; } = new List<Quest>();
    public UiQuest UiQuestPrefab;
    public List<UiQuest> UiQuests { get; set; } = new List<UiQuest>();
    public static Quest SelectedQuest;

    private void Awake()
    {
        Instance = Instance ?? this;

    }
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Planets",LoadSceneMode.Additive);
        for (int i = 0; i < MaxQuestAvaible; ++i)
        {
            UiQuests.Add(Instantiate(UiQuestPrefab, this.transform));
            AvaibleQuest.Add(CreateQuest());
            UiQuests.Last().LoadQuest(AvaibleQuest.Last());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Quest CreateQuest()
    {
        Package package;
        Planet planet;
        List<Planet> tempPlanets;
        List<Package> tempPackages;


        if (AvaibleQuest.Count > 0)
        {
            if (AvaibleQuest.Where(Quest => Quest.Difficulty == QuestDifficulty.Easy).ToList().Count < 2)
            {
                tempPackages = Packages.Where(p => p.Difficulty == QuestDifficulty.Easy).ToList();
                package = tempPackages[Random.Range(0, tempPackages.Count)];

                tempPlanets = Planets.Where(c => c.Difficulty == QuestDifficulty.Easy).ToList();
                planet = tempPlanets[Random.Range(0, tempPlanets.Count)];
                return new Quest(QuestDifficulty.Easy, Random.Range(RangeTimeEasy.x, RangeTimeEasy.y), package, planet);
            }
            else if (AvaibleQuest.Where(Quest => Quest.Difficulty == QuestDifficulty.Normal).ToList().Count < 2)
            {
                tempPackages = Packages.Where(p => p.Difficulty == QuestDifficulty.Normal).ToList();
                package = tempPackages[Random.Range(0, tempPackages.Count)];
                tempPlanets = Planets.Where(c => c.Difficulty == QuestDifficulty.Normal).ToList();
                planet = tempPlanets[Random.Range(0, tempPlanets.Count)];
                return new Quest(QuestDifficulty.Normal, Random.Range(RangeTimeNormal.x, RangeTimeNormal.y), package, planet);

            }
            else if (AvaibleQuest.Where(Quest => Quest.Difficulty == QuestDifficulty.Hard).ToList().Count < 2)
            {
                tempPackages = Packages.Where(p => p.Difficulty == QuestDifficulty.Hard).ToList();
                package = tempPackages[Random.Range(0, tempPackages.Count)];
                tempPlanets = Planets.Where(c => c.Difficulty == QuestDifficulty.Hard).ToList();
                planet = tempPlanets[Random.Range(0, tempPlanets.Count)];
                return new Quest(QuestDifficulty.Hard, Random.Range(RangeTimeHard.x, RangeTimeHard.y), package, planet);

            }
        }

        tempPackages = Packages.Where(p => p.Difficulty == QuestDifficulty.Easy).ToList();
        package = tempPackages[Random.Range(0, tempPackages.Count)];

        tempPlanets = Planets.Where(c => c.Difficulty == QuestDifficulty.Easy).ToList();
        planet = tempPlanets[Random.Range(0, tempPlanets.Count)];


        return new Quest(QuestDifficulty.Easy, Random.Range(RangeTimeEasy.x, RangeTimeEasy.y), package, planet);



    }


    private void OnMessage(GameEventMessage message)
    {
        if (message == null) return;
        if (message.EventName == "ShowInfoQuest")
        {
            if (message.HasSource)
            {
                UiQuest fromButton = message.Source.GetComponentInParent<UiQuest>();
                fromButton.ShowInfoQuest();
            }
        }
    }

    private void OnEnable()
    {
        //Start listening for game events
        Message.AddListener<GameEventMessage>(OnMessage);
    }

    private void OnDisable()
    {
        //Stop listening for game events
        Message.RemoveListener<GameEventMessage>(OnMessage);
    }
}
