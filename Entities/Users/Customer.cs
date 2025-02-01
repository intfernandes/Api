

namespace Api.Entities
{
public class Customer : IUser
{   
    public  List<PaymentMethod> PaymentMethods { get; set; } = []; 
    public  List<Review> Reviews { get; set; } = [];
    public  List<Rating> Ratings { get; set; } = [];
    public  List<Cart> Carts { get; set; } = [];
    public  List<Wishlist> Wishlists { get; set; } = [];
    public  List<Subscription> Subscriptions { get; set; } = [];
    public  List<Feedback> Feedbacks { get; set; } = [];
    public  List<Complaint> Complaints { get; set; } = [];  
    public  List<Refund> Refunds { get; set; } = [];
    public  List<Return> Returns { get; set; } = []; 
    public  List<Report> Reports { get; set; } = [];
    public  List<Chat> Chats { get; set; } = [];
    public  List<Conversation> Conversations { get; set; } = [];
    public  List<Message> Messages { get; set; } = [];
    public  List<Notification> Notifications { get; set; } = [];
}


    public class PaymentMethod : BaseEntity
    {
    public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }
   public class Complaint : BaseEntity
    {
    public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }   

    public class Subscription : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

    public class Wishlist : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

    public class Cart : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

    public class Rating : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

    public class Review : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }



    public class Feedback : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

        public class Refund : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

        public class Return : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

        public class Report : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

        public class Chat : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

        public class Conversation : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

        public class Message : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }

        public class Notification : BaseEntity
    {
            public Guid CustomerId { get; set; }
    public  Customer? Customer { get; set; } 
    }
    
}