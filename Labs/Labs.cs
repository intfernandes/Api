
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Entities;

namespace Api.Labs
{

        //     public Guid? CustomerId { get; set; }
        // public virtual Customer? Customer { get; set; }
        // public Guid? EmployeeId { get; set; }
        // public virtual Employee? Employee { get; set; }

        public class PaymentMethod : BaseEntity
    {

        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [Required]
        [CreditCard] // Basic format validation for credit card number
        [MaxLength(20)] // Max length for typical card numbers
        public string CardNumber { get; set; } = null!;  // Consider encryption at rest & in transit

        [Required]
        [MaxLength(100)]
        public string CardHolderName { get; set; } = null!;

        [Required]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Expiration date must be in MM/YY format")] // More robust MM/YY format validation
        public string ExpirationDate { get; set; } = null!;  // Format: MM/YY

        [Required]
        [MaxLength(4)] // CVV is usually 3 or 4 digits
        // **Security:**  CVV should **never** be stored persistently if possible.  Consider processing and discarding immediately after transaction. If you must store, use strong encryption.
        public string CVV { get; set; } = null!;

        public PaymentType Type { get; set; }  // Enum for Payment Type

        public bool IsDefault { get; set; }  // Flag for default payment method
    }

    public enum PaymentType
    {
        CreditCard,
        DebitCard,
        PayPal,
        BankTransfer,
        Crypto
    }

        public class Complaint : BaseEntity
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        public Guid? OrderId { get; set; }  // Optional - Complaint related to an order
        [ForeignKey(nameof(OrderId))]
        public virtual Order? Order { get; set; }  // Navigation property

        [Required]
        [MaxLength(200)] // Limit length of Subject
        public string Subject { get; set; } = null!;  // Short summary of the complaint

        [Required]
        public string Description { get; set; } = null!;  // Detailed complaint description

        [MaxLength(50)] // Limit length of Status
        public ComplaintStatus Status { get; set; } = ComplaintStatus.Pending ;  // Consider using an Enum for Status! (e.g., ComplaintStatus enum)

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;  // When the complaint was submitted
        public DateTime? ResolvedAt { get; set; }  // When the complaint was resolved

        public Guid? EmployeeId { get; set; }  // Admin/Support resolving the complaint
        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee? Employee { get; set; }  // Navigation property - Assuming Admin/Support are also Users

