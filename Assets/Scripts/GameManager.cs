using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int currentPauseState = 1;
    [SerializeField] GameObject SceletonSpawner;
    [SerializeField] GameObject GhostSpawner;
    [SerializeField] GameObject PotionSpawner;
    [SerializeField] GameObject BoxSpawner;
    [SerializeField] GameObject Crystal;

    [SerializeField] private int sceletonsAmount = 3;
    [SerializeField] private int ghostAmount = 2;

    [SerializeField] Transform Player;
    [SerializeField] Transform Diamond;

    [SerializeField] GameObject GameOverPanel;
    [SerializeField] TextMeshProUGUI RestartText;
    [SerializeField] GameObject MenuPanel;

    private GameObject Potion;
    private GameObject Box;

    [SerializeField] private List<GameObject> ghostsList;
    [SerializeField] private List<GameObject> sceletonList;

    void Awake()
    {
        InvokeRepeating(nameof(CheckEnemies), 0f, 10f);
        InvokeRepeating(nameof(PotionSpawn), 20f, 20f);
        InvokeRepeating(nameof(BoxSpawn), 20f, 40f);
        TogglePause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();


        if (Player == null || Diamond == null)
        {
            //Game Over
            GameOverPanel.SetActive(true);

            string finalScore = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>().OnGameOverScore();
            RestartText.text = "Your coins: " + finalScore + "\nPress R to restart";

            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (sceletonList.Count == 0)
        {
            sceletonsAmount++;
            StartCoroutine(MyCounter(sceletonsAmount, true));
        }

        if (ghostsList.Count == 0)
        {
            ghostAmount++;
            StartCoroutine(MyCounter(ghostAmount, false));
        }

    }

    void PotionSpawn()
    {
        if (Potion != null)
            Destroy(Potion);

        Potion = PotionSpawner.GetComponent<Spawner>().Spawn();
    }

    void BoxSpawn()
    {
        // if (Box != null)
        //     Destroy(Box);

        Box = BoxSpawner.GetComponent<Spawner>().Spawn();
    }

    void SceletonSpawn()
    {
        GameObject newBornSceleton = SceletonSpawner.GetComponent<Spawner>().Spawn();
        sceletonList.Add(newBornSceleton);
        newBornSceleton.GetComponent<SceletonAi>().SetTarget(Diamond);
    }

    void GhostSpawn()
    {
        var newbornGhost = GhostSpawner.GetComponent<Spawner>().Spawn();
        ghostsList.Add(newbornGhost);
        newbornGhost.GetComponent<AIDestinationSetter>().target = Player;
    }

    void CheckEnemies()
    {
        ghostsList = GameObject.FindGameObjectsWithTag("Ghost").ToList<GameObject>();
        sceletonList = GameObject.FindGameObjectsWithTag("Sceleton").ToList<GameObject>();
    }

    IEnumerator MyCounter(int number, bool isSceleton)
    {
        int i = 0;
        while(i < number)
        {
            if (isSceleton)
                SceletonSpawn();
            else
                GhostSpawn();

            yield return new WaitForSeconds(1.0f);
            i++;
        }
    }

    public void TogglePause()
    {
        currentPauseState = Mathf.Abs(currentPauseState - 1);
        Time.timeScale = currentPauseState;

        if (currentPauseState == 0)
        {
            MenuPanel.SetActive(true);
        }
        else
        {
            MenuPanel.SetActive(false);
        }
    }

    public void SpawnCrystal(Vector3 pos)
    {
        Instantiate(Crystal, pos, Quaternion.identity);
    }

}
