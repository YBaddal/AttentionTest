using System.Collections.Generic;

[System.Serializable]
public class PostFeedbackModel 
{
    public string userId;
    public List<Game> games;
    public bool feedback;
    public string feedbackNote;
}
