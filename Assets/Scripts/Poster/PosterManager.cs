using System.Collections.Generic;
using UnityEngine;


public class PosterManager : MonoBehaviour
{
    public List<ObjectPosterPart> parts;
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
                break;
            }

            if (win)
            {
                // Poner lo que haga falta
            }

        }
    }
}
