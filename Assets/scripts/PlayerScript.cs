using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    // 获取刚体，用以改变物体的速度
    public Rigidbody2D rigidBody2D;

    // 移动速度
    public float speed = 5f;

    //上跳参数
    public float jumpForce;

    // 动画组件
    public Animator animator;

    public LayerMask ground;

    public BoxCollider2D boxCollider2D;

    bool jumpPressed;

    int jumpCount;

    bool test = false; // 第一次提交之后新增了这个变量

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();
        switchAnim();
    }

    private void movement() {
        float move = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");

        // 移动
        //if(move != 0) {
        rigidBody2D.velocity = new Vector2(speed * move * Time.deltaTime, rigidBody2D.velocity.y);
        //}
        if(move != 0) {
            animator.SetFloat("running",Mathf.Abs(faceDirection));
        }

        // 修改左右面向
        if(faceDirection != 0) {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }

        //处理跳跃
        if(Input.GetButtonDown("Jump")) {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, jumpForce * Time.deltaTime);

            animator.SetBool("jumping",true);
        
        }
    }

    private void jump() {
        
    }

    void switchAnim() {

        animator.SetBool("idle",false);

        if(animator.GetBool("jumping")) {
            if(rigidBody2D.velocity.y < 0) {
                animator.SetBool("jumping",false);
                animator.SetBool("falling",true);
            }
        }else if(boxCollider2D.IsTouchingLayers(ground)) {
                animator.SetBool("falling",false);
                animator.SetBool("idle",true);
        }
    }
}
