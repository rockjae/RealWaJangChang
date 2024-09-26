using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FZBossAmmo : MonoBehaviour
{
    bool isRight;
    bool isLeft;
    public int mode;
    public bool reverse;
    float jumpTimer;
    float cloneTimer;

    // Update is called once per frame
    void Update()
    {
        jumpTimer += Time.deltaTime;
        cloneTimer += Time.deltaTime;
        switch (mode)
        {
            case 0:
                {
                    if (reverse)
                    {
                        if (!isLeft)
                        {
                            transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x > 2)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                                isLeft = true;
                            }
                        }
                        else
                        {
                            transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x < -2)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isLeft = false;
                            }
                        }
                    }
                    else
                    {
                        if (isRight)
                        {
                            transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x > 2)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                                isRight = false;
                            }
                        }
                        else
                        {
                            transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x < -2)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isRight = true;
                            }
                        }
                    }
                    break;
                }
            case 1:
                {
                    if (reverse)
                    {
                        if (!isLeft)
                        {
                            transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x > 2)
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                                transform.eulerAngles = new Vector3(0, 180, 0);
                                isLeft = true;
                            }
                        }
                        else
                        {
                            transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x < -2)
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isLeft = false;
                            }
                        }
                    }
                    else
                    {
                        if (isRight)
                        {
                            transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x > 2)
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                                transform.eulerAngles = new Vector3(0, 180, 0);
                                isRight = false;
                            }
                        }
                        else
                        {
                            transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x < -2)
                            {
                                transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isRight = true;
                            }
                        }
                    }

                    
                    break;
                }
            case 2:
                {
                    if (jumpTimer > 1)
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
                        jumpTimer = 0;
                    }

                    if (reverse)
                    {
                        if (!isLeft)
                        {
                            transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x > 2)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                                isLeft = true;
                            }
                        }
                        else
                        {
                            transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x < -2)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isLeft = false;
                            }
                        }
                    }
                    else
                    {
                        if (isRight)
                        {
                            transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x > 2)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                                isRight = false;
                            }
                        }
                        else
                        {
                            transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x < -2)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isRight = true;
                            }
                        }
                    }
                    
                    break;
                }
            case 3:
                {
                    if (jumpTimer > 1)
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
                        jumpTimer = 0;
                    }
                    break;
                }
            case 4:
                {
                    if (cloneTimer > 1)
                    {
                        int ran = Random.Range(0, 2);
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
                        switch (ran)
                        {
                            case 0:
                                {
                                    transform.GetChild(0).gameObject.SetActive(true);
                                    transform.GetChild(1).gameObject.SetActive(false);
                                    break;
                                }
                            case 1:
                                {
                                    transform.GetChild(0).gameObject.SetActive(false);
                                    transform.GetChild(1).gameObject.SetActive(true);
                                    break;
                                }
                        }
                        cloneTimer = 0;
                    }

                    if (reverse)
                    {
                        if (!isLeft)
                        {
                            transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x > 2)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                                isLeft = true;
                            }
                        }
                        else
                        {
                            transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x < -2)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isLeft = false;
                            }
                        }
                    }
                    else
                    {
                        if (isRight)
                        {
                            transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x > 2)
                            {
                                transform.eulerAngles = new Vector3(0, 180, 0);
                                isRight = false;
                            }
                        }
                        else
                        {
                            transform.position -= new Vector3(2 * Time.deltaTime, 0, 0);

                            if (transform.position.x < -2)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 0);
                                isRight = true;
                            }
                        }
                    }
                    break;
                }
        }
    }
}
