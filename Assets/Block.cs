using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public bool isControll = false;

    public float angle = 0, moveSpeed = 10f, rotateSpeed = 5f;
    Transform tr;
    Rigidbody ri;

    [SerializeField]
    float h = 0;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        ri = GetComponent<Rigidbody>();
    }

    public void SetController()
    {
        isControll = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlay && isControll)
        {
            h = Input.GetAxis("Horizontal");


            if (Input.GetKey(KeyCode.Space))
            {
                angle += Time.deltaTime * rotateSpeed;
                tr.Rotate(Vector3.forward, angle);
            }

        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isPlay && isControll)
            Move();
    }

    void Move()
    {
        ri.velocity = new Vector3(moveSpeed * h, ri.velocity.y, ri.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isControll && (collision.gameObject.CompareTag("Block") || collision.gameObject.CompareTag("Destroy")))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Block");
            isControll = false;
            GameManager.instance.CreateBlock();
        }

        if (collision.gameObject.CompareTag("Destroy"))
            Destroy(gameObject);
    }
}
