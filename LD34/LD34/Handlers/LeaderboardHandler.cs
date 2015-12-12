using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace LD34.Handlers
{
    public class LeaderboardHandler
    {

        private const string PublicCode = "566c14826e51b611f8eed478";
        private const string PrivateCode = "GSdXecqkSUac1zbYNHg6pAAPsMifet80qAHN6kJ4taQg";
        private const string WebURL = "http://dreamlo.com/lb/";

        public delegate void HighscoreRequestFinish(Highscore[] scores);
        private delegate void DelegatePutScore(string username, int score);
        private delegate void DelegateHighscoreRequest(HighscoreRequestFinish callback);

        private Semaphore waitRequest;
        private HttpWebRequest webRequest;

        public LeaderboardHandler()
        {
            waitRequest = new Semaphore(0, 1);
        }

        public void PutScoreAsync(string username, int score)
        {
            DelegatePutScore action = PutScore;
            action.BeginInvoke(username, score, ar => action.EndInvoke(ar), null);
        }

        public void PutScore(string username, int score)
        {
            waitRequest.WaitOne();
            webRequest = (HttpWebRequest)WebRequest.Create(WebURL + PrivateCode + "/add/" + Uri.EscapeDataString(username) + "/" + score);
            HttpWebResponse response = (HttpWebResponse) webRequest.GetResponse();
            if(response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(response.StatusDescription);
            }
            waitRequest.Release();
        }

        public void HighscoreRequestAsync(HighscoreRequestFinish callback)
        {
            DelegateHighscoreRequest action = HighscoreRequest;
            action.BeginInvoke(callback, ar => action.EndInvoke(ar), null);
        }

        public void HighscoreRequest(HighscoreRequestFinish callback)
        {
            waitRequest.WaitOne();
            webRequest = (HttpWebRequest)WebRequest.Create(WebURL + PublicCode + "/pipe/");
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(response.StatusDescription);
            }

            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

            string[] entries = readStream.ReadToEnd().Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            Highscore[] highscoresList = new Highscore[entries.Length];

            response.Close();
            readStream.Close();
            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];
                int score = int.Parse(entryInfo[1]);
                highscoresList[i] = new Highscore(username, score);
            }
            callback(highscoresList);
            waitRequest.Release();
        }
    }

    public struct Highscore
    {
        public string Username;
        public int Score;

        public Highscore(string username, int score)
        {
            Username = username;
            Score = score;
        }
    }
}
