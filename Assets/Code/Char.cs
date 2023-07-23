using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char : MonoBehaviour
{
    public float Speed = 5f;
    public float SSpeed = 10f;
    public float Jump = 7f;
    public float hurt = 200f;
    public float wall = 5f;
    public bool gc = false;
    public bool wc = false;
    public bool GroundTouch = false;
    public Text countText;
    public Text winText;
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        gc = true;
        wc = true;
        count = 0;
        SetCountText();
    }


    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += Vector3.forward * Time.deltaTime * SSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * Time.deltaTime * Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * Speed;
        }
        if (Input.GetKey(KeyCode.Space) && GroundTouch == true)
        {
            transform.position += Vector3.up * Time.deltaTime * Jump ;
        }
        Vector3 gc = transform.TransformDirection(Vector3.down);
        Vector3 wc = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, gc, 0.5f))
        {
            GroundTouch = true;
        }
        if (Physics.Raycast(transform.position, wc, 0.4f))
        {
            transform.position += Vector3.back * Time.deltaTime * wall;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            transform.position += Vector3.back * Time.deltaTime * hurt;
            count = count - 1;
            SetCountText ();
            if (count <= -1)
            {
                Destroy (gameObject);
                countText.text = "";
                winText.text = "You lose :(";
            }
        }
        if (other.gameObject.CompareTag("Finish"))
            {
                countText.text = "";
                winText.text = "Congrats you win ! ! !";
            }
        
    }
    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString ();
        
    }


    // Update is called once per frame
    void Update()
    {
    }
}
