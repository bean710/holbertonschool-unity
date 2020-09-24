using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneChanger.ChangeScene("MainMenu");
    }

    public void Next()
    {
        switch (SceneChanger.CurrentScene())
        {
            case "Level01":
                SceneChanger.ChangeScene("Level02");
                break;
            case "Level02":
                SceneChanger.ChangeScene("Level03");
                break;
            case "Level03":
                SceneChanger.ChangeScene("MainMenu");
                break;
            default:
                break;
        }
    }
}
