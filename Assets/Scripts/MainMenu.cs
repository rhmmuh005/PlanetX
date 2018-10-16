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
        if(sprites.Length!=0)
            image.sprite = sprites[0];
        strings = new string[4];
        //instruction messages for controls in game
        strings[0] = "Point in direction you want to face with  mouse. Press w to walk forward, hold shift to run.";
        strings[1] = "While running you can press Space roll.";
        strings[2] = "Left Click to shoot.";
        strings[3] = "Enemies drop items when killed, press E when near to pick them up.";
        
        if(text!=null)
        text.SetText(0 + 1 + "/" + sprites.Length + " " + strings[0]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //show the next image
    public void NextImage()
    {
        if (index == sprites.Length - 1)
            index = 0;
        else
            index++;

        image.sprite = sprites[index];
        text.SetText(index + 1 + "/" + sprites.Length +" "+ strings[index]);
    }

    //show the previous image
    public void PreviousImage()
    {
        if (index == 0)
            index = sprites.Length-1;
        else
            index--;

        image.sprite = sprites[index];
        text.SetText(index+1+"/"+sprites.Length+" "+strings[index]);
    }

    //enter the lobby
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Enter Lobby");
    }

    //exit the game
    //only works in build.
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    //show the controls panel so player knows buttons
    public void ShowControls()
    {
        t.gameObject.SetActive(true);
    }

    //hide the controls panel
    public void HideControls()
    {
        t.gameObject.SetActive(false);
    }
}
