using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class pl1 : MonoBehaviour
{
    public float movementspeed = 10.0f;
    public Rigidbody rb;
    float HChecker = 0;
    public SpawnManger Spawnmanger;
    CapsuleCollider orgn;
    // Use this for initialization
    [System.Serializable]
    public class MovementSettings
    {
        public float forwardVelocity = 10;
        public float jumpVelocity = 70;
    }
    [System.Serializable]
    public class PhysicsSettings
    {
        public float downAccel = 0.75f;
    }
    public MovementSettings movementSettings = new MovementSettings();
    public PhysicsSettings physicsSettings = new PhysicsSettings();
    public Vector3 v_Velocity;
    private Animator _animator;
    private float _colliderSize;
    private float _colliderhight;
    private Vector3 _colliderCentre;    
    private int _jumpInput = 0, _slideInput = 0;
    private bool _onGround = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        v_Velocity = Vector3.zero;
        _animator = GetComponent<Animator>();
        orgn = GetComponent<CapsuleCollider>();
        _colliderCentre = orgn.center;
        _colliderhight = orgn.height;
        StartCoroutine("keyy");
        StartCoroutine("K_jump");

        rb.mass = 10.0f;
    }
    private void FixedUpdate()
    {
        {
            Vector3 cur = transform.position;
            if (cur.x < -6.7f && cur.x > -6.9f)
            {
                cur.x = -6.80f;
            }
            else if (cur.x > -4.3f)
            {
                cur.x = -4.3f;
            }
            else if (cur.x < -8.8f) cur.x = -9.3f;
            transform.position = cur;
            if (HChecker > 0)
            {
                Vector3 mx;
                mx = transform.position;
                if (transform.position.x == -6.8f)
                    mx.x = -4.3f;
                else if (transform.position.x == -9.3f) mx.x = -6.8f;
                transform.position = mx;
            }
            else if (HChecker < 0)
            {
                Vector3 mx;
                mx = transform.position;
                if (transform.position.x == -6.8f)
                    mx.x = -9.3f;
                else if (transform.position.x == -4.3f) mx.x = -6.8f;
                transform.position = mx;
            }
            HChecker = 0;
        }
        InputHandling();
        Run();
       // CheckGround();
        Jump();
        Slide();
        rb.velocity = v_Velocity;
    }
    void Jump()
    {
        if (_jumpInput == 1 && _onGround)
        {
            v_Velocity.y = movementSettings.jumpVelocity * Time.deltaTime * movementspeed;
            _animator.SetTrigger("Jump");
            _onGround = false;
            // StartCoroutine("Gro",0.4f);
        }
        else if (_jumpInput == 0 && _onGround) { v_Velocity.y = 0f; }

        else { v_Velocity.y -= physicsSettings.downAccel * Time.deltaTime * 20;
            Vector3 down = rb.transform.position;

            if (down.y < -7.2) { down.y =-7.2f; rb.position = down; v_Velocity.y = 0; _onGround = true; } }
       
            _jumpInput = 0;

    }
    void CheckGround()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.3f, Vector3.down);
        RaycastHit[] hits = Physics.RaycastAll(ray, 0.5f);
        _onGround = false;
        rb.useGravity = true;
        foreach (var hit in hits)
        {
            if (!hit.collider.isTrigger)
            {
                if (v_Velocity.y <= 0)
                {
                   // rb.position = Vector3.MoveTowards(rb.position, new Vector3(hit.point.x, Mathf.Max(-7.3f,rb.position.y- physicsSettings.downAccel), hit.point.z), Time.deltaTime * 50);
                }
                rb.useGravity = false;
                _onGround = true; break;
            }
        }
    }
    void Slide()
    {
        if (_slideInput == 1 && _onGround)
        {
            _animator.SetTrigger("Slide");

            orgn.center = new Vector3(0f, -0.69f, 0f);
            orgn.height = .8f;
            v_Velocity.z = movementSettings.forwardVelocity;
            _slideInput = 0;
            StartCoroutine("SlideingBack");

        }
       // else { orgn.center = _colliderCentre;orgn.height = _colliderhight; }
    }
    void InputHandling()
    {
        float Sl = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _slideInput = 1;
        }

    }
    IEnumerator SlideingBack()
    {
        for (int i = 0; i < 2; i++)
        {
            if (i == 0) yield return new WaitForSeconds(.6f);
            else
            {
                orgn.center = _colliderCentre; orgn.height = _colliderhight;
                yield return new WaitForSeconds(.0f);
            }
        }
    }
    void Run()
    {
        v_Velocity.z = movementSettings.forwardVelocity;
    }
    //IEnumerator keyy()
    //{
    //    while (true)
    //    {
    //        while (HChecker==0.0)
    //        {
    //            Getinput();
    //        }
    //            if (HChecker > 0)
    //            {
    //                Vector3 mx;
    //    mx = transform.position;
    //                if (transform.position.x == -6.8f)
    //                    mx.x = -4.6f;
    //                else if (transform.position.x == -9f) mx.x = -6.8f;
    //                transform.position = mx;
    //            }
    //            else if (HChecker< 0)
    //            {
    //                Vector3 mx;
    //mx = transform.position;
    //                if (transform.position.x == -6.8f)
    //                    mx.x = -9f;
    //                else if (transform.position.x == -4.6f) mx.x = -6.8f;
    //                transform.position = mx;
    //            }
    //        HChecker = 0.0f;
    //        yield return new WaitForSeconds(.2f);
    //    }
    //}
    IEnumerator keyy()
    {
        while (true)
        {
            HChecker = Input.GetAxis("Horizontal");
            if (HChecker != 0) yield return new WaitForSeconds(0.15f);
            else { yield return new WaitForSeconds(0.01f); }

        }
    }
    IEnumerator K_jump()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                _jumpInput = 1;
                yield return new WaitForSeconds(.8f);
            }
            else yield return new WaitForSeconds(.01f);
        }
    }
    void Getinput()
    {
        HChecker = Input.GetAxis("Horizontal");
    }
    // Update is called once per frame
    //   void Update () {
    //	float h = Input.GetAxis ("Horizontal")*movementspeed;
    //	float v = Input.GetAxis ("Vertical")*movementspeed/2;

    //	transform.Translate (new Vector3 (h, 0, v)*Time.deltaTime);
    //}
    private void OnTriggerEnter(Collider other)
    {
        Vector3 down = rb.transform.position;

        if (other.gameObject.tag == "split")
        Spawnmanger.SpawonTriggerEnter();
         if(other.gameObject.tag == "land")
        {
            _onGround = true;
        }
        else down.y -= .1f;
        //rb.transform.position = down;
    }
    private void OnTriggerStay(Collider other)
    {
        Vector3 down = rb.transform.position;
        if (other.gameObject.tag == "land")
        {
            _onGround = true;
        }
        else down.y -= .1f;
     
    }

}
