﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour {
    public static AppManager instance;
    public User User;
    [Header("Username of logged in User")]
    [SerializeField]
    private string username;

    // Makes sure the App_Manager does not get destroyed & Singleton 
    void Awake()
    {
        if (instance != null)
            Debug.LogError("More than one App Manager in the scene");
        else
            instance = this;
        DontDestroyOnLoad(transform.gameObject);
    }


    public void Destroy()
    {
        Destroy(transform.gameObject);
    }

    public void SetUser(User user)
    {
        User = user;
    }


    /*public IEnumerator UpdateResources()
    {
        //Assigning strings from the text
        Debug.Log("RESOURCES UPDATE " + RM.currency);
        //Init form and give them the email and password
        WWWForm form = new WWWForm();
        form.AddField("user_ID", User.ID);
        form.AddField("air_pollution", RM.airPollution);
        form.AddField("soil_pollution", RM.soilPollution);
        form.AddField("water_pollution", RM.waterPollution);
        form.AddField("land_use", RM.landUse);
        form.AddField("biodiversity", RM.biodiversity);
        form.AddField("currency", RM.currency);
        form.AddField("population", RM.population);
        form.AddField("wellbeing", GetWellbeing());


        //Login to the website and wait for a response
        WWW w = new WWW("http://81.169.177.181/OLP/update_resources.php", form);
        yield return w;

        //Check if the response if empty or not
        if (string.IsNullOrEmpty(w.error))
        {
            Debug.Log("w.text: " + w.text);
            //Return the json array and put it in the C# User class
            Check check = JsonUtility.FromJson<Check>(w.text);
            if (check.success == true)
            {
                //Check if there is any error in the class. If there is return the error
                if (check.error != "")
                {
                    Debug.LogError("An error occured.. " + check.error);
                }
                else
                {
                    Debug.Log("RM : " + RM.currency);
                    User.air_pollution = RM.airPollution;
                    User.soil_pollution = RM.soilPollution;
                    User.water_pollution = RM.waterPollution;
                    User.land_use = RM.landUse;
                    User.biodiversity = RM.biodiversity;
                    User.currency = RM.currency;
                    User.population = RM.population;
                    User.currency = RM.currency;
                    ParseTowardsResources();
                    //Login the user and redirect it to a new scene
                    Debug.Log("Succes!");
                }
                //If the JsonArary is empty return this string in the feedback
            }
            else
            {
                Debug.LogError("An error occured.");
            }

            //If the string is empty return this string in the feedback
        }
        else
        {
            // error
            Debug.LogError("An error occured.");
        }

    }*/

    public void LogOut()
    {
        User = null;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Invoke("LoadUrl", 2);
    }
    public void LoadUrl()
    {
        Application.OpenURL("http://perspectiveworks.nl/OLP/public/#scoreboard");
    }
}
public class Check
{
   public bool success;
   public  string error;
}