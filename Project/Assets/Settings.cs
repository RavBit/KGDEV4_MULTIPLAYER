using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.NetworkLobby
{
    public class Settings : MonoBehaviour {
        [Header("UI Elements")]
        public Text NicknamePlaceholder;
        public InputField NicknameInput;


        void Awake()
        {
            NicknamePlaceholder.text = AppManager.instance.User.nickname;
        }

        public void UpdateUsername()
        {
            StartCoroutine("SetNickName");
        }
        //Corountine that goes through the Login process
        public IEnumerator SetNickName()
        {
            WWWForm nickname_form = new WWWForm();
            nickname_form.AddField("session_id", AppManager.instance.User.session);
            nickname_form.AddField("nickname", NicknameInput.text);
            WWW nicknamedata = new WWW("http://81.169.177.181/KGDEV4/update_nickname.php", nickname_form);
            yield return nicknamedata;

            if (string.IsNullOrEmpty(nicknamedata.error))
            {
                AppManager.instance.User.nickname = NicknameInput.text;
                NicknamePlaceholder.text = AppManager.instance.User.nickname;
                LobbyManager.s_Singleton.ChangeTo(LobbyManager.s_Singleton.mainMenuPanel);
            }
            else
            {
                Debug.LogError("ERROR FATAL");
            }
        }
    }
}
