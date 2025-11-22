using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class Result : MonoBehaviour
{
	[Serializable]
	public struct Stamp
	{
		public Sprite texture;
		public float accuracyRate;
	}

	[Header("Text")]
	[SerializeField] Text _questionText = null;
	[SerializeField] Text _correctText = null;

	[Header("Stamp")]
	[SerializeField] GameObject _stamp;
	[SerializeField] Image[] _stampSprite = new Image[1];
	[SerializeField] Stamp[] _accuracyStamp = new Stamp[1];
	[SerializeField, Min(0.1f)] float _stampSpeed = 10f;
	[SerializeField, Min(1.1f)] float _startSize = 10f;


	[Header("SceneChange")]
	[SerializeField] string _changeSceneName = "TitleScene";
	[SerializeField] KeyCode _changeSceneKey = KeyCode.Space;
	[SerializeField, Min(0.1f)] float _waitInputTime = 2.0f;

	IEnumerator Start()
	{
		int questionNum = ResultManager.QuestionNum;
		int correctNum = ResultManager.CorrectAnswerNum;
		if (_questionText)
		{
			_questionText.text =questionNum.ToString();
		}
		if (_correctText)
		{
			_correctText.text = correctNum.ToString();
		}
		StampSet(questionNum, correctNum);

		//入力可能まで少し時間を空ける
		yield return new WaitForSeconds(_waitInputTime);

		while (!Input.GetKeyDown(_changeSceneKey))
		{
			yield return null;
		}

		//問題数/正答数リセット
		ResultManager.Reset();

		//シーン遷移
		if (!string.IsNullOrEmpty(_changeSceneName))
		{
			SceneManager.LoadScene(_changeSceneName);
		}
	}
	private void Update()
	{
		StampView();
	}

	void StampView()
	{
		if (_stamp.transform.localScale == Vector3.one) { return; }

		Vector3 size = _stamp.transform.localScale;
		float s = size.x;
		s -= Time.deltaTime * _stampSpeed;
		if (s < 0.98f)
		{
			_stamp.transform.localScale = Vector3.one;
		}
		else
		{
			_stamp.transform.localScale = new Vector3(s, s, 1f);
		}
	}

	void StampSet(float questionNum, float correctNum)
	{
		_stamp.transform.localScale = new Vector3(_startSize, _startSize, _startSize);

		if (questionNum == 0)
		{
			for (int i = 0; i < _stampSprite.Length; i++)
			{
				_stampSprite[i].sprite = _accuracyStamp[_accuracyStamp.Length - 1].texture;
			}
			return;
		}

		float accuracy = (float)correctNum / questionNum;
		for (int i = 0; i < _accuracyStamp.Length; i++)
		{
			if (_accuracyStamp[i].accuracyRate <= accuracy)
			{
				for (int j = 0; j < _stampSprite.Length; j++)
				{

					_stampSprite[j].sprite = _accuracyStamp[i].texture;
				}
				return;
			}
		}
	}
}

