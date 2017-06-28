using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    public CameraController cam;
    public GameObject[] blocks;
    public GameObject dpObj;
    public Transform dpPos;
    public GameObject explosion;

    public float timer = 60;

    public bool isPlay = true;

    public float minScale = 1f, maxScale = 5f;

    public Vector3 checkBoxScale;

    public float h = 0;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        CreateBlock();
        StartCoroutine("CheckHeight");
    }

    public void CreateBlock()
    {
        if (isPlay)
        {
            GameObject g = Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
            g.layer = LayerMask.NameToLayer("FallingBlock");
            g.GetComponent<Block>().SetController();
            g.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
        }
    }

    IEnumerator CheckHeight()
    {
        while (isPlay)
        {
            RaycastHit hit;
            if (Physics.BoxCast(transform.position, checkBoxScale / 2, -transform.up, out hit, transform.rotation, Mathf.Infinity, LayerMask.GetMask("Block")))
            {
                h = transform.position.y - hit.distance;

                if (transform.position.y - hit.point.y < 5f)
                    transform.position += Vector3.up * 10f;
                else if (hit.distance > 10f)
                    transform.position -= Vector3.up * 5f;

                cam.movePos.y = hit.point.y;
                UIManager.instance.SetHeight(h);

            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void OnDrawGizmos()
    {

        float maxDistance = 30f;
        RaycastHit hit;
        // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
        bool isHit = Physics.BoxCast(transform.position, checkBoxScale / 2, -transform.up, out hit, transform.rotation, Mathf.Infinity, LayerMask.GetMask("Block"));

        Gizmos.color = Color.red;
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, -transform.up * hit.distance);
            Gizmos.DrawWireCube(transform.position + -transform.up * hit.distance, checkBoxScale / 2);
        }
        else
        {
            Gizmos.DrawRay(transform.position, -transform.up * maxDistance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
            Timer();
    }

    void Timer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            isPlay = false;
            ShotDP();
        }
        UIManager.instance.SetTimer((int)(timer / 60), (int)(timer % 60));
    }

    void ShotDP()
    {
        GameObject g1 = Instantiate(explosion,Vector3.zero + (Vector3.up *  (h/4)),Quaternion.identity);

       // GameObject g2 = Instantiate(dpObj, dpPos.position + (Vector3.up * (h / 4)), Quaternion.identity);
       // g2.GetComponent<Rigidbody>().AddForce(Vector3.back * 250f, ForceMode.Impulse);
    }

}
