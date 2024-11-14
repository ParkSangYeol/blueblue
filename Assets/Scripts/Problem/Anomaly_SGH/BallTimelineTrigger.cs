using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BallTimelineTrigger : SerializedMonoBehaviour
{
    [InfoBox("공 이사현상 트리거 변수")]
    public bool triggerBall = false;
    [InfoBox("애니메이션이 저장된 타임라인")]
    public PlayableDirector pd;
    [InfoBox("애니메이션되는 공")]
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
