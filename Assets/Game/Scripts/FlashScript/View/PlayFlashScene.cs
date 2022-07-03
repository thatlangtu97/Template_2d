using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayFlashScene : View
{
    public static PlayFlashScene _instance;
    [Inject] public PopupManager popupManager { get; set; }
    public string nameScene;
    public Button StartGameBtn;
    public Animator animator;
    public GameObject parent;
    public static PlayFlashScene instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject prefab = Resources.Load<GameObject>("UI/LoadingFlashScene");
                PlayFlashScene flashScene = Instantiate(prefab).GetComponent<PlayFlashScene>();
                _instance = flashScene;
                DontDestroyOnLoad(flashScene);
            }
            return _instance;
        }
    }
    protected override void Start()
    {
        base.Start();
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        StartGameBtn.onClick.AddListener(StartGameClick);
    }
    
    public void StartGameClick()
    {
        
        StartCoroutine(DelayLoadScene());
    }
    IEnumerator DelayLoadScene()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync(nameScene);
    }
    public void Loading(string namescene, float timeDelay,System.Action action=null)
    {
        StartCoroutine(DelayLoadSceneWithLoading(namescene, timeDelay, action));
    }
    IEnumerator DelayLoadSceneWithLoading(string namescene , float timeDelay, System.Action action = null)
    {
        ShowLoading();
        try
        {
            if (action != null) action.Invoke();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }
        yield return new WaitForSeconds(timeDelay);        
        SceneManager.LoadScene(namescene);
    }
    public void HideLoading()
    {
        animator.SetTrigger("HideLogo");
    }
    public void ShowLoading()
    {
        animator.SetTrigger("ShowLoading");
    }
}
