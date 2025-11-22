public static class ResultManager
{
	private static int _questionNum = 0;
	private static int _correctAnswerNum = 0;

	public static int QuestionNum => _questionNum;
	public static int CorrectAnswerNum => _correctAnswerNum;

	public static void Answer(bool correct)
	{
		_questionNum++;

		if (correct)
		{
			_correctAnswerNum++;
		}
	}

	public static void Reset()
	{
		_questionNum = 0;
		_correctAnswerNum = 0;
	}
}
