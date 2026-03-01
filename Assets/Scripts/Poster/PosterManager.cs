using System.Collections.Generic;
using UnityEngine;


public class PosterManager : MonoBehaviour
{
    public List<ObjectPosterPart> parts;
    public GameObject monologue;
    public GameObject dialogue;
    private bool win;


    private void Start()
    {
        win = true;
    }

    public void OnEnable()
    {
        PosterSlot.CorrectDrop += CheckWin;
    }

    public void OnDisable()
    {
        PosterSlot.CorrectDrop -= CheckWin;
    }

    private void CheckWin()
    {
        foreach (ObjectPosterPart part in parts)
        {
            if (!part.GetPosterPart().IsPositionated)
            {
                win = false;
                return;
            }
        }
        win = true;

        if (win)
        {
            // Poner lo que haga falta
            monologue.SetActive(true);
            dialogue.SetActive(true);
        }
    }
}
