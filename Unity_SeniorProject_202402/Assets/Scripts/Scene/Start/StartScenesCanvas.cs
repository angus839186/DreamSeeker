using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScenesCanvas : MonoBehaviour
{
    public GameObject SettingsCanvas;
    public GameObject loginCanvas;
    public GameObject caCanvas;
    public GameObject changePasswordCanvas;
    public GameObject howToPlayCanvas;

    public bool isLogin;

    public bool isFullScreen;
    public GameObject FullScreenCheck;

    private void Update()
    {

    }

    public void StartBtn()
    {
        if (isLogin) 
        {
            SceneManager.LoadScene("Scene00");
        }
    }

    public void ContinueBtn()
    {
        SceneManager.LoadScene("Scene00");
    }

    public void SettingsBtn()
    {
        if (SettingsCanvas.active != true)
        {
            SettingsCanvas.SetActive(true);
        }
        else
        {
            SettingsCanvas.SetActive(false);
        }
    }

    public void howToPlayBtn()
    {
        if (howToPlayCanvas.active != true)
        {
           howToPlayCanvas.SetActive(true);
        }
        else
        {
            howToPlayCanvas.SetActive(false);
        }
    }

    public void QuitBtn() { Application.Quit(); }

    public void LoginBtn() {loginCanvas.SetActive(true); }

    public void CABtn() { caCanvas.SetActive(true); }
    public void ChangePasswordBtn() { changePasswordCanvas.SetActive(true); }

    public void CancelLoginBtn() { loginCanvas.SetActive(false); }
    public void CancelCABtn() {caCanvas.SetActive(false); }
    public void CancelChangePasswordBtn() { changePasswordCanvas.SetActive(false); }

    public void FullScreen()
    {
        isFullScreen = !isFullScreen;
        FullScreenCheck.SetActive(isFullScreen);
        Screen.fullScreen = !Screen.fullScreen;
    }
}
