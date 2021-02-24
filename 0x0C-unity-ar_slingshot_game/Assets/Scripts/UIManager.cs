using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public SlingshotManager slingshotManager;
    public Text promptText;
    public Image promptPanel;

    public Text pointsText;
    public List<Image> ammoImages;

    private bool planeDetected = false;

    // Start is called before the first frame update
    void Start()
    {
        arPlaneManager.planesChanged += PlaneAdded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaneAdded(ARPlanesChangedEventArgs args)
    {
        if (planeDetected)
            return;

        if (args.added.Count == 0)
            return;

        planeDetected = true;

        promptText.text = "Select a plane to use as your playfield by tapping on it.";
    }

    public void PlaneSelected()
    {
        promptPanel.color = new Color32(0, 255, 0, 60);
        promptText.text = "Plane selected!";

        StartCoroutine(CloseSelectionDisplay(3f));
    }

    IEnumerator CloseSelectionDisplay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Calling load ammo");
        slingshotManager.LoadAmmo();

        promptPanel.color = new Color32(255, 255, 255, 60);
        promptText.text = "Pull back on the ball to launch at the enemies!";
    }

    public void UseAmmo(int ammoLeft)
    {
        ammoImages[ammoLeft - 1].color = new Color32(255, 255, 255, 85);
    }

    public void ScorePoints(int points)
    {
        
    }
}
