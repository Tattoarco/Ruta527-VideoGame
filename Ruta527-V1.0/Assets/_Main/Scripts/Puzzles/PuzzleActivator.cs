using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{
    public ColorPuzzleManager puzzleManager;

    private void OnMouseDown()
    {
        puzzleManager.ActivatePuzzle();
        gameObject.SetActive(false); // Oculta el activador si ya no se necesita
    }
}
