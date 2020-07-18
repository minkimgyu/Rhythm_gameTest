using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    string hit = "Hit";

    [SerializeField] Animator JudgementAnimator = null;
    [SerializeField] Image JudgementImage = null;
    [SerializeField] Sprite[] JudgementSprite = null;

    public void JudgementEffect(int p_num)
    {
        JudgementImage.sprite = JudgementSprite[p_num];
        JudgementAnimator.SetTrigger(hit);
    }

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(hit);
    }
}
