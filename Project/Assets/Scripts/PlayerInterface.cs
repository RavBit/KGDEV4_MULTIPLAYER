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
        name.text = "You are: " + GetComponentInParent<Player>().gameObject.name;
	}
    public void AdjustNeedle(float force)
    {
        forceFill.localScale = new Vector3(1f, force, 1f);
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
    }
    void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PlayerController.LockCursor();
        PauseMenu.isOn = pauseMenu.activeSelf;
    }
}
