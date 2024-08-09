using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AbrirEmail : MonoBehaviour
{
    public string emailAddress = "example@example.com";
    private Button emailButton;

    void Start()
    {
        emailButton = gameObject.GetComponent<Button>();
        emailButton.onClick.AddListener(OpenGmailClient);
    }

    void OpenGmailClient()
    {
        // Encode the email address to handle special characters
        string encodedEmail = UnityWebRequest.EscapeURL(emailAddress);
        // Create the Gmail URL
        string gmailUrl = $"https://mail.google.com/mail/?view=cm&fs=1&to={encodedEmail}";
        Application.OpenURL(gmailUrl);
    }
}
