using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RestartBtnAction : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GameObject btnObj = GameObject.Find ("Canvas/Button");
		//获取按钮脚本组件
		Button btn = (Button) btnObj.GetComponent<Button>();
		//添加点击侦听
		btn.onClick.AddListener (onClick);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void onClick ()
	{
		Debug.Log ("click!");
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}
