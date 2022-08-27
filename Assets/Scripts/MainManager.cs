using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestscoreText;
    public GameObject GameOverText;
    public GameObject mainMenu;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public static MainManager2 Instance;
    public string playerName;
    //public string bestPlayer;
    public int playerScore = 0;


    // Start is called before the first frame update
    private void Awake()
    {
        MainManager2.Instance.LoadBestPlayer();
        bestscoreText.text = $"Best score: {MainManager2.Instance.bestplayerName} : {MainManager2.Instance.playerScore}";
    }
    void Start()
    {
        if (MainManager2.Instance != null)
        {
            ScoreText.text = $"{MainManager2.Instance.playerName} Score : {m_Points}";
        }        

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        playerName = MainManager2.Instance.playerName;       
        ScoreText.text = $"{playerName} Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        mainMenu.SetActive(true);
        SaveAll();


    }
    public void MainMenu()
    {
        SaveAll();
        SceneManager.LoadScene(0);
    }

    public void SaveAll()
    {
        if (m_Points > playerScore)
        {
            playerScore = m_Points;
            bestscoreText.text = $"Best score: {playerName} : {playerScore}";
            MainManager2.Instance.bestplayerName = playerName;
            MainManager2.Instance.playerScore = playerScore;
            MainManager2.Instance.SaveBestPlayerName();
        }
        else
        {
            bestscoreText.text = $"Best score: {MainManager2.Instance.bestplayerName} : {MainManager2.Instance.playerScore}";
        }
    }
}
