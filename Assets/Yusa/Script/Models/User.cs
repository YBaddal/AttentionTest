[System.Serializable]
public class User : UserRequest
{
    public string name;
    public string surname;
    public string school;
    public int grade;
    public string branch;
    public int avatar;
}
[System.Serializable]
public class UserRequest
{
    public string userId;
}
