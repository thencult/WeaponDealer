using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public GameObject shopMenu;
    bool isShopOpened = false;

    public void SwitchShop() {
        if (!isShopOpened) {
            OpenShop();
        }
        else {
            CloseShop();
        }
    }
    public void OpenShop() {
        shopMenu.SetActive(true);
        isShopOpened = true;
    }
    public void CloseShop() {
        shopMenu.SetActive(false);
        isShopOpened = false;
    }
    public void RestartLevel() {
        SceneManager.LoadScene(1);
    }

}
