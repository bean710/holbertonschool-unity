using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    private SceneChanger sc;

    public void Back()
    {
        SceneChanger.LastScene();
    }
}
