using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour
{
	const string privateCode = "k5jNQkjv_U6zyxqxOWYQ5wPGiaJ7GgcU-YyKLnIDsUNw";
	const string publicCode = "604e0e10778d3c2c689e7332";
	const string webURL = "http://dreamlo.com/lb/";

	public Highscore[] HighscoresArray;

	private void Awake()
	{
		WWW www = new WWW(webURL + privateCode + "/clear/");
	}

	public void AddNewHighscore(string username, int score)
	{
		StartCoroutine(UploadNewHighscore(username, score));
	}

// Ignores warning of outdated web code.
# pragma warning disable 0618
	IEnumerator UploadNewHighscore(string username, int score)
	{
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			Debug.Log("Upload Successfull!");
		}
		else
		{
			Debug.LogWarning("Error uploading: " + www.error);
		}
	}

	public void DownloadHighscores()
	{
		StartCoroutine(DownloadHighscoresFromDatabase());
	}

	IEnumerator DownloadHighscoresFromDatabase()
	{
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			FormatHighscores(www.text);
		}
		else
		{
			Debug.LogWarning("Error downloading: " + www.error);
		}
	}
# pragma warning restore 0618
	void FormatHighscores(string textStream)
	{
		string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		HighscoresArray = new Highscore[entries.Length];

		for (int i = 0; i < entries.Length; i++)
		{
			string[] entryInfo = entries[i].Split(new char[] { '|' });
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			HighscoresArray[i] = new Highscore(username, score);
			print(HighscoresArray[i].username + ": " + HighscoresArray[i].score);
		}
	}
}

public struct Highscore
{
	public string username;
	public int score;

	public Highscore(string _username, int _score)
	{
		username = _username;
		score = _score;
	}
}
