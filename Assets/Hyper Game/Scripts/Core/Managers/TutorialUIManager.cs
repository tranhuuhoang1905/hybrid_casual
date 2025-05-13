using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUIManager : MonoBehaviour
{
    public static TutorialUIManager Instance;
    private Dictionary<TutorialType, List<string>> tutorialDialogs = new Dictionary<TutorialType, List<string>>();
    private TutorialType nowType;
    private int nowTutorial = 0;
    [SerializeField] private GameObject tutorialUI;
    private TextMeshProUGUI tutorialText;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        if (tutorialUI)
        {
            tutorialText = tutorialUI.transform.Find("TutorialChat/Text")?.GetComponent<TextMeshProUGUI>();
        }
        // Khởi tạo danh sách hội thoại
        tutorialDialogs[TutorialType.StartGame] = new List<string>
        {
            "Chào mừng, kẻ lữ hành lạc lối... đến với thế giới của những bóng ma và quái vật.",
            "Đây không phải là giấc mơ, mà là cơn ác mộng mà bạn phải đối mặt.",
            "Bạn chỉ có một cơ hội duy nhất để sống sót... hoặc bị nuốt chửng bởi bóng tối.",
            "Từng quyết định của bạn có thể là cơ hội cuối cùng. Hãy cẩn thận với mỗi bước đi."
        };

        tutorialDialogs[TutorialType.FinalWar] = new List<string>
        {
            "Đây là điểm cuối của hành trình... nơi mà chỉ có kẻ mạnh mới có thể tồn tại.",
            "Phía trước là thử thách cuối cùng, không còn đường quay đầu nữa.",
            "Những kẻ yếu đuối đã gục ngã. Chỉ những chiến binh thực thụ mới có thể tiến lên.",
            "Hãy chiến đấu như thể đây là trận chiến cuối cùng của cuộc đời bạn!",
            "Sống để trở về vinh quang... hoặc bị lãng quên trong bóng tối mãi mãi."
        };
    }

    private void OnEnable()
    {
        GameEvents.OnShowTutorialGame += StartTutorial;
    }

    private void OnDisable()
    {
        GameEvents.OnShowTutorialGame -= StartTutorial;
    }

    public void StartTutorial(TutorialType type)
    {
        nowTutorial = 0 ;
        nowType = type;
        RunTutorial();
        
    }
    private void RunTutorial()
    {
        if (tutorialDialogs.ContainsKey(nowType))
        {
            List<string> dialogList = tutorialDialogs[nowType];
            if (dialogList.Count > 0) // Kiểm tra danh sách có câu thoại không
            {
                if (nowTutorial>= dialogList.Count){
                    Skip();
                }else
                {
                    RunTutorial(dialogList[nowTutorial]);  

                } 
            }
            
        }
    }

    private void RunTutorial(string tutorial)
    {
        if (tutorialUI) 
        {
            
            tutorialUI.SetActive(true);
            if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = StartCoroutine(TypeText(tutorial));

            
        }
        Time.timeScale = 0f;
    }

     private IEnumerator TypeText(string text)
    {
        tutorialText.text = ""; // Xóa nội dung cũ
        foreach (char letter in text.ToCharArray())
        {
            tutorialText.text += letter; // Thêm từng ký tự vào văn bản
            yield return new WaitForSecondsRealtime(0.05f); // ⏳ Điều chỉnh tốc độ hiển thị ký tự
        }
    }

    public void Next()
    {
        nowTutorial ++;
        RunTutorial();
    }

    public void Skip()
    {
        if (tutorialUI) tutorialUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
