using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int score;

    bool firebaseReady = false;
    
    FirebaseFirestore db;

    public Text scoreText;

    public GameObject gameStartUI;

    private string playId;

    private void Awake() 
    {
        instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        this.firebaseReady = false;
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                this.db = FirebaseFirestore.DefaultInstance;
                this.firebaseReady = true;
            } else {
                UnityEngine.Debug.LogError(System.String.Format(
                "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        if (this.firebaseReady) {
            gameStartUI.SetActive(false);
            scoreText.gameObject.SetActive(true);

            if (Context.UserId == null) {
                string userId = System.Guid.NewGuid().ToString();
                DocumentReference docRef = db.Collection("players").Document(userId);
                    Dictionary<string, object> user = new Dictionary<string, object>
                    {
                            { "id", userId },
                            { "name", GenerateName(10) },
                            { "nick", GenerateName(3) }
                    };
                    docRef.SetAsync(user).ContinueWithOnMainThread(task => {
                            Debug.Log("Added data to the players collection.");
                    });
                Context.UserId = userId;
            }

            this.playId = System.Guid.NewGuid().ToString();

            DocumentReference docRefPlay = db.Collection("play").Document(this.playId);
                    Dictionary<string, object> play = new Dictionary<string, object>
                    {
                            { "id", this.playId },
                            { "userId", Context.UserId },
                            { "finished", false },
                    };
                    docRefPlay.SetAsync(play).ContinueWithOnMainThread(task => {
                            Debug.Log("Added data to the play collection.");
                    });
        }
    }

     public static string GenerateName(int len)
    { 
        System.Random r = new System.Random();
        string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
        string Name = "";
        Name += consonants[r.Next(consonants.Length)].ToUpper();
        Name += vowels[r.Next(vowels.Length)];
        int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
        while (b < len)
        {
            Name += consonants[r.Next(consonants.Length)];
            b++;
            Name += vowels[r.Next(vowels.Length)];
            b++;
        }

        return Name;
     }

    public void Restart()
    {
        DocumentReference docRef = db.Collection("play").Document(this.playId);
        Dictionary<string, object> play = new Dictionary<string, object>
            {
                 { "id", this.playId },
                 { "userId", Context.UserId },
                 { "finished", true },
                 { "score", score }
            };

        docRef.SetAsync(play).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the play collection.");
        });

        SceneManager.LoadScene("Game");
    }

    public void ScoreUp()
    {
        score ++;
        scoreText.text = score.ToString();

        DocumentReference docRef = db.Collection("play").Document(this.playId);
        Dictionary<string, object> play = new Dictionary<string, object>
            {
                 { "id", this.playId },
                 { "userId", Context.UserId },
                 { "finished", false },
                 { "score", score },
            };
        
        docRef.SetAsync(play).ContinueWithOnMainThread(task => {
            Debug.Log("Added data to the play collection.");
        });
    }
}
