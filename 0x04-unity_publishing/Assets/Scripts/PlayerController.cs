using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 200;
    public int health = 5;

    public Text scoreText;
    public Text healthText;
    public Text winLoseText;

    private Rigidbody rb3d;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb3d = GetComponent<Rigidbody>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckHealth();

        if (Input.GetButton("Cancel"))
        {
            SceneManager.LoadScene("menu");
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        movement = movement * speed * Time.deltaTime;
        rb3d.AddForce(movement);
    }

    private void CheckHealth()
    {
        if (health == 0)
        {
            AnnounceLoss();
            StartCoroutine(LoadScene(3));
        }
    }

    void OnTriggerEnter(Collider col)
    {
        switch (col.tag)
        {
            case "Pickup":
                score++;
                SetScoreText();
                Destroy(col.gameObject);
                break;
            case "Trap":
                health--;
                SetHealthText();
                break;
            case "Goal":
                AnnounceWin();
                break;
        }
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }

    void AnnounceWin()
    {
        GameObject parent = winLoseText.transform.parent.gameObject;
        Image pImg = parent.GetComponent<Image>();

        winLoseText.text = "You Win!";
        winLoseText.color = Color.black;
        pImg.color = Color.green;
        parent.SetActive(true);
    }

    void AnnounceLoss()
    {
        GameObject parent = winLoseText.transform.parent.gameObject;
        Image pImg = parent.GetComponent<Image>();

        winLoseText.text = "Game Over!";
        winLoseText.color = Color.white;
        pImg.color = Color.red;
        parent.SetActive(true);
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        health = 5;
        score = 0;
        SceneManager.LoadScene("maze");
    }
}
