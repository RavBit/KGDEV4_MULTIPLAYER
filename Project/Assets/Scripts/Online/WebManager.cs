using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class WebManager : NetworkBehaviour
{

    public static WebManager instance;


    public GameObject EndScreen;
    // Makes sure the App_Manager does not get destroyed & Singleton 
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one Web Manager in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    public void EnableEndScreen()
    {
        EndScreen.SetActive(true);
    }

    public void Set_Score()
    {
        StartCoroutine("SetScore", AppManager.instance.Score);
    }
    //Corountine that goes through the Score proccess
    public IEnumerator SetScore(float score)
    {
        WWWForm score_form = new WWWForm();
        score_form.AddField("session_id", AppManager.instance.User.session);
        score_form.AddField("score", (int)score);
        Debug.Log("http://81.169.177.181/KGDEV4/register_score.php?PHPSESSID=" + AppManager.instance.User.session);
        WWW owneditemsdata = new WWW("http://81.169.177.181/KGDEV4/register_score.php", score_form);
            yield return owneditemsdata;

            if (string.IsNullOrEmpty(owneditemsdata.error))
            {
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Lobby");
            }
            else
            {
            Debug.LogError("ERROR FATAL");
            }
    }
}
