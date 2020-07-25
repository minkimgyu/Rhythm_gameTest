using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] GameObject goStageUI = null;

    StageMenu theStageMenu;

    private void Start()
    {
        theStageMenu = FindObjectOfType<StageMenu>();
    }

    public void BtnPlay()
    {
        goStageUI.SetActive(true);
        this.gameObject.SetActive(false);
        theStageMenu.resetCurrentSong();
        theStageMenu.SettingSong();
    }
}
