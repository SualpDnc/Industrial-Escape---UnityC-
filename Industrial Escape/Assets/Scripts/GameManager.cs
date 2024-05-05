using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LootLocker.Requests;
using TMPro;



public class GameManager : MonoBehaviour
{
   
    public GameObject menuPanel;
    public GameObject gameOverPanel;
    public GameObject infoPanel;
    public GameObject collector;
    public Text scoreText;
    public AudioSource audioSource;
    public AudioSource src;
    public AudioClip s1;
    public GameObject cross; //for showing sound on-off
    public Leaderboard leaderboard;
    public TMP_InputField playerNameInputfield;
    public Text lastScore;
    
 
    void Start()
    {
        StartCoroutine(SetupRoutine());
        Time.timeScale = 0;
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        collector.SetActive(false);
        scoreText.text = "0";
        cross.SetActive(false);
    }

    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputfield.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("succesfully set player name");
            }
            else
            {
                Debug.Log("couldnt set player name" + response.errorData.message);
            }
        });
    }
    
    IEnumerator SetupRoutine() //lootlocker stuff
    {
        yield return LoginRoutine();
        yield return leaderboard.FetchTopHighscoresRoutine();
    }
    public IEnumerator LoginRoutine()  //lootlocker stuff
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("player logged in.");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    public void GameOver(int score)
    {
        gameOverPanel.SetActive(true);
        lastScore.text = ("Your Score: " + score);
        Time.timeScale = 0;
    }

    public void OpenInfoPanel()
    {
        src.clip = s1;
        src.Play();
        infoPanel.SetActive(true);
    }
    
    public void CloseInfoPanel()
    {
        src.clip = s1;
        src.Play();
        infoPanel.SetActive(false);
    }
    
    public void RestartGame()
    {
        src.clip = s1;
        src.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        src.clip = s1;
        src.Play();
        menuPanel.SetActive(false);
        collector.SetActive(true);
        Time.timeScale = 1;
    }

    public void PrintScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void SoundOnOff()
    {
        src.clip = s1;
        src.Play();
        audioSource.mute = !audioSource.mute;
        if (audioSource.mute)
        {
            cross.SetActive(true);
        }
        else
        {
            cross.SetActive(false);
        }
    }


}
