using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackUIScript : MonoBehaviour {

    [SerializeField] private Slider SliderOpinion;
    [SerializeField] private Slider SliderKnowledge;

    public void PassOpinionToBoard()
    {
        if (LeaderboardTracker.Exists)
        {
            LeaderboardTracker.Instance.CurrentPlayer.FeedbackOpinion = (int)SliderOpinion.value;
        }
    }

    public void PassKnowledgeToBoard()
    {
        if (LeaderboardTracker.Exists)
        {
            LeaderboardTracker.Instance.CurrentPlayer.FeedbackKnowledge = (int)SliderKnowledge.value;
        }
    }

    public void TryAddCurrentPlayer()
    {
        if (LeaderboardTracker.Exists)
        {
            LeaderboardTracker.Instance.TryAddCurrentPlayer();
        }
    }
}
