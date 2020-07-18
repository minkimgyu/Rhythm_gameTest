using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; // 1분당 비트 수
    double currentTime = 0f;
    int ran = 0;

    [SerializeField] Transform tfNoteApper = null;

    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager theComboManager = null;

    private void Start()
    {
        theTimingManager = GetComponent<TimingManager>();
        theEffectManager = FindObjectOfType<EffectManager>();
        theComboManager = FindObjectOfType<ComboManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        ran += 1;

        if (currentTime >= 60d / bpm && ran % 10 == 5)
        {
            GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
            t_note.transform.position = tfNoteApper.position;
            t_note.SetActive(true);
            theTimingManager.boxNoteList.Add(t_note);
            currentTime -= 60d / bpm; // --> 조금의 시간이 지나가기 때문에 이걸 0으로 초기화하면 안 됨
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                theEffectManager.JudgementEffect(4);
                theComboManager.ResetCombo();
            }

            theTimingManager.boxNoteList.Remove(collision.gameObject);

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
