using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; // 1분당 비트 수
    double currentTime = 0f;

    [SerializeField] Transform tfNoteApper = null;
    [SerializeField] GameObject goNote = null;


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteApper.position, Quaternion.identity, transform);
            currentTime -= 60d / bpm; // --> 조금의 시간이 지나가기 때문에 이걸 0으로 초기화하면 안 됨
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            Destroy(collision.gameObject);
        }
    }
}
