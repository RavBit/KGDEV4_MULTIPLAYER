using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInterface : MonoBehaviour {
    [SerializeField]
    private Image needle;

    [SerializeField]
    private Text name;
    [SerializeField]
    RectTransform forceFill;

    [SerializeField]
    RectTransform healthFill;

    [SerializeField]
    GameObject pauseMenu;
	// Use this for initialization
	void Start () {
        PauseMenu.isOn = false;
	}
    public void AdjustHealth(float health)
    {
        healthFill.localScale = new Vector3(1f, health, 1f);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        AdjustHealth(GetComponentInParent<Player>().GetCurrentHealth() / 100);
        name.text = "You are: " + GetComponentInParent<Player>().gameObject.name + " Hunter: " + GetComponentInParent<Player>()._hunter;
    }

    //Toggle Pause
    void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PlayerController.LockCursor();
        PauseMenu.isOn = pauseMenu.activeSelf;
    }
}
