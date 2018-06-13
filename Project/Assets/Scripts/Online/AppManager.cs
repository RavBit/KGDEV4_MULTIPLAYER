using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;
    [Header("User that's logged in")]
    public User User;

    // Makes sure the App_Manager does not get destroyed & Singleton 
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one App Manager in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }


    // Set the user that logs in
    public void SetUser(User user)
    {
        User = user;
    }


    // Logs out the user that was logged in
    public void LogOut()
    {
        User = null;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Invoke("LoadUrl", 2);
    }
}
