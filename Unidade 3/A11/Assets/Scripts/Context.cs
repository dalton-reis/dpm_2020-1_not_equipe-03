public static class Context
{
    private static string userId;

    public static string UserId 
    {
        get 
        {
            return userId;
        }
        set 
        {
            userId = value;
        }
    }
}
