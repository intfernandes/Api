
using Api.Labs;

namespace Api.Entities
{
    public class Customer : IUser
    {
        public List<PaymentMethod> PaymentMethods { get; set; } = [];
        public List<Review> Reviews { get; set; } = [];
        public List<Rating> Ratings { get; set; } = [];
        public List<Cart> Carts { get; set; } = [];
        public List<Wishlist> Wishlists { get; set; } = [];
        public List<Subscription> Subscriptions { get; set; } = [];
        public List<Feedback> Feedbacks { get; set; } = [];
        public List<Complaint> Complaints { get; set; } = [];
        public List<Refund> Refunds { get; set; } = [];
        public List<Report> Reports { get; set; } = [];
        public List<Chat> Chats { get; set; } = [];
        public List<Conversation> Conversations { get; set; } = [];
        public List<Message> Messages { get; set; } = [];
        public List<Notification> Notifications { get; set; } = [];
    }

}