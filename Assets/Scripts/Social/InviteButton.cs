using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class InviteButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Invite);
    }

    private void Invite()
    {
#if VK_SDK
        VKSDK.instance.ShowInvite();
#endif
    }
}