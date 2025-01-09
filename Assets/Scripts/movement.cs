using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class movement : NetworkBehaviour
{
    private float moveSpeed = 3f;
    Animator boxerAnimator;
    Rigidbody2D rb;
    private bool isWalk;
    private bool backWalk;
    public GameObject target;
    private bool isTargetBehind;

    // Start is called before the first frame update
    void Start()
    {   if (IsServer && IsOwner)
        {
            transform.position = new Vector3(-6, 3, 0);
        }
        /*
        else if (IsOwner) {
            transform.position = new Vector3(6, 3, 0);
        }*/
        
        target = GameObject.Find("Square (1)");
        boxerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isWalk = false;
        backWalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        RotationSetter();
        MovementSetter();
        BoxMovement();
        if (Input.GetKeyDown(KeyCode.F)) {
            boxerAnimator.Play("RightJab");
        }

        boxerAnimator.SetBool("isWalking", isWalk);
        boxerAnimator.SetBool("backWalking", backWalk);


    }

    void RotationSetter() {
        if (target.transform.position.x < transform.position.x)
        {
            isTargetBehind = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else {
            isTargetBehind = false;
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    
    }

    void MovementSetter() {
        if (Input.GetKey(KeyCode.A))
        {
            if (isTargetBehind)
            {
                //StartCoroutine(MovePlayer(Vector3.left));
                isWalk = true;
                backWalk = false;
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
            else
            {
                backWalk = true;
                isWalk = false;
            }
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (isTargetBehind)
            {
                backWalk = true;
                isWalk = false;
            }
            else
            {
                isWalk = true;
                backWalk = false;
            }
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        if (rb.velocity.magnitude < 2f)
        {
            isWalk = false;
            backWalk = false;
        }

    }

    void BoxMovement() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            target.transform.position =  target.transform.position + new Vector3(-1f, 0f, 0f);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow)){
            target.transform.position = target.transform.position + new Vector3(1f, 0f, 0f);
        }
    }
    IEnumerator MovePlayer(Vector3 pos)
    {
    
        Vector3 startposition = transform.position;
        Vector3 endposition = transform.position + pos;

        float elapsedTime = 0f;
        while (elapsedTime < 1f / moveSpeed)
        {
            transform.position = Vector3.Lerp(startposition, endposition, elapsedTime * moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;

        }

        transform.position = endposition;
     


    }
}
