using UnityEngine;
using UnityEngine.UI;  // For Image component
using TMPro;
using System.Collections;

public class TileHeatManager : MonoBehaviour
{
    // Tiles grouped by color
    public GameObject[] redTiles;
    public GameObject[] orangeTiles;
    public GameObject[] grayTiles;
    public GameObject[] cyanTiles;

    // UI element to indicate which tiles will drop
    public TMP_Text dropIndicatorText;
    public Image colorIndicatorImage;  // Add this to reference the color indicator image

    // Time delays
    public float indicationTime = 3f; // Time before tiles drop
    public float resetTime = 2f; // Time before tiles reset

    private GameObject[] currentDroppingTiles;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            // Step 1: Choose random tile group to drop
            currentDroppingTiles = ChooseRandomTileGroup();

            // Step 2: Update UI to show the dropping tiles
            UpdateDropIndicator();

            // Step 3: Wait for indication time
            yield return new WaitForSeconds(indicationTime);

            // Step 4: Drop the tiles
            DropTiles();

            // Step 5: Wait for reset time
            yield return new WaitForSeconds(resetTime);

            // Step 6: Reset the tiles
            ResetTiles();
        }
    }

    GameObject[] ChooseRandomTileGroup()
    {
        int randomGroup = Random.Range(0, 4); // 4 groups now

        if (randomGroup == 0)
            return redTiles;
        else if (randomGroup == 1)
            return orangeTiles;
        else if (randomGroup == 2)
            return grayTiles;
        else
            return cyanTiles;
    }

    void UpdateDropIndicator()
    {
        string color = currentDroppingTiles == redTiles ? "Red" :
                          currentDroppingTiles == orangeTiles ? "Orange" :
                          currentDroppingTiles == grayTiles ? "Gray" : "Cyan";

        dropIndicatorText.text = $"Tiles Dropping: {color}";

        // Update the image color based on the tile group
        if (currentDroppingTiles == redTiles)
            colorIndicatorImage.color = Color.red; // Red
        else if (currentDroppingTiles == orangeTiles)
            colorIndicatorImage.color = new Color(1f, 0.647f, 0f); // Orange (RGB)
        else if (currentDroppingTiles == grayTiles)
            colorIndicatorImage.color = Color.gray; // Gray
        else
            colorIndicatorImage.color = Color.cyan; // Cyan
    }

    void DropTiles()
    {
        foreach (GameObject tile in currentDroppingTiles)
        {
            // Disable the tile's collider and renderer
            if (tile.TryGetComponent<Collider>(out Collider col))
                col.enabled = false;
            if (tile.TryGetComponent<Renderer>(out Renderer rend))
                rend.enabled = false;
        }
    }

    void ResetTiles()
    {
        foreach (GameObject tile in currentDroppingTiles)
        {
            // Enable the tile's collider and renderer
            if (tile.TryGetComponent<Collider>(out Collider col))
                col.enabled = true;
            if (tile.TryGetComponent<Renderer>(out Renderer rend))
                rend.enabled = true;
        }

        // Clear UI indicator
        dropIndicatorText.text = "";
        colorIndicatorImage.color = Color.white; // Reset color to default
    }
}
