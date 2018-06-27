using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackUIScript : MonoBehaviour {

	public void PassOpinionToBoard(int pFeedback = 1)
    {
        if (LeaderboardTracker.Exists)
        {
            LeaderboardTracker.Instance.CurrentPlayer.FeedbackOpinion = pFeedback;
        }
    }

    public void PassKnowledgeToBoard(int pFeedback = 1)
    {
        if (LeaderboardTracker.Exists)
        {
            LeaderboardTracker.Instance.CurrentPlayer.FeedbackKnowledge = pFeedback;
        }
    }
}
