using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    float movementZ;

    bool justJumped;
    bool isGrounded;

    public string scene;
    public string gameOverScene;
    public string mainMenuScene;

    public static string previousScene;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        justJumped = false;
        isGrounded = true;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementZ = 0.0f;

        if (justJumped)
        {
            movementZ = speed;
            justJumped = false;
        }
        Vector3 movement = new Vector3(movementX, movementZ * 2.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        if(!justJumped && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            justJumped = true;
            isGrounded = false;
        }
        if(GameObject.Find("Player").transform.position.y < -15)
        {
            previousScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(gameOverScene);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(mainMenuScene);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(scene);
        }
        
    }
}
