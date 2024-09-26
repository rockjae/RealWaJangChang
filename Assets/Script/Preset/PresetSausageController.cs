using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PresetSausageController : MonoBehaviour
{
    private Transform Sausage;
    private Rigidbody2D SausageRig;


    [SerializeField]
    private float speed = 1f;
    public float SausageSpeed;
    [SerializeField]
    private float jumpforce = 10f;
    private float touchTimer;

    [HideInInspector]
    public bool isJump;
    [HideInInspector]
    public bool reverse = false;
    [HideInInspector]
    public bool isGlue = false;
    [Header("Hit")]
    public PresetMain presetMain;
    public AudioSource SausageHitSound;
    protected bool isHit;

    private Vector2 touchBeganPos;
    private Vector2 touchEndedPos;
    private Vector2 touchDif;
    private float swipeSensitivity = 100f;


    [SerializeField]
    public FollowCamera followCamera;
    [SerializeField]
    public GameObject BackGround;

    [Header("HP state")]
    [SerializeField]
    protected int HP;
    [SerializeField]
    protected GameObject[] hpGameOBJ;

    private void Awake()
    {
        Sausage = gameObject.transform;
        SausageRig = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        SausageSpeed = speed;
    }


    // Update is called once per frame
    void Update()
    {
        SausageMobileMove();
        SausageKeyboardMove();
    }

    protected void SausageMobileMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("touch");

            if (touch.phase == TouchPhase.Began)
            {
                touchBeganPos = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                touchEndedPos = touch.position;
                touchDif = (touchEndedPos - touchBeganPos);

                //스와이프. 터치의 x이동거리나 y이동거리가 민감도보다 크면
                if (Mathf.Abs(touchDif.x) > swipeSensitivity)
                {
                    if (!reverse)
                    {
                        if (touchDif.x > 0)// && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                        {
                            Debug.Log("right");
                            moveRight();
                        }
                        else if (touchDif.x < 0)// && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                        {
                            Debug.Log("Left");
                            moveLeft();
                        }
                    }
                    else
                    {
                        if (touchDif.x > 0)// && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                        {
                            Debug.Log("right");
                            moveLeft();

                        }
                        else if (touchDif.x < 0)// && Mathf.Abs(touchDif.y) < Mathf.Abs(touchDif.x))
                        {
                            Debug.Log("Left");
                            moveRight();
                        }
                    }
                }
                //터치.
                else
                {
                    Debug.Log("touch");
                    moveJump();
                }
            }
        }
        Sausage.position += Sausage.eulerAngles.y == 0 ? new Vector3(SausageSpeed * Time.deltaTime, 0, 0) : new Vector3(-SausageSpeed * Time.deltaTime, 0, 0);
    }
    protected void SausageKeyboardMove()
    {
        if (!reverse)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveLeft();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveRight();
            }
        }
        else if (reverse)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                moveRight();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                moveLeft();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveJump();

        }

        Sausage.position += Sausage.eulerAngles.y == 0 ? new Vector3(SausageSpeed * Time.deltaTime, 0, 0) : new Vector3(-SausageSpeed * Time.deltaTime, 0, 0);
    }

    void moveLeft()
    {
        if (isGlue)
        {
            Debug.Log("capture");
            isGlue = false;
            isJump = false;
            SausageSpeed = speed;
        }
        else
        {
            Sausage.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    void moveRight()
    {
        if (isGlue)
        {
            Debug.Log("capture");
            isGlue = false;
            isJump = false;
            SausageSpeed = speed;
        }
        else
        {
            Sausage.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    void moveJump()
    {
        if (isGlue)
        {
            Debug.Log("capture");
            isGlue = false;
            isJump = false;
            SausageSpeed = speed;
        }
        else
        {
            if (!isJump)
            {
                isJump = true;
                SausageRig.AddForce(Vector3.up * jumpforce, ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("ground"))
        {
            if (GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                isJump = false;
            }
        }
        else if (collision.gameObject.tag.Equals("side"))
        {
            if (Sausage.eulerAngles.y == 0) //벽에 닿으면 방향꺽음
            {
                Sausage.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                Sausage.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else if (collision.gameObject.tag.Equals("reverseGround"))
        {
            if (GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                Debug.Log("reverse start");
                isJump = false;
                reverse = true;
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("reverseGround"))
        {
            reverse = false;
            if (GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                Debug.Log("reverse Exit");
                isJump = false;
                
            }
        }
    }

    public void HpMakeStart()
    {
        for (int i = 0; i < hpGameOBJ.Length; i++)
        {
            hpGameOBJ[i].SetActive(true);
        }
    }

    public IEnumerator hitSausage()
    {
        float timer = 0;

        hpGameOBJ[HP - 1].SetActive(false);
        HP--;
        SausageHitSound.Play();

        isHit = true;

        if (HP == 0)
        {
            presetMain.FOGameOver();
        }

        SpriteRenderer s = GetComponent<SpriteRenderer>();
        Color c = s.color;

        while (true)
        {
            timer += Time.deltaTime;

            if (timer < 0.5f)
            {
                c.a -= 2 * Time.deltaTime;
            }
            else if (timer > 0.5f && timer < 1)
            {
                c.a += 2 * Time.deltaTime;
            }
            else if (timer > 1.5f && timer < 2)
            {
                c.a -= 2 * Time.deltaTime;
            }
            else if (timer > 2f && timer < 2.5f)
            {
                c.a += 2 * Time.deltaTime;
            }
            else if (timer > 2.5f)
            {
                c.a = 1;
                s.color = c;
                break;
            }

            s.color = c;

            yield return null;
        }

        isHit = false;
        yield return null;
    }
}
