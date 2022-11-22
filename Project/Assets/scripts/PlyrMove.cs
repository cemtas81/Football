using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlyrMove : MonoBehaviour
{
    
    public float speed;
    private bool move;
    public Vector3 movePos;
    private Vector3 screenPoint;
    private Vector3 offset;
    public Rigidbody rigid;
    private Touch touch;
    public GameObject ball;
    public SoccerEnvController envController;
   
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        speed = 500;
#endif
#if UNITY_IPHONE
        speed=75f;
          
#endif
#if UNITY_ANDROID
        speed=75f;
        
#endif
        envController = envController.GetComponent<SoccerEnvController>();
    }
    // Mouse input when on screen
    void OnMouseDown()
    {
        if (envController.canMove==true)
        {
            screenPoint = Camera.main.WorldToScreenPoint(rigid.transform.position);

            offset = rigid.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
       else
            offset = Vector3.zero;

    }

    void OnMouseDrag()
    {
        if (envController.canMove == true)
        {
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            movePos = cursorPosition;
        }
        else
            movePos = Vector3.zero;
       
    }
    // Touch input
    private void Update()
    {
        //rigid.transform.LookAt(ball.transform.position);
        rigid.transform.forward = new Vector3(movePos.x, 0, movePos.z);


        if (envController.canMove==true)
        {
           
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                move = true;

                if (touch.phase == TouchPhase.Moved)
                {
                    movePos = new Vector3(-touch.deltaPosition.y, rigid.transform.position.y, touch.deltaPosition.x);
                }
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Stationary)
                {
                    movePos = Vector3.zero;
                }
            }
        }
        else
            movePos = Vector3.zero;
      

    }
    //Movement
    private void FixedUpdate()
    {
        if (envController.canMove==true)
        {
           
            //For touch
            if (move == true)
            {
                rigid.velocity = movePos * Time.deltaTime * speed;

            }
            //For mouse
            else

                rigid.velocity = (movePos - rigid.transform.position) * Time.deltaTime * speed;
        }
  
    }
    

}
