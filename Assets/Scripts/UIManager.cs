using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    private Transform player;
    private Image innerBar;
    private float health;
    private GameObject canvas;
    private GameObject LoadingPanel;
    private RectTransform LoadingRec;
    
    Quaternion originalRoation;
    // Start is called before the first frame update
    void Start()
    {
        LoadingPanel = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        LoadingRec = LoadingPanel.GetComponent<RectTransform>();
        LoadingRec.sizeDelta = new Vector2(Screen.width, Screen.height); 
    }

   
    // Update is called once per frame
    void LateUpdate()
    {
        
        if (player != null)
        {
            health = 1 -  Mathf.Clamp(Mathf.Abs(player.position.x), 0.0f, 5.0f)/5.0f;
            innerBar.fillAmount = health;
            if(health > 0.5f && innerBar.color == Color.red)
            {
                innerBar.color = new Color32(0, 255, 0, 255);
            }
            if (health < 0.5f && innerBar.color == new Color32(0, 255, 0, 255))
            {
                innerBar.color = Color.red;
            }
            canvas = innerBar.gameObject.transform.parent.gameObject;
            canvas.transform.rotation = Camera.main.transform.rotation;
        }
        
        

    }
    public void ShowLoadingScreen()
    {
        LoadingRec.anchoredPosition = new Vector2(0.0f, 0.0f);
    }
    public void LoadFirstLevel()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync(1);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

 

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            
            Button quitButton = GameObject.FindGameObjectWithTag("QuitButton").GetComponent<Button>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            innerBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<Image>();
            canvas = innerBar.gameObject.transform.parent.gameObject;
            quitButton.onClick.AddListener(QuitGame);
            
        }
    }
}
