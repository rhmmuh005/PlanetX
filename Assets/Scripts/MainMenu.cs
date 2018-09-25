using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public Sprite[] sprites;
    public Image image;
    private int index;
    public TMPro.TextMeshProUGUI text;
    string[] strings;

    public Transform t;

    // Use this for initialization
    void Start () {
        index = 0;
        image.sprite = sprites[0];
        strings = new string[3];
        strings[0] = "Left Click to shoot";
        strings[1] = "Point in direction you want to face with  mouse";
        strings[2] = "Press w to walk forward, hold shift to run. While running you can press shift roll";
        

        text.SetText(0 + 1 + "/" + sprites.Length + " " + strings[0]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NextImage()
    {
        if (index == sprites.Length - 1)
            index = 0;
        else
            index++;

        image.sprite = sprites[index];
        text.SetText(index + 1 + "/" + sprites.Length +" "+ strings[index]);
    }

    public void PreviousImage()
    {
        if (index == 0)
            index = sprites.Length-1;
        else
            index--;

        image.sprite = sprites[index];
        text.SetText(index+1+"/"+sprites.Length+" "+strings[index]);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Enter Lobby");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void ShowControls()
    {
        t.gameObject.SetActive(true);
    }

    public void HideControls()
    {
        t.gameObject.SetActive(false);
    }
}
