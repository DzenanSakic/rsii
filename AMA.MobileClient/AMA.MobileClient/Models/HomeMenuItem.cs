namespace AMA.MobileClient.Models
{
    public enum MenuItemType
    {
        RecommendedQuestions,
        Questions,
        Users,
        EditProfile,
        Logout,
        Login,
        Messages,
        Payments
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
