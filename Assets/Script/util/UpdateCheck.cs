using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateCheck : MonoBehaviour
{
    public GameObject versionCheck;

    private void Start()
    {
        versionCheck.SetActive(false);
        Debug.Log(Application.version);
        StartCoroutine(getVersion());
    }

    public IEnumerator getVersion()
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get("http://3fu1.iptime.org/MiSil/manggame_version_srh.jsp"))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                //Debug.Log(request.downloadHandler.text);
                string[] tmp = request.downloadHandler.text.Split('>');
                string[] tmp2 = tmp[2].Split('<');
                Debug.Log(tmp2[0]);

                if(Application.version != tmp2[0])
                {
                    StartCoroutine(versionActive());
                }
            }
        }
    }

    IEnumerator versionActive()
    {
        versionCheck.SetActive(true);
        yield return new WaitForSeconds(3f);
        versionCheck.SetActive(false);
    }
}
