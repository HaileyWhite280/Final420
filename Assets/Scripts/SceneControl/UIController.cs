using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
	public void OnStartScene(string sceneName)
	{
		Game.Instance.onLoadScene(sceneName);
	}

	public void OnQuit()
	{
		Application.Quit();
	}
}
