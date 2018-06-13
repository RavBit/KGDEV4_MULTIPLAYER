using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebManager : MonoBehaviour
{
    public static WebManager instance;

    // Makes sure the App_Manager does not get destroyed & Singleton 
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one Web Manager in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }
    //Corountine that goes through the Login process
    public IEnumerator SetScore(int score)
    {
        WWWForm score_form = new WWWForm();
        score_form.AddField("session_id", AppManager.instance.User.session);
        score_form.AddField("score", 45);
        Debug.Log("http://81.169.177.181/KGDEV4/register_score.php?PHPSESSID=" + AppManager.instance.User.session);
        WWW owneditemsdata = new WWW("http://81.169.177.181/KGDEV4/register_score.php", score_form);
            yield return owneditemsdata;

            if (string.IsNullOrEmpty(owneditemsdata.error))
            {
                Debug.Log("Gelukt denk ik? " + owneditemsdata.text);
                Debug.Log("test: " + owneditemsdata.error);
            }
            else
            {
            Debug.LogError("ERROR FATAL");
            }
    }
}
