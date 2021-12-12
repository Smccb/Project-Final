using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //play button on main menu
    public void playGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//loads next scene based on what the current scene is
    }

    //quit Button on main menu
    public void quitGame() {
        Application.Quit();//closes game
    }

    //selecting levels on level selection menu
    public void playLevel1()
    {
        SceneManager.LoadScene(1);//loads level1 scene
    }
    public void playLevel2()
    {
        SceneManager.LoadScene(2);//loads level2 scene
    }
    public void playLevel3()
    {
        SceneManager.LoadScene(3);//loads level3 scene
    }

    //paused menu leave quit game and come back to start menu
    public void callStartMenu()
    {
        SceneManager.LoadScene(0);
    }
	
	public GameObject mySlider;
	
	void update(){
		GetComponent<AudioSource>().volume = mySlider.VolumeSlider.value;
	}
	


}
