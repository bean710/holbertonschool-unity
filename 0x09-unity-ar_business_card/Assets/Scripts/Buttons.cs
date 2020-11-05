using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public AudioSource clickSound;

    private void Start() {
        Debug.Log("Started Click script");
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                string hitName = Hit.transform.name;
                string url = null;

                switch (hitName)
                {
                    case "Twitter":
                        url = "http://twitter.com/TheBenKeener";
                        break;
                    case "GitHub":
                        url = "http://github.com/bean710";
                        break;
                    case "Medium":
                        url = "http://medium.com/@thebenkeener";
                        break;
                    case "Email":
                        url = "mailto:benjamin.keener@gmail.com";
                        break;
                    case "LinkedIn":
                        url = "https://www.linkedin.com/in/ben-keener/";
                        break;
                    case "Subtext":
                        url = "http://benkeener.com";
                        break;
                    default:
                        break;
                }

                if (url != null)
                {
                    clickSound.Play();
                    Application.OpenURL(url);
                }
            }
        }
    }
}
