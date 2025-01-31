

namespace Api.Entities
{
public class Customer : IUser
{
    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = []; 
    public virtual ICollection<Review> Reviews { get; set; } = [];
    public virtual ICollection<Rating> Ratings { get; set; } = [];
    public virtual ICollection<Cart> Carts { get; set; } = [];
    public virtual ICollection<Wishlist> Wishlists { get; set; } = [];
    public virtual ICollection<Subscription> Subscriptions { get; set; } = [];
    public virtual ICollection<Feedback> Feedbacks { get; set; } = [];
    public virtual ICollection<Complaint> Complaints { get; set; } = [];  
    public virtual ICollection<Refund> Refunds { get; set; } = [];
    public virtual ICollection<Return> Returns { get; set; } = []; 
    public virtual ICollection<Report> Reports { get; set; } = [];
    public virtual ICollection<Chat> Chats { get; set; } = [];
    public virtual ICollection<Conversation> Conversations { get; set; } = [];
    public virtual ICollection<Message> Messages { get; set; } = [];
    public virtual ICollection<Notification> Notifications { get; set; } = [];
    public virtual ICollection<Notification> SentNotifications { get; set; } = [];
    public virtual ICollection<Notification> ReceivedNotifications { get; set; } = [];
    public virtual ICollection<Notification> ReadNotifications { get; set; } = [];
    public virtual ICollection<Notification> UnreadNotifications { get; set; } = [];
    public virtual ICollection<Notification> DeletedNotifications { get; set; } = [];
}

   public class Complaint : BaseEntity
    {
    public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }   

    public class Subscription : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

    public class Wishlist : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

    public class Cart : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

    public class Rating : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

    public class Review : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

    public class PaymentMethod : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

    public class Feedback : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

        public class Refund : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

        public class Return : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

        public class Report : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

        public class Chat : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

        public class Conversation : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

        public class Message : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }

        public class Notification : BaseEntity
    {
            public int? CustomerId { get; set; }
    public virtual Customer? Customer { get; set; } 
    }
    
}