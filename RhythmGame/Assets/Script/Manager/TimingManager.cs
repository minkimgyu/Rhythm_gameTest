using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    int[] judgementRecord = new int[5];

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;

    EffectManager theEffect = null;
    ScoreManager theScore = null;
    ComboManager theComboManager = null;
    StageManager theStageManager = null;
    PlayerController thePlayerController = null;

    // Start is called before the first frame update
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScore = FindObjectOfType<ScoreManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theStageManager = FindObjectOfType<StageManager>();
        thePlayerController = FindObjectOfType<PlayerController>();

        timingBoxs = new Vector2[timingRect.Length];

        // 가장 작은 박스부터 큰 박스까지 계산함
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public bool CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for (int j = 0; j < timingBoxs.Length; j++)
            {
                if (timingBoxs[j].x <= t_notePosX && t_notePosX <= timingBoxs[j].y)
                {
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);

                    if (j < timingBoxs.Length - 1)
                        theEffect.NoteHitEffect();
                    theEffect.JudgementEffect(j);

                    if(CheckCanNextPlate())
                    {
                        theScore.IncreaseScore(j); // 점수 증가
                        theStageManager.ShowNextPlate(); // 판 생성
                        judgementRecord[j]++;
                    }
                    else
                    {
                        theEffect.JudgementEffect(5);
                    }
                    return true;
                }
            }
        }

        theComboManager.ResetCombo();
        theEffect.JudgementEffect(timingBoxs.Length);
        MissRecord();
        return false;
    }

    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }

    public void MissRecord()
    {
        judgementRecord[4]++;
    }

    bool CheckCanNextPlate()
    {
        if (Physics.Raycast(thePlayerController.destPos, Vector3.down, out RaycastHit t_hitInfo, 1.1f))
        {
            if (t_hitInfo.transform.CompareTag("BasicPlate"))
            {
                BasicPlate t_plate = t_hitInfo.transform.GetComponent<BasicPlate>();
                if (t_plate.flag)
                {
                    t_plate.flag = false;
                    return true;
                }
            }
        }

        return false;
    }
}
