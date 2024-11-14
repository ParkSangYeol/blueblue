using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BallTimelineTrigger : SerializedMonoBehaviour
{
    [InfoBox("�� �̻����� Ʈ���� ����")]
    public bool triggerBall = false;
    [InfoBox("�ִϸ��̼��� ����� Ÿ�Ӷ���")]
    public PlayableDirector pd;
    [InfoBox("�ִϸ��̼ǵǴ� ��")]
    public GameObject ball;

    private Rigidbody rb;

    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
        rb = ball.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (triggerBall)
        {
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            triggerBall = false;
            pd.Play();
            if(rb !=null)
                StartCoroutine(WaitEndTimeline());
        }
    }
    IEnumerator WaitEndTimeline()
    {
        yield return new WaitForSeconds((float)pd.duration);
        if(rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
