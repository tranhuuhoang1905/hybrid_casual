using UnityEngine.SceneManagement;
using UnityEngine;

public class BattleSelectController : MonoBehaviour
{
    [SerializeField] private AudioClip BattleSelectScenebackgroundMusic; // Nhạc nền mặc định
    public SceneSignal sceneSignal;
    private int index = 1;
    public GameObject buttonList1;
    public GameObject buttonList2;

    private GameObject buttonList1_Select;
    private GameObject buttonList1_Unselect;
    private GameObject buttonList2_Select;
    private GameObject buttonList2_Unselect;



    void Start()
    {
        if (BattleSelectScenebackgroundMusic != null)
        {
            AudioManager.Instance.PlayMusic(BattleSelectScenebackgroundMusic);
        }
        // Tìm các thành phần bên trong buttonList1
        buttonList1_Select = buttonList1.transform.Find("Image_Select").gameObject;
        buttonList1_Unselect = buttonList1.transform.Find("Image_Unselect").gameObject;

        // Tìm các thành phần bên trong buttonList2
        buttonList2_Select = buttonList2.transform.Find("Image_Select").gameObject;
        buttonList2_Unselect = buttonList2.transform.Find("Image_Unselect").gameObject;
        ChangerSelected(index);
    }
    public void OnBattleSelectButtonPressed(int level)
    {
        Debug.Log("check selsect");
        index = level;
        GameManager.Instance.SetPlayerType(index);
        ChangerSelected(index);
    }
    public void OnBattleGotoButtonPressed()
    {
        Debug.Log("check OnBattleGotoButtonPressed selsect");
        sceneSignal.LoadScene("BattleScene");
    }
    
    public void OnExitButtonPressed()
    {
        sceneSignal.LoadScene("StartScene");        // 🔥 Chuyển sang màn hình Loading trước khi vào game
        
    }
    private void ChangerSelected(int index)
    {
        switch (index)
        {
            case 1:
                // Hiển thị UI của buttonList1 khi chọn
                buttonList1_Select.SetActive(true);
                buttonList1_Unselect.SetActive(false);

                // Hiển thị UI của buttonList2 khi không chọn
                buttonList2_Select.SetActive(false);
                buttonList2_Unselect.SetActive(true);
                break;
            case 2:
                // Hiển thị UI của buttonList1 khi chọn
                buttonList1_Select.SetActive(false);
                buttonList1_Unselect.SetActive(true);

                // Hiển thị UI của buttonList2 khi không chọn
                buttonList2_Select.SetActive(true);
                buttonList2_Unselect.SetActive(false);
                break;
            
            default:
                break;
        }
    }
}
