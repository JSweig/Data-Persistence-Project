using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScore;
    public GameObject GameOverScreen;
    
    private bool m_Started = false;
    private int m_Points;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameOverScreen.SetActive(false);
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        HighScore.text = $"Best Score:{DataManager.Instance.playerNames[0]}: {DataManager.Instance.highScores[0]}";


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
        
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        if (m_Points > DataManager.Instance.highScores[0])
        {
            HighScore.text = $"Best Score: {DataManager.Instance.currentPlayer}: {m_Points}";
        }
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        if(m_Points > DataManager.Instance.highScores[0])
        {
            if (DataManager.Instance.highScores[1]!= 0)
            {
                DataManager.Instance.highScores[1] = DataManager.Instance.highScores[0];
                DataManager.Instance.playerNames[1] = DataManager.Instance.playerNames[0];

                DataManager.Instance.highScores[2] = DataManager.Instance.highScores[1];
                DataManager.Instance.playerNames[2] = DataManager.Instance.playerNames[1];

            }

            if (DataManager.Instance.highScores[2] != 0)
            {
                DataManager.Instance.highScores[2] = DataManager.Instance.highScores[1];
                DataManager.Instance.playerNames[2] = DataManager.Instance.playerNames[1];
            }
            DataManager.Instance.highScores[0] = m_Points;
            DataManager.Instance.playerNames[0] = DataManager.Instance.currentPlayer;

        } else if(m_Points > DataManager.Instance.highScores[1])
        {
            if (DataManager.Instance.highScores[2] != 0)
            {
                DataManager.Instance.highScores[2] = DataManager.Instance.highScores[1];
                DataManager.Instance.playerNames[2] = DataManager.Instance.playerNames[1];
            }

            DataManager.Instance.highScores[1] = m_Points;
            DataManager.Instance.playerNames[1] = DataManager.Instance.currentPlayer;
        }
        else if (m_Points > DataManager.Instance.highScores[2])
        {
            DataManager.Instance.highScores[2] = m_Points;
            DataManager.Instance.playerNames[2] = DataManager.Instance.currentPlayer;
        }
    }
}
