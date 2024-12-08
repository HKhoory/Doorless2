using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; // Make sure to include TMPro if you're using TextMeshPro

public class TileHeatManager : MonoBehaviour
{
    // Tiles grouped by color
    public GameObject[] redTiles;
    public GameObject[] orangeTiles;
    public GameObject[] grayTiles;
    public GameObject[] cyanTiles;

    // UI elements
    public TMP_Text dropIndicatorText;
    public Image colorIndicatorImage;
    public TMP_Text countDownText; // Countdown text
    public Image countDownImage; // Countdown Image
    public TMP_Text roundText; // Round text

    // Time delays
    public float indicationTime = 3f;
    public float resetTime = 2f;

    private GameObject[] currentDroppingTiles;
    private int round = 1;

    private void Start()
    {
        StartCoroutine(GameStartCountdown());

        dropIndicatorText.gameObject.SetActive(false);
        colorIndicatorImage.gameObject.SetActive(false);
        roundText.gameObject.SetActive(false);
    }

    IEnumerator GameStartCountdown()
    {
        int countdown = 5;
        while (countdown > 0)
        {
            countDownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countDownText.text = "Go!";
        yield return new WaitForSeconds(1f);

        // Hide the countdown text after it's finished
        countDownText.gameObject.SetActive(false);
        countDownImage.gameObject.SetActive(false);

        dropIndicatorText.gameObject.SetActive(true);
        colorIndicatorImage.gameObject.SetActive(true);
        roundText.gameObject.SetActive(true);

        // Start the game loop after countdown
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

            // Update round number
            round++;
            roundText.text = $"Round: {round}";

            // Make the game harder by decreasing the time it takes for tiles to drop
            indicationTime = Mathf.Max(1f, indicationTime - 0.2f); // Decrease time, but keep it above 1 second
        }
    }

    GameObject[] ChooseRandomTileGroup()
    {
        int randomGroup = Random.Range(0, 4);

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

        if (currentDroppingTiles == redTiles)
            colorIndicatorImage.color = Color.red;
        else if (currentDroppingTiles == orangeTiles)
            colorIndicatorImage.color = new Color(1f, 0.647f, 0f); // Orange RGB
        else if (currentDroppingTiles == grayTiles)
            colorIndicatorImage.color = Color.gray;
        else
            colorIndicatorImage.color = Color.cyan;
    }

    void DropTiles()
    {
        foreach (GameObject tile in currentDroppingTiles)
        {
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
            if (tile.TryGetComponent<Collider>(out Collider col))
                col.enabled = true;
            if (tile.TryGetComponent<Renderer>(out Renderer rend))
                rend.enabled = true;
        }

        dropIndicatorText.text = "";
        colorIndicatorImage.color = Color.white;
    }
}