        // Consider adding:
        // - ComplaintPriority (Enum: High, Medium, Low)
        // - Category (Enum: Product, Service, Delivery, etc.)
        // - LastActionTakenAt (DateTime)
    }

    // Consider creating a ComplaintStatus Enum:
    public enum ComplaintStatus
    {
        Pending,
        UnderReview,
        Resolved,
        Rejected,
        Escalated
    }



        public class Subscription : BaseEntity
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [Required]
        [MaxLength(100)] // Limit length of SubscriptionType
        public string SubscriptionType { get; set; } = null!;  // Type of subscription (e.g., Basic, Premium)

        [Required]
        public DateTime StartDate { get; set; }  // Start date of the subscription

        public DateTime? EndDate { get; set; }  // Optional: End date of the subscription, if applicable

        public bool IsActive { get; set; }  // Status to check if subscription is active

        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Explicit decimal precision for Price - important for currency
        [Range(0, double.MaxValue)] // Ensure Price is not negative
        public decimal Price { get; set; }  // Price of the subscription

        [MaxLength(100)] // Limit length of PaymentMethod
        public string? PaymentMethod { get; set; }  // Payment method used for the subscription (nullable)

        [MaxLength(50)] // Limit length of BillingCycle
        public string? BillingCycle { get; set; }  // Billing cycle (nullable)

        // Constructor to initialize required properties (if any) - Constructors are less important for EF Core entities, but good practice.
        public Subscription() { /* EF Core needs a parameterless constructor */ }

        public Subscription(Guid customerId, string subscriptionType, DateTime startDate, decimal price)
        {
            CustomerId = customerId;
            SubscriptionType = subscriptionType ?? throw new ArgumentNullException(nameof(subscriptionType));
            StartDate = startDate;
            Price = price;
            IsActive = true;
        }
    }



  public class Wishlist : BaseEntity
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>(); // Navigation to WishlistItems (One-to-Many)

        // Optional: Date the wishlist was created - Already in BaseEntity (CreatedAt)

        // Optional: Date the wishlist was last updated - Already in BaseEntity (UpdatedAt)

        // Parameterless constructor for EF Core
        public Wishlist() { }

        // Constructor to initialize the wishlist - Adjusted customerId to Guid
        public Wishlist(Guid customerId)
        {
            CustomerId = customerId;
            Items = new List<WishlistItem>();  // Initialize the items list
            // CreatedAt and UpdatedAt are handled by BaseEntity or database defaults.
        }
    }


        public class WishlistItem : BaseEntity
    {
        [Required]
        public Guid ProductId { get; set; } // Foreign key to Product (assuming Product entity exists)
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!; // Navigation to Product

        [Required]
        [MaxLength(200)] // Limit length of Name
        public string Name { get; set; } = null!; // Name/Description of the item (consider fetching from Product if possible)

        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Decimal precision for Price
        [Range(0, double.MaxValue)] // Ensure price is not negative
        public decimal Price { get; set; } // Price of the item (consider fetching from Product)

        public DateTime AddedAt { get; set; } = DateTime.UtcNow; // Optional: Date added

        [MaxLength(200)] // Limit URL length
        public string? ImageUrl { get; set; } // Image URL (consider fetching from Product or Photo entity)

        // Parameterless constructor for EF Core
        public WishlistItem() { }

        // Constructor to initialize a wishlist item - Adjusted ProductId to Guid
        public WishlistItem(Guid productId, string name, decimal price)
        {
            ProductId = productId; // Changed to Guid
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Price = price;
            AddedAt = DateTime.UtcNow;
        }
    }


        public class Cart : BaseEntity
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<CartItem> Items { get; set; } = new List<CartItem>(); // Navigation to CartItems (One-to-Many)

        [Column(TypeName = "decimal(18, 2)")] // Decimal precision for TotalPrice
        public decimal TotalPrice { get; set; } // Calculated property, consider making it non-mapped and calculate on-demand

        public bool IsActive { get; set; } = true; // Default to active

        // Parameterless constructor for EF Core
        public Cart() { }

        // Constructor to initialize the cart - Adjusted CustomerId to Guid
        public Cart(Guid customerId)
        {
            CustomerId = customerId; // Changed to Guid
            Items = new List<CartItem>();  // Initialize the items list
            TotalPrice = 0; // Initialize TotalPrice
            IsActive = true;  // By default, a cart is active
            // CreatedAt and UpdatedAt handled by BaseEntity or database defaults
        }

        // Method to recalculate the total price of the cart - Consider making this a method in a CartService instead of in the entity itself for better separation of concerns.
        public void RecalculateTotalPrice()
        {
            TotalPrice = Items.Sum(item => item.Quantity * item.Price);
        }
    }

    public class CartItem : BaseEntity
    {
        [Required]
        public Guid ProductId { get; set; } // Foreign key to Product
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!; // Navigation to Product

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")] // Ensure Quantity is positive
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Decimal precision for Price
        [Range(0, double.MaxValue)] // Ensure price is not negative
        public decimal Price { get; set; } // Price of the item at the time it was added (consider fetching from Product Price initially and updating if price changes)

        [MaxLength(200)] // Limit length of Name
        public string? Name { get; set; } // Name/Description of the item (consider fetching from Product)

        [MaxLength(200)] // Limit URL length
        public string? ImageUrl { get; set; } // Image URL (consider fetching from Product or Photo entity)


        // Parameterless constructor for EF Core
        public CartItem() { }

        // Constructor to initialize a cart item - Adjusted ProductId to Guid
        public CartItem(Guid productId, int quantity, decimal price, string name)
        {
            ProductId = productId; // Changed to Guid
            Quantity = quantity;
            Price = price;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }


        public class Rating : BaseEntity
    {
        [Required]
        public Guid ProductId { get; set; } // Foreign key to Product
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!; // Navigation to Product

        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")] // Example: 1-5 star rating
        public int RatingScore { get; set; }

        [MaxLength(500)] // Limit length of Comment
        public string? Comment { get; set; } // Optional: Comment

        public DateTime RatingDate { get; set; } = DateTime.UtcNow; // Date rating was given

        public bool IsVerified { get; set; } = false; // Default to unverified
        // Consider: Source of verification (e.g., OrderId if verified by purchase)
    }

    public class Review : BaseEntity
    {
        [Required]
        public Guid ProductId { get; set; } // Foreign key to Product
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; } = null!; // Navigation to Product

        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [Required]
        public string ReviewText { get; set; } = null!; // Text of the review

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")] // Example: 1-5 star rating, consistent with Rating entity
        public int Rating { get; set; } // Rating score associated with the review (redundant with Rating entity if they are always linked?)

        public DateTime ReviewDate { get; set; } = DateTime.UtcNow; // Date review was written

        public bool IsVerified { get; set; } = false; // Default to unverified

        [MaxLength(200)] // Limit URL length
        public string? MediaUrl { get; set; } // Optional: Media URL

        [MaxLength(200)] // Limit length of ReviewTitle
        public string? ReviewTitle { get; set; } // Optional: Review title

        public string? Response { get; set; } // Optional: Response from seller/support

        // Parameterless constructor for EF Core
        public Review() { }

        // Constructor to initialize a review - Adjusted ProductId, CustomerId to Guid
        public Review(Guid productId, Guid customerId, string reviewText, int rating, DateTime reviewDate, bool isVerified, string? mediaUrl = null, string? reviewTitle = null, string? response = null)
        {
            ProductId = productId; // Changed to Guid
            CustomerId = customerId;     // Changed to Guid
            ReviewText = reviewText ?? throw new ArgumentNullException(nameof(reviewText));
            Rating = rating;
            ReviewDate = reviewDate;
            IsVerified = isVerified;
            MediaUrl = mediaUrl;
            ReviewTitle = reviewTitle;
            Response = response;
        }
    }



       public class Feedback : BaseEntity
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [Required]
        public string FeedbackText { get; set; } = null!; // Content of feedback

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")] // Optional rating, if used
        public int? Rating { get; set; } // Optional: Rating score

        [Required]
        [MaxLength(50)] // Limit length of FeedbackType
        public string FeedbackType { get; set; } = null!; // Type of feedback

        public DateTime FeedbackDate { get; set; } = DateTime.UtcNow; // Date submitted

        public bool IsAddressed { get; set; } = false; // Default to not addressed

        public string? Response { get; set; } // Optional: Response from support/team

        // Parameterless constructor for EF Core
        public Feedback() { }

        // Constructor to initialize feedback - Adjusted customerId to Guid
        public Feedback(Guid customerId, string feedbackText, string feedbackType, DateTime feedbackDate, int? rating = null, bool isAddressed = false, string? response = null)
        {
            CustomerId = customerId; // Changed to Guid
            FeedbackText = feedbackText ?? throw new ArgumentNullException(nameof(feedbackText));
            FeedbackType = feedbackType ?? throw new ArgumentNullException(nameof(feedbackType));
            FeedbackDate = feedbackDate;
            Rating = rating;
            IsAddressed = isAddressed;
            Response = response;
        }
    }



       public class Refund : BaseEntity
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [Required]
        public Guid OrderId { get; set; } // Foreign key to Order
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; } = null!; // Navigation to Order

        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Decimal precision for RefundAmount
        [Range(0.01, double.MaxValue, ErrorMessage = "Refund amount must be greater than 0")] // Ensure RefundAmount is positive
        public decimal RefundAmount { get; set; }

        [Required]
        [MaxLength(200)] // Limit length of RefundReason
        public string RefundReason { get; set; } = null!; // Reason for refund

        [Required]
        [MaxLength(50)] // Limit length of RefundStatus
        public string RefundStatus { get; set; } = "Pending"; // Consider Enum for Status (e.g., RefundStatus enum)

        public DateTime RequestDate { get; set; } = DateTime.UtcNow; // Date refund requested
        public DateTime? ProcessedDate { get; set; } // Optional: Date processed

        public string? Response { get; set; } // Optional: Response from team

        // Parameterless constructor for EF Core
        public Refund() { }

        // Constructor to initialize refund - Adjusted customerId, OrderId to Guid
        public Refund(Guid customerId, Guid orderId, decimal refundAmount, string refundReason, string refundStatus, DateTime requestDate, DateTime? processedDate = null, string? response = null)
        {
            CustomerId = customerId; // Changed to Guid
            OrderId = orderId; // Changed to Guid
            RefundAmount = refundAmount;
            RefundReason = refundReason ?? throw new ArgumentNullException(nameof(refundReason));
            RefundStatus = refundStatus ?? throw new ArgumentNullException(nameof(refundStatus));
            RequestDate = requestDate;
            ProcessedDate = processedDate;
            Response = response;
        }
    }

    // Consider creating a RefundStatus Enum:
    // public enum RefundStatus
    // {
    //     Pending,
    //     Approved,
    //     Denied,
    //     Processed,
    //     Failed
    // }



       public class Report : BaseEntity
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [Required]
        [MaxLength(50)] // Limit length of ReportType
        public string ReportType { get; set; } = null!; // Type of report

        [Required]
        public string ReportDescription { get; set; } = null!; // Description of the report

        [Required]
        public Guid ReportedItemId { get; set; } // ID of the item being reported - Consider making this more specific type based on what can be reported (e.g., ProductId, ReviewId, etc. and use inheritance or separate FKs)
        // You might NOT need a navigation property to ReportedItem if it can be of various types. If it's always a Product, for example:
        // public virtual Product ReportedProduct { get; set; } = null!;

        [Required]
        [MaxLength(50)] // Limit length of ReportStatus
        public string ReportStatus { get; set; } = "Pending"; // Consider Enum for Status (e.g., ReportStatus enum)

        public DateTime ReportDate { get; set; } = DateTime.UtcNow; // Date created
        public DateTime? ResolvedDate { get; set; } // Optional: Date resolved

        public string? Response { get; set; } // Optional: Response from team

        public bool IsUrgent { get; set; } = false; // Default to not urgent

        // Parameterless constructor for EF Core
        public Report() { }

        // Constructor to initialize report - Adjusted customerId, ReportedItemId to Guid
        public Report(Guid customerId, string reportType, string reportDescription, Guid reportedItemId, string reportStatus, DateTime reportDate, bool isUrgent, DateTime? resolvedDate = null, string? response = null)
        {
            CustomerId = customerId; // Changed to Guid
            ReportType = reportType ?? throw new ArgumentNullException(nameof(reportType));
            ReportDescription = reportDescription ?? throw new ArgumentNullException(nameof(reportDescription));
            ReportedItemId = reportedItemId; // Changed to Guid
            ReportStatus = reportStatus ?? throw new ArgumentNullException(nameof(reportStatus));
            ReportDate = reportDate;
            IsUrgent = isUrgent;
            ResolvedDate = resolvedDate;
            Response = response;
        }
    }

    // Consider creating a ReportStatus Enum:
    // public enum ReportStatus
    // {
    //     Pending,
    //     UnderReview,
    //     Resolved,
    //     Rejected,
    //     Duplicate,
    //     Invalid
    // }


    public class Chat : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = "Unnamed Chat"; // Or default name if applicable

        public ChatType ChatType { get; set; } = ChatType.Group; // Default to Group chat

        // Navigation Properties
        public virtual ICollection<UserChat> UserChats { get; set; } = new List<UserChat>(); // Many-to-Many with User (via join entity)
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();      // One-to-Many with Message

        // Parameterless constructor for EF Core
        public Chat() { }
    }

       public enum ChatType
    {
        Group,   // Multi-user chat
        Direct    // One-to-one chat (can be different from Conversation if you want to distinguish)
                 // You can add more types if needed, e.g., Channel, Broadcast, etc.
    }

    public class UserChat : BaseEntity
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [Required]
        public Guid ChatId { get; set; }
        [ForeignKey(nameof(ChatId))]
        public virtual Chat? Chat { get; set; } // Navigation property to Chat

        // Optionally, you could add properties specific to the user's participation in the chat, e.g.,
        // - LastReadMessageTimestamp
        // - IsAdmin
        // - MutedUntil (DateTime?)
        // - etc.

        // Composite Key Configuration - to be configured in DbContext (Fluent API)
        // Example in DbContext.OnModelCreating:
        //   modelBuilder.Entity<UserChat>()
        //       .HasKey(uc => new { uc.UserId, uc.ChatId });
    }


      public class Conversation : BaseEntity
    {
        // Conversations are often Direct (1-to-1) - you can enforce this or allow group conversations too.
        public ConversationType ConversationType { get; set; } = ConversationType.Direct; // Default to Direct conversation
        // Navigation Properties
        public virtual ICollection<UserConversation> UserConversations { get; set; } = new List<UserConversation>(); // Many-to-Many with User (via join entity)
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();            // One-to-Many with Message

        // Parameterless constructor for EF Core
        public Conversation() { }}

         public enum ConversationType
    {
        Direct, // 1-to-1 Conversation
        Group  // Group Conversation (if Conversations can be groups too)
    }

     public class UserConversation : BaseEntity
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [Required]
        public Guid ConversationId { get; set; }
        [ForeignKey(nameof(ConversationId))]
        public virtual Conversation? Conversation { get; set; } // Navigation property to Conversation

        // Optionally, properties specific to user in conversation can be added here, e.g.,
        // - LastReadMessageTimestamp
        // - IsAdmin
        // - etc.

        // Composite Key Configuration - to be configured in DbContext (Fluent API)
        // Example in DbContext.OnModelCreating:
        //   modelBuilder.Entity<UserConversation>()
        //       .HasKey(uc => new { uc.UserId, uc.ConversationId });
    }


 public class Message : BaseEntity
    {
        [Required]
        public string Content { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        // Foreign Key to Chat (or Conversation, depending on your design)
        public Guid? ChatId { get; set; } // Nullable if messages can be outside a Chat context
        [ForeignKey(nameof(ChatId))]
        public virtual Chat? Chat { get; set; }  // Navigation property to Chat

        public Guid? ConversationId { get; set; } // Nullable if messages can be outside Conversation context
        [ForeignKey(nameof(ConversationId))]
        public virtual Conversation? Conversation { get; set; } // Navigation property to Conversation

        // Parameterless constructor for EF Core
        public Message() { }
    }


    public class Notification : BaseEntity
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        [Required]
        [MaxLength(50)] // Limit length of NotificationType
        public string NotificationType { get; set; } = null!; // Type of notification

        [Required]
        public string NotificationMessage { get; set; } = null!; // Message content

        public DateTime NotificationDate { get; set; } = DateTime.UtcNow; // Date created

        public bool IsRead { get; set; } = false; // Default to unread

        public Guid? RelatedEntityId { get; set; } // Consider making this more specific type and separate FKs like in Report entity, if notification always relates to a specific entity type
        // If RelatedEntityId is intended for diverse types, you might keep it Guid? and handle type context in code.

        [MaxLength(200)] // Limit URL length
        public string? ActionUrl { get; set; } // Optional: Action URL

        // Parameterless constructor for EF Core
        public Notification() { }

        // Constructor to initialize notification - Adjusted customerId, RelatedEntityId to Guid?
        public Notification(Guid customerId, string notificationType, string notificationMessage, DateTime notificationDate, bool isRead = false, Guid? relatedEntityId = null, string? actionUrl = null)
        {
            CustomerId = customerId; // Changed to Guid
            NotificationType = notificationType ?? throw new ArgumentNullException(nameof(notificationType));
            NotificationMessage = notificationMessage ?? throw new ArgumentNullException(nameof(notificationMessage));
            NotificationDate = notificationDate;
            IsRead = isRead;
            RelatedEntityId = relatedEntityId; // Changed to Guid?
            ActionUrl = actionUrl;
        }
    }


}

    

