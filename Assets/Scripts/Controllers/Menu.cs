using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Menu : MonoBehaviour {

    [SerializeField]
    List<GameObject> WindowsList;
    // Login
    public InputField L_login;
    public InputField L_password;
    // Register
    public InputField R_login;
    public InputField R_password;
    public InputField R_email;
    // User Account
    public Text U_login;
    public Text U_gold;
    public Text U_lvl;
    public Text U_rating;
    public Text U_energy;

    public Image U_progressBar;
    private bool loggedIn = false;

    public GameObject menuBar;

    public Image userImage;

    // Start

    private void Awake()
    {

        if (Data.userSession == null)
            ShowLoginWindow();
        else
        {
            loggedIn = true;
            menuBar.SetActive(true);
            ClientTCP.Send_RequestUserAccountDataUpdate(Data.userSession.login);
            WindowSetActive(3);
        }
    }

    // Base functions
    public void RegisterUser()
    {
        ClientTCP.Send_RequestUserRegistration(R_login.text, R_password.text, R_email.text);
        L_login.text = R_login.text;
        L_password.text = R_password.text;
    }

    public void LoginUser()
    {
        ClientTCP.Send_RequestUserLogin(L_login.text, L_password.text);
    }

    public void SwitchAudio()
    {

    }

    public void QuickGame(int id)
    {
        menuBar.SetActive(false);
        ClientTCP.Send_RequestEnterQuickPlay(Data.userSession, id);
        WindowSetActive(4);
    }

    public void OpenAvatarEditor()
    {
        menuBar.SetActive(false);
        WindowSetActive(5);
    }

    // Windows controller
    public void WindowSetActive(int value)
    {
        foreach (GameObject go in WindowsList)
        {
            go.SetActive(false);
        }
        WindowsList[value].SetActive(true);
        
    }

    public void ShowUserAccountWindow()
    {
        loggedIn = true;

        L_login.text = "";
        L_password.text = "";
        R_login.text = "";
        R_password.text = "";
        R_email.text = "";

        UpdateUserAccountWindow();
        WindowSetActive(3);
    }

    public void UpdateUserAccountWindow()
    {
        U_login.text = Data.userSession.login;
        U_gold.text =  Data.userSession.gold.ToString();
        U_energy.text = Data.userSession.energy.ToString();
        U_rating.text = Data.userSession.rating.ToString();
        int lvl = (Data.userSession.experience / 100);
        switch(lvl)
        {
            case 0:
                U_lvl.text = "Fish";
                break;
            case 1:
                U_lvl.text = "Newbie";
                break;
            case 2:
                U_lvl.text = "Bronze";
                break;
            case 3:
                U_lvl.text = "Silver";
                break;
            case 4:
                U_lvl.text = "Gold";
                break;
            case 5:
                U_lvl.text = "Pro";
                break;
            case 6:
                U_lvl.text = "Star";
                break;
            default:
                U_lvl.text = "Admin";
                break;
        }
        U_progressBar.GetComponent<RectTransform>().localScale = new Vector3(((float)(Data.userSession.experience % 100) / 100.0F), 0.6F);
    }

    public void ShowLoginWindow()
    {
        if (loggedIn)
        {
            loggedIn = false;
            menuBar.SetActive(false);
            ClientTCP.Send_UserLogout();
            Data.userSession = null;
            Data.userImageTexture = null;
            Data.userImageScale = 0;
            U_login.text = "Login";
        }
        WindowSetActive(1);
    }

    public void ShowSettingsWindow()
    {

    }

    public void ShowInfoWindow()
    {

    }

    // Events
    private void OnUserLogin(UserSession userSession)
    {
        Data.userSession = userSession;
        menuBar.SetActive(true);
        ShowUserAccountWindow();
    }

    private void OnUserSessionUpdate(UserSession userSession)
    {
        Data.userSession = userSession;
        Debug.Log("Data: " + JsonConvert.SerializeObject(Data.userSession));
        UpdateUserAccountWindow();
    }

    private void OnUserRegister()
    {
        LoginUser();
    }
    private void OnQuickGameInfo(QuickPlaySessionInfo quickPlaySessionInfo)
    {
        PlayerPrefs.SetString("QuickPlaySessionInfo", JsonConvert.SerializeObject(quickPlaySessionInfo));
        SceneManager.LoadScene(1);
    }

    private void OnImageUpdate(UserImageData userImageData)
    {
        byte[] b64_bytes = System.Convert.FromBase64String(userImageData.b64str);
        Texture2D texture = new Texture2D(userImageData.scale, userImageData.scale)
        {
		    filterMode = FilterMode.Point,
        };
        texture.LoadImage(b64_bytes);
        userImage.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

		Data.userImageTexture = userImage.sprite.texture;
        Data.userImageScale = userImageData.scale;
    }

    public void ShowMainWindow()
    {
        ClientTCP.Send_RequestUserAccountDataUpdate(Data.userSession.login);
        if (!menuBar.activeSelf)
        {
            menuBar.SetActive(true);
        }
        WindowSetActive(3);
    }

}
