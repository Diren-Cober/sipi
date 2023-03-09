using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
[SerializeField] private float speed = 3f; //СКОРОСТЬ
[SerializeField] private int lives = 5; //ЖИЗНИ
[SerializeField] private float jumpForce = 5f; //СИЛА ПРЫЖКА
private bool isGrounded = false; //Чек на землю


private Rigidbody2D rb;
private SpriteRenderer sprite;

private void Awake() 
{
    rb = GetComponent<Rigidbody2D>();
    sprite = GetComponentInChildren<SpriteRenderer>();
}
private void Update()
{
    if (Input.GetButton("Horizontal"))
        Run();
    if (isGrounded && Input.GetButtonDown("Jump"))
        Jump();
    //if (Input.GetButton("Vertical"))
    //Run1();
    //else if (!Input.GetButton("Vertical")) - ДЛЯ ДЖЕТПАКА
    //Run2();
}
private void FixedUpdate() 
{
    CheckGround();
}
private void Jump()
{
    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
}
private void Run()
{
    Vector3 dir = transform.right * Input.GetAxis("Horizontal");
    transform.position = Vector3.MoveTowards(transform.position, transform.position+dir, speed * Time.deltaTime);
    
    sprite.flipX = dir.x < 0.0f;
    
    //Vector3 dir1 = transform.up * Input.GetAxis("Vertical");
    //transform.position = Vector3.MoveTowards(transform.position, transform.position+dir1, speed * Time.deltaTime);
}
private void CheckGround()
{
    Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.8f);
    isGrounded = collider.Length > 1;
}
//private void Run1()
//{
//    Vector3 dir1 = transform.up * Input.GetAxis("Vertical");
//    transform.position = Vector3.MoveTowards(transform.position, transform.position+dir1, speed * Time.deltaTime);
    // rb.AddForce (transform.up * speed); JETPACK!!!!!!!
//}
//private void Run2()
//{
//   rb.AddForce(transform.up * -speed);            ЗАБАВНАЯ ДИЧЬ С ДЖЕТ ПАКОМ
//}
}
