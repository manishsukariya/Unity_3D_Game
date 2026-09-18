using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject missionScreen;
    public GameObject resultScreen;

    public TMP_Text missionText;
    public TMP_Text progressText;
    public TMP_Text resultText;

    public Button startButton;

    public List<GameObject> yellowVehicles;
    public List<GameObject> policeVehicles;
    public List<GameObject> armyVehicles;

    int mission;
    int destroyed;
    int target;
    float timeLeft;
    bool playing;

    void Start()
    {
        startScreen.SetActive(true);
        missionScreen.SetActive(false);
        resultScreen.SetActive(false);

        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        startScreen.SetActive(false);
        missionScreen.SetActive(true);

        destroyed = 0;
        mission = Random.Range(0, 4);

        if (mission == 0)
        {
            target = 10;
            timeLeft = 20;
            missionText.text = "Destroy 10 yellow vehicles";
        }
        else if (mission == 1)
        {
            target = 5;
            timeLeft = 30;
            missionText.text = "Destroy 5 police vehicles";
        }
        else if (mission == 2)
        {
            target = 5;
            timeLeft = 30;
            missionText.text = "Destroy 5 army vehicles";
        }
        else
        {
            target = yellowVehicles.Count;
            timeLeft = 30;
            missionText.text = "Destroy all yellow vehicles";
        }

        playing = true;

        Invoke(nameof(HideMission), 3f);
    }

    void Update()
    {
        if (!playing)
            return;

        timeLeft -= Time.deltaTime;

        progressText.text =
            destroyed + " / " + target +
            "    " + Mathf.Ceil(timeLeft);

        if (destroyed >= target)
            Win();

        if (timeLeft <= 0)
            Lose();
    }

    public void DestroyYellow(GameObject vehicle)
    {
        if (!playing)
            return;

        if (yellowVehicles.Contains(vehicle))
            destroyed++;

        Destroy(vehicle);
    }

    public void DestroyPolice(GameObject vehicle)
    {
        if (!playing)
            return;

        if (policeVehicles.Contains(vehicle))
            destroyed++;

        Destroy(vehicle);
    }

    public void DestroyArmy(GameObject vehicle)
    {
        if (!playing)
            return;

        if (armyVehicles.Contains(vehicle))
            destroyed++;

        Destroy(vehicle);
    }

    void HideMission()
    {
        missionScreen.SetActive(false);
    }

    void Win()
    {
        playing = false;
        resultScreen.SetActive(true);
        resultText.text = "YOU WIN!";
    }

    void Lose()
    {
        playing = false;
        resultScreen.SetActive(true);
        resultText.text = "YOU LOSE!";
    }
}