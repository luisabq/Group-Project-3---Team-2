using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadScene : MonoBehaviour
{
  void OnTriggerEnter(Collider coL){
    if(coL.CompareTag("Player")){
        SceneManager.LoadScene("Third Planet");
    }
  }
}