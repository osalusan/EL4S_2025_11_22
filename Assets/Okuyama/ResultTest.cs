using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultTest : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			ResultManager.Answer(true);
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			ResultManager.Answer(false);
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			SceneManager.LoadScene("ResultScene");
		}
	}
}
