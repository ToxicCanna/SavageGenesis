using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    [SerializeField] int sceneBuildIndex;

    //if player collider entered warp tile move to other scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");

        // could use pther.Component<Player>() to see if the object has a player component
        //tags work too. maybe some players have different script components
        if(other.tag == "Player")
        {
            //player entered, switch level
            print("Switching to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
            PlayerPrefs.DeleteAll();
        }
    }
}
