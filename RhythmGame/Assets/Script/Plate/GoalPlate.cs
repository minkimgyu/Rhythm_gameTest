using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    AudioSource theAudio;
    NoteManager theNoteManager;

    Result theResult;

    // Start is called before the first frame update
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        theNoteManager = FindObjectOfType<NoteManager>();
        theResult = FindObjectOfType<Result>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            theAudio.Play();
            PlayerController.s_canPresskey = false;
            theNoteManager.RemoveNote();
            theResult.ShowResult();
        }
    }
}
