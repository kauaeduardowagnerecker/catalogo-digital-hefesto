using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FecharMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;

    void DesativarMenu()
    {
        menu.SetActive(false);
    }
    void AnimaçãoFechamento()
    {
        Animator animadorMenu = menu.GetComponent<Animator>();
        animadorMenu.Play("FecharMenu");
        Invoke("DesativarMenu", 1);
    }
}
