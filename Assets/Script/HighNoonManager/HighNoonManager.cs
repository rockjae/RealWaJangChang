using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighNoonManager : MonoBehaviour
{
    public GameObject Boss;

    public Image img;
    private Color color;
    [HideInInspector]
    public bool isGame;
    [HideInInspector]
    public bool waitShot;

    public GameObject readyBtn;
    public GameObject sausageGun;
    public GameObject bossGun;
    private GameObject gun1;
    private GameObject gun2;

    private float ranTime;
    private float timer;
    private float timer2;
    public AudioSource shotSound;

    IEnumerator _imgBlack;

    int count;
    float score;
    public Text scoreText;
    public Text score1;
    public Text score2;
    public Text score3;
    public GameObject[] hp;
    bool isMistake;

    public GameObject mushroom;

    public GameObject GameOver;
    public AudioClip mainclip;
    [Header("Rank")]
    public Text LastScore;
    public HighNoonTopRank highNoonTopRank;
    public Text TopRankText;
    public GameObject RankingBtn;
    public GameObject rankNotiText;
    public GameObject setRank;
    public Text inputFieldText;
    public GameObject Text8;
    private bool isRankEnd;

    [System.Serializable]
    public class HighNoonTopRank
    {
        public RANK[] RANK;
    }

    [System.Serializable]
    public class RANK
    {
        public string NO;
        public string NAME;
        public string SCORE;
    }

    private void Start()
    {
        color = img.color;
        img.color = color;

        StartCoroutine(readyBtnOn());
    }

    public void endGame()
    {
        count++;
        if(count == 3)
        {
            StartCoroutine(bossBlack());
        }
        else
        {
            StartCoroutine(readyBtnOn());
        }
    }

    IEnumerator readyBtnOn()
    {
        yield return new WaitForSeconds(3f);

        gun1 = Instantiate(sausageGun);
        gun1.transform.position = sausageGun.transform.position;
        gun1.transform.rotation = sausageGun.transform.rotation;
        gun1.transform.localScale = sausageGun.transform.localScale;
        gun1.SetActive(true);

        gun2 = Instantiate(bossGun);
        gun2.transform.position = bossGun.transform.position;
        gun2.transform.rotation = bossGun.transform.rotation;
        gun2.transform.localScale = bossGun.transform.localScale;
        gun2.SetActive(true);

        readyBtn.SetActive(true);
    }

    public void readyGame()
    {
        readyBtn.SetActive(false);

        _imgBlack = imgBlack();
        StartCoroutine(_imgBlack);
        ranTime = Random.Range(3, 20);
        isGame = true;
        timer = 0;
    }

    private void Update()
    {
        if (isGame)
        {
            timer += Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                //실패 시
                isMistake = true;
                isGame = false;
                waitShot = false;

                shotSound.Play();
                color.a = 0;
                img.color = color;

                StopCoroutine(_imgBlack);
                _imgBlack = null;

                timer = 0;

                StartCoroutine(mistakeShot());
            }

            if(timer >= ranTime)
            {
                shotSound.Play();
                color.a = 0;
                img.color = color;

                StopCoroutine(_imgBlack);
                _imgBlack = null;

                timer = 0;
                isGame = false;

                waitShot = true;
            }

            if(timer > 10)
            {
                if (!mushroom.activeSelf)
                {
                    mushroom.SetActive(true);
                }
            }
        }

        if (waitShot)
        {
            timer2 += Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                score += timer2;

                switch (count)
                {
                    case 0:
                        {
                            score1.text = timer2.ToString()+"s";
                            break;
                        }
                    case 1:
                        {
                            score2.text = timer2.ToString() + "s";
                            break;
                        }
                    case 2:
                        {
                            score3.text = timer2.ToString() + "s";
                            scoreText.text = score.ToString() + "s";
                            break;
                        }
                }

                hp[count].SetActive(false);
                mushroom.SetActive(false);

                timer2 = 0;
                waitShot = false;

                gun1.GetComponent<HighNoonAmmo>().isStart = true;
                gun2.GetComponent<HighNoonAmmo>().isStart = true;
            }
        }
    }

    IEnumerator imgBlack()
    {
        color.a = 0;
        while (true)
        {
            color.a += 0.1f * Time.deltaTime;
            img.color = color;
            if (color.a >= 1)
            {
                break;
            }
            yield return null;
        }
    }

    IEnumerator bossBlack()
    {
        yield return new WaitForSeconds(1f);
        Boss.GetComponent<AudioSource>().Play();
        SpriteRenderer spr = Boss.GetComponent<SpriteRenderer>();
        Color c = spr.color;

        while (true)
        {
            c.a -= 0.5f * Time.deltaTime;
            spr.color = c;

            if (c.a <= 0)
            {
                break;
            }
            yield return null;
        }
        GameOverEvent();
    }

    IEnumerator mistakeShot()
    {
        gun1.GetComponent<AudioSource>().Play();
        while (true)
        {
            gun1.transform.position = Vector3.MoveTowards(gun1.transform.position, new Vector3(4, 4, 0), 5*Time.deltaTime);
            if(gun1.transform.position == new Vector3(4, 4, 0))
            {
                break;
            }
            yield return null;
        }
        GameOverEvent();
    }

    void GameOverEvent()
    {
        SoundManager.Instance.setSound(mainclip);
        GameOver.SetActive(true);

        StartCoroutine(viewRank());
        if (isMistake)
        {
            LastScore.text = "너무 빨리 쏨";
        }
        else
        {
            LastScore.text = "님 점수 : " + score.ToString() + "초";

            float highScore = DataManager.Instance.getHighNoonSausageScore();

            if (highScore == 0 || score < highScore)
            {
                RankingBtn.SetActive(true);
            }

            StartCoroutine(GetRank(highScore.ToString()));
        }
    }

    public void onClickSetRankingBtn()
    {
        setRank.SetActive(true);
    }
    public void onClickSetRankEnd()
    {
        if (isRankEnd)
        {
            return;
        }

        Debug.Log(inputFieldText.text.Length);
        if (inputFieldText.text.Length > 8)
        {
            Text8.SetActive(true);
            return;
        }

        if (!string.IsNullOrEmpty(inputFieldText.text))
        {
            DataManager.Instance.setHighNoonSausageScore(score);
            StartCoroutine(SetRank(inputFieldText.text, score.ToString()));
            isRankEnd = true;
        }
    }


    IEnumerator GetRank(string s)
    {
        float requsetTime = float.Parse(s) - 0.000001f;

        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_rank_srh3.jsp?score=" + requsetTime))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string[] tmp = request.downloadHandler.text.Split('>');
                string[] tmp2 = tmp[2].Split('<');
                Debug.Log(tmp2[0]);
                if (tmp2[0].Equals("null"))
                {
                    tmp2[0] = "꼴";
                }

                if(s == "0")
                {
                    LastScore.text += "\n" + "님 최고점수 : " + "없음" + "\n" + "님 랭킹 : 꼴등";
                }
                else
                {
                    LastScore.text += "\n" + "님 최고점수 : " + s + "초" + "\n" + "님 랭킹 : " + tmp2[0] + "등";
                }
            }
        }
    }

    public IEnumerator SetRank(string name, string score)
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_rank_insert3.jsp?name=" + name + "&score=" + score))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                /*
                string[] tmp = request.downloadHandler.text.Split('>');
                string[] tmp2 = tmp[2].Split('<');
                Debug.Log(tmp2[0]);
                */
                StartCoroutine(RankEnd());
            }
        }
    }

    IEnumerator RankEnd()
    {
        rankNotiText.SetActive(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Stage");
    }

    IEnumerator viewRank()
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_rank_select3.jsp?num=10"))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string[] tmp = request.downloadHandler.text.Split('>');
                string[] tmp2 = tmp[1].Split('<');
                Debug.Log(tmp2[0]);
                highNoonTopRank = JsonUtility.FromJson<HighNoonTopRank>(tmp2[0]);
                for (int i = 0; i < highNoonTopRank.RANK.Length; i++)
                {
                    TopRankText.text += highNoonTopRank.RANK[i].NO + "등 " + highNoonTopRank.RANK[i].NAME + " " + highNoonTopRank.RANK[i].SCORE + "초\n";
                }
            }
        }
    }
}
