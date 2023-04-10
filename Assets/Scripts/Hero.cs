using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
[SerializeField] private float speed = 3f; //СКОРОСТЬ
[SerializeField] private int lives = 5; //ЖИЗНИ
[SerializeField] private float jumpForce = 5f; //СИЛА ПРЫЖКА
private bool _onGround = false; //Чек на землю
[SerializeField] private GameObject _groundCheckObj;
[SerializeField] private float _radiusGroundCheck = 1f;
[SerializeField] LayerMask _groundMask;

private Rigidbody2D rb; //объявляем ригидбоди
private SpriteRenderer sprite; //объявляем спрайт
private Animator anim;
private States State  
{
    get {return (States)anim.GetInteger("state");}
    set {anim.SetInteger("state", (int)value);}
}

private void Awake() 
{
    //По сути "будим переменные и даем им значения"
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    sprite = GetComponentInChildren<SpriteRenderer>();
}
private void Update()
{
    if (_onGround) State = States.idle;
    if (Input.GetButton("Horizontal")) //Чек на горизонтальную ходьбу
        Run();
    if (Input.GetButtonDown("Jump") && _onGround == true) //Чек на прыжок"
        Jump();
    //if (Input.GetButton("Vertical"))
    //Run1();
    //else if (!Input.GetButton("Vertical")) - ДЛЯ ДЖЕТПАКА
    //Run2();
}
private void FixedUpdate() 
{
    GroundCheck();
    //CheckGround(); //Чек на землю
}
private void Jump()
{
    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //Код прыжка
    
    //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
}
private void Run()
{
    if (_onGround) State = States.run;
    Vector3 dir = transform.right * Input.GetAxis("Horizontal");
    transform.position = Vector3.MoveTowards(transform.position, transform.position+dir, speed * Time.deltaTime); //Код ходьбы
    
    sprite.flipX = dir.x < 0.0f; //Разворот при ходьбе назад
    
    //Vector3 dir1 = transform.up * Input.GetAxis("Vertical");
    //transform.position = Vector3.MoveTowards(transform.position, transform.position+dir1, speed * Time.deltaTime);
}
private void GroundCheck()
{
    _onGround = Physics2D.OverlapCircle(_groundCheckObj.transform.position, _radiusGroundCheck, _groundMask);
   
    if(!_onGround) State = States.jump;
    //Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.8f); //(не совсем врубаю как оно работает, но оно помогает чекнуть землю)
    //isGrounded = collider.Length > 1; //(тоже дичь для чека земли, погуглите если че.)
}
public void OnDrawGizmos()
{
    Gizmos.color = Color.magenta;
    Gizmos.DrawWireSphere(_groundCheckObj.transform.position, _radiusGroundCheck);
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

public enum States
{
    idle,
    run,
    jump
}
}
