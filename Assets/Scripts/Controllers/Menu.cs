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
    public Text H_login;
    // Register
    public InputField R_login;
    public InputField R_password;
    public InputField R_email;
    // User Account
    public Text U_login;
    public Text U_gold;
    public Text U_lvl;
    public float U_progress;
    public Text U_rating;
    public Text U_energy;

    public Text U_char_1;
    public Text U_char_2;
    public Text U_char_3;

    public Image U_progressBar;

    private bool loggedIn = false;

    public Image icon;

    // Start

    private void Awake()
    {

        if (Data.userSession == null)
            ShowLoginWindow();
        else
        {
            H_login.text = "LOGOUT";
            loggedIn = true;
            ClientTCP.Send_RequestUserAccountDataUpdate(Data.userSession.login);
            WindowSetActive(3);
        }


    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (L_login.isFocused)
            {
                L_password.Select();
            }
            else if (L_password.isFocused)
            {
                LoginUser();
            }
            //for debug
            else if (loggedIn)
            {
                QuickGame();
            }
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

    public void QuickGame()
    {
        ClientTCP.Send_RequestEnterQuickPlay(Data.userSession);
        WindowSetActive(4);
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
        H_login.text = "LOGOUT";
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
        U_gold.text = "Gold: " + Data.userSession.gold.ToString();
        U_energy.text = "Energy: " + Data.userSession.energy.ToString();
        U_rating.text = "Rating: " + Data.userSession.rating.ToString();
        U_char_1.text = Data.userSession.mainTeam[0].name + "(" + Data.userSession.mainTeam[0].power + ", lvl:" + Data.userSession.mainTeam[0].lvl + ")";
        U_char_2.text = Data.userSession.mainTeam[1].name + "(" + Data.userSession.mainTeam[1].power + ", lvl:" + Data.userSession.mainTeam[1].lvl + ")";
        U_char_3.text = Data.userSession.mainTeam[2].name + "(" + Data.userSession.mainTeam[2].power + ", lvl:" + Data.userSession.mainTeam[2].lvl + ")";

        U_lvl.text = (Data.userSession.exp / 100).ToString();
        U_progress = (float)((Data.userSession.exp % 100) * 1.7);
        U_progressBar.GetComponent<RectTransform>().sizeDelta = new Vector2(U_progress, 20);
    }


    public void ShowLoginWindow()
    {
        if (loggedIn)
        {
            ClientTCP.Send_UserLogout();
            Data.userSession = null;
            U_login.text = "Login";
            H_login.text = "LOGIN";
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


    // Update User Session
   
    /*
    public void AddGold()
    {
        userSessionObject.gold += 20;
        userSessionObject.exp += 150;
        UpdateLocalUserSession();
        ClientTCP.Send_RequestUserAccountDataUpdate(userSessionObject);
        UpdateUserAccountWindow();
    }

    public void ApplyFaction(int faction)
    {
        userSessionObject.faction = faction;
        UpdateLocalUserSession();
        ClientTCP.Send_RequestUserAccountDataUpdate(userSessionObject);
        UpdateUserAccountWindow();
    }
    */

    public void SetImage(string base64Pic)
    {
        byte[] b64_bytes = System.Convert.FromBase64String(base64Pic);
        Texture2D tex = new Texture2D(1, 1);
        tex.LoadImage(b64_bytes);
        icon.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

}
