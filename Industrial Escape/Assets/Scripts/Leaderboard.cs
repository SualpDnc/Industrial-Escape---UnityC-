using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;


public class Leaderboard : MonoBehaviour  //lootlocker stuff
{
    string leaderboardID = "globalHighscore";

    public Text playerNames;
    public Text playerScores;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("uploded the score.");
                done = true;
            }
            else
            {
                Debug.Log("failed" + response.errorData.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    
    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 5, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "Names\n";
                string tempPlayerScores = "Scores\n";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += "escpr";
                    }

                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("failed" + response.errorData.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
    
}
