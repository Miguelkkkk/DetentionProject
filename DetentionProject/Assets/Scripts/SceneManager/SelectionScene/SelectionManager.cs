using UnityEngine;

public class SelectionsManager : MonoBehaviour
{
    public RectTransform imageRight;
    public RectTransform imageLeft;
    public CanvasGroup rightImageCanvasGroup;
    public CanvasGroup leftImageCanvasGroup;
    public float moveDistance = 200f;
    public float transitionSpeed = 5f; 
    public float fadeSpeed = 2f; 

    private Vector3 rightImageStartPos;
    private Vector3 leftImageStartPos;
    private Vector2 targetPositionRight;
    private Vector2 targetPositionLeft;

    void Start()
    {
        rightImageStartPos = imageRight.anchoredPosition;
        leftImageStartPos = imageLeft.anchoredPosition;

        targetPositionRight = rightImageStartPos;
        targetPositionLeft = leftImageStartPos;

        imageRight.anchoredPosition = Vector2.Lerp(imageRight.anchoredPosition, targetPositionRight, Time.deltaTime * transitionSpeed);
        imageLeft.anchoredPosition = Vector2.Lerp(imageLeft.anchoredPosition, targetPositionLeft, Time.deltaTime * transitionSpeed);
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x > Screen.width / 2 + 100)
        {
            targetPositionRight = new Vector2(leftImageStartPos.x - moveDistance, leftImageStartPos.y);
            targetPositionLeft = new Vector2(rightImageStartPos.x, rightImageStartPos.y);

            rightImageCanvasGroup.alpha = Mathf.Lerp(rightImageCanvasGroup.alpha, 0.2f, Time.deltaTime * fadeSpeed);
            leftImageCanvasGroup.alpha = Mathf.Lerp(leftImageCanvasGroup.alpha, 1f, Time.deltaTime * fadeSpeed);

            if (Input.GetMouseButtonDown(0))
            {
                PlayerData player = new PlayerData("Sofia");
                SaveManager.SavePlayerData(player);
                Loader.Load(Loader.Scene.Tutorial);
            }
        }
        else if (mousePos.x < Screen.width / 2 - 100)
        {
            targetPositionLeft = new Vector2(rightImageStartPos.x + moveDistance, rightImageStartPos.y);
            targetPositionRight = new Vector2(leftImageStartPos.x, leftImageStartPos.y);

            rightImageCanvasGroup.alpha = Mathf.Lerp(rightImageCanvasGroup.alpha, 1f, Time.deltaTime * fadeSpeed);
            leftImageCanvasGroup.alpha = Mathf.Lerp(leftImageCanvasGroup.alpha, 0.2f, Time.deltaTime * fadeSpeed);

            if (Input.GetMouseButtonDown(0))
            {
                PlayerData player = new PlayerData("Andre");
                SaveManager.SavePlayerData(player);
                Loader.Load(Loader.Scene.Tutorial);
            }
        }
        else
        {
            targetPositionLeft = new Vector2(rightImageStartPos.x + moveDistance, rightImageStartPos.y);
            targetPositionRight = new Vector2(leftImageStartPos.x - moveDistance, leftImageStartPos.y);

            rightImageCanvasGroup.alpha = Mathf.Lerp(rightImageCanvasGroup.alpha, 0.2f, Time.deltaTime * fadeSpeed);
            leftImageCanvasGroup.alpha = Mathf.Lerp(leftImageCanvasGroup.alpha, 0.2f, Time.deltaTime * fadeSpeed);
        }

        imageRight.anchoredPosition = Vector2.Lerp(imageRight.anchoredPosition, targetPositionRight, Time.deltaTime * transitionSpeed);
        imageLeft.anchoredPosition = Vector2.Lerp(imageLeft.anchoredPosition, targetPositionLeft, Time.deltaTime * transitionSpeed);

    }
}
