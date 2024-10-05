using System;
using UnityEngine;

internal class RetrievePlayerScoresEventArgs : EventArgs
{
    public GameObject SourceGameObject;

    public RetrievePlayerScoresEventArgs(GameObject sourceGameObject)
    {
        this.SourceGameObject = sourceGameObject;
    }
}