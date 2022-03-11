using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : Singleton<Game>
{
	enum State
	{
		TITLE,
		PLAYER_START,
		GAME,
		PLAYER_DEAD,
		GAME_OVER,
		GAME_WON
	}

	[SerializeField] ScreenFade screenFade;
	[SerializeField] SceneLoader sceneLoader;
	public GameData gameData;

	State state = State.TITLE;
	int highScore;

	private void Start()
	{
		highScore = PlayerPrefs.GetInt("highscore", 0);
		highScore++;
		PlayerPrefs.SetInt("highscore", highScore);

		InitScene();

		SceneManager.activeSceneChanged += OnSceneWasLoaded;
	}

	void InitScene()
	{
		SceneDescriptor sceneDescriptor = FindObjectOfType<SceneDescriptor>();
		if (sceneDescriptor != null)
		{
			Instantiate(sceneDescriptor.player, sceneDescriptor.playerSpawn.position, sceneDescriptor.playerSpawn.rotation);
		}
	}

	private void Update()
	{
		switch (state)
		{
			case State.TITLE:
				break;
			case State.PLAYER_START:
				break;
			case State.GAME:
				break;
			case State.PLAYER_DEAD:
				break;
			case State.GAME_OVER:
				break;
			case State.GAME_WON:
				break;
			default:
				break;
		}
	}

	public void onLoadScene(string sceneName)
	{
		sceneLoader.Load(sceneName);
	}

	public void OnPlayerDead()
	{
		gameData.intData["Lives"]--;

		if (gameData.intData["Lives"] == 0)
		{
			onLoadScene("Main Menu");
			//Mak so it goes to game end screen then reset button
			//goes to Main Menu
		}
		else
		{
			onLoadScene(SceneManager.GetActiveScene().name);

		}

	}

	public void OnEnemyDead()
    {
		gameData.intData["EnemyLives"]--;

		if(gameData.intData["EnemyLives"] == 0)
        {
			onLoadScene("Game Won!");
        }
    }

	public void OnSceneWasLoaded(Scene current, Scene next)
	{
		InitScene();
	}
}
