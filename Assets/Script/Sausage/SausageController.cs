using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SausageController : MonoBehaviour
{
    private Transform Sausage;
    private Rigidbody2D SausageRig;
    

    [SerializeField]
    public float speed = 1f;
    [SerializeField]
    private float jumpforce = 10f;
    private float touchTimer;

    [HideInInspector]
    public bool isJump;
    [HideInInspector]
    public bool reverse = false;
    [HideInInspector]
    public bool isGlue = false;

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
    private int HP;
    [SerializeField]
    private GameObject[] hpGameOBJ;
    [SerializeField]
    private AudioSource thunderHit;

    [Header("Hit detection")]
    [SerializeField]
    SpriteRenderer sr;
    [SerializeField]
    GameObject go;
    [SerializeField]
    private float unBeatTime;
    [SerializeField]
    private AnimationCurve fadeCurve;

    [SerializeField]
    private float fadeSpeed;
    bool unBeatState;

    [Header("Game Over")]
    public FieldOutGameOver fieldOutGameOver;

    [Header("Sausage Attack")]
    [SerializeField]
    float speedOfAttack = 0f;
    [SerializeField]
    private GameObject[] ammoPrefab;
    [SerializeField]
    private GameObject player;




    private void Awake()
    {
        Sausage = gameObject.transform;
        SausageRig = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        SausageMobileMove();
        SausageKeyboardMove();
    }

    private void SausageMobileMove()
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
        Sausage.position += Sausage.eulerAngles.y == 0 ? new Vector3(speed * Time.deltaTime, 0, 0) : new Vector3(-speed * Time.deltaTime, 0, 0);
    }
    private void SausageKeyboardMove()
    {
        if(!reverse)
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
        else if(reverse)
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

        Sausage.position += Sausage.eulerAngles.y == 0 ? new Vector3(speed * Time.deltaTime, 0, 0) : new Vector3(-speed * Time.deltaTime, 0, 0);
    }

    void moveLeft()
    {
        if(isGlue)
        {
            Debug.Log("capture");
            isGlue = false;
            isJump = false;
            speed = 1f;
        }
        else
        {
            Sausage.eulerAngles = new Vector3(0, 180, 0);
        }
        
    }
    void moveRight()
    {
        if(isGlue)
        {
            Debug.Log("capture");
            isGlue = false;
            isJump = false;
            speed = 1f;
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
            speed = 1f;
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
            if(Sausage.eulerAngles.y==0) //벽에 닿으면 방향꺽음
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
                isJump = false;
                reverse = true;
            }
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("reverseGround"))
        {
            if (GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                isJump = false;
                reverse = false;
            }
        }
    }


    public void HpMakeStart()
    {
        for(int i = 0; i < hpGameOBJ.Length; i++)
        {
            hpGameOBJ[i].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("ammo"))
        {
            thunderHit.Play();
            if (!unBeatState)
            {
                sr = go.GetComponent<SpriteRenderer>();
                unBeatState = true;
                Invoke("BeatState", unBeatTime);
                StartCoroutine(HitDetection());         
                if(HP > 1)
                {
                    hpGameOBJ[HP-1].SetActive(false);
                    HP--;
                }
                else
                {
                    hpGameOBJ[HP - 1].SetActive(false);
                    HP--;
                    fieldOutGameOver.FOGameOver();
                }

                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag.Equals("mwboss"))
        {
            if (!unBeatState)
            {
                sr = go.GetComponent<SpriteRenderer>();
                unBeatState = true;
                Invoke("BeatState", unBeatTime);
                StartCoroutine(HitDetection());
                if (HP > 1)
                {
                    hpGameOBJ[HP - 1].SetActive(false);
                    HP--;
                }
                else
                {
                    hpGameOBJ[HP - 1].SetActive(false);
                    HP--;
                    fieldOutGameOver.FOGameOver();
                }
            }
        }
    }

    private IEnumerator HitDetection()
    {
        
        while (true)
        {
            
            yield return StartCoroutine(Fade(1, 0));
            yield return StartCoroutine(Fade(0, 1));

            if (!unBeatState)
            {
                break;
            }
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeSpeed;

            Color color = sr.color;
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            sr.color = color;

            yield return null;
        }
    }

    void BeatState()
    {
        unBeatState = false;
    }

    public void SausageAttackStart(int ammoIndex)
    {
        StartCoroutine(AttackStart(ammoIndex));
    }

    private IEnumerator AttackStart(int ammoIndex)
    {
        float timer = 0;
        IEnumerator attck_cor = Attack(ammoIndex);
        StartCoroutine(attck_cor);
        while(true)
        {
            timer += Time.deltaTime;

            if (timer > 3f)
            {
                StopCoroutine(attck_cor);
                break;
            }

            yield return null;
            //if( boss.isDie == true) { 보스 사망전까지 계속 발사

            //}
        }
    }

    private IEnumerator Attack(int ammoIndex)
    {
        while (true)
        {
            GameObject ammo;
            ammo = Instantiate(ammoPrefab[ammoIndex], Sausage.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(speedOfAttack);
            //if( boss.isDie == true) { 보스 사망전까지 계속 발사

            //}
        }
    }
}
