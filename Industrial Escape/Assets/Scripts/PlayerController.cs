using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 dragStartPosition;
    private Rigidbody rb;
    private Animator humanAC;
    public GameManager gameManager;
    public int score = 0;
    public AudioSource src;
    public AudioClip s1, s2;
    public Leaderboard leaderboard;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        humanAC = GetComponent<Animator>();
    }

    void Update()
    {
        //swiping controls: left, right, up
        
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 dragDelta = Input.mousePosition - dragStartPosition;
            src.clip = s2;

            if (dragDelta.x < 0) // left swipes
            {
                src.Play();
                MoveLeft();
            }
            else if (dragDelta.x > 0) // right swipes
            {
                src.Play();
                MoveRight();
            }
            //else if (dragDelta.y > 0 && transform.position.y < 1.5f) // jump swipes
            //{
             //   src.Play();
             //   Jump();
              
            //}
        }
    }
    void MoveLeft()
    {
        if (transform.position.x <= 0)
        {
            transform.DOMoveX(-1f, 0.5f);
        }
        else if(transform.position.x > 0.3f)
        {
            transform.DOMoveX(0, 0.5f);
        }
        else
        {
            transform.DOMoveX(-1f, 0.5f);
        }
        isDragging = false;
    }
    void MoveRight()
    {
        if (transform.position.x >= 0)
        {
            transform.DOMoveX(1, 0.5f);
        }
        else if(transform.position.x < -0.3f)
        {
            transform.DOMoveX(0, 0.5f);
        }
        else
        {
            transform.DOMoveX(1, 0.5f);
        }
        isDragging = false;
    }

    void Jump()
    {
        JumpAnimation();
        transform.DOMoveY(transform.position.y + 1.5f, 0.5f).OnComplete(() => humanAC.SetBool("isJumped", false));
        isDragging = false;
    }
    private void JumpAnimation()
    {
        humanAC.SetBool("isJumped",true);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("game over");
            gameManager.GameOver(score);
            StartCoroutine(DieRoutine());
            
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("coin"))
        {
            src.clip = s1;
            src.Play();
            Destroy(other.gameObject);
            score = score + 1;
            gameManager.PrintScore(score);
        }
    }

    IEnumerator DieRoutine()
    {
        yield return leaderboard.SubmitScoreRoutine(score);
    }
}