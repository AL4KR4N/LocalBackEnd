using monchotradebackend.models.entities;

namespace monchotradebackend.models.dtos
{
    public class ExchangeDto
    {
        public int Id { get; set; }
        public int InitiatorUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public int InitiatorProductId { get; set; }
        public int ReceiverProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? RejectionReason { get; set; }
        public string? Notes { get; set; }
        
        // Additional properties for UI display
        public string InitiatorUserName { get; set; } = string.Empty;
        public string ReceiverUserName { get; set; } = string.Empty;
        public string InitiatorProductName { get; set; } = string.Empty;
        public string ReceiverProductName { get; set; } = string.Empty;
    }
}

public class ExchangeCreationDto{
    public int InitiatorUserId { get; set; }
    public int InitiatorProductId { get; set; }
    public int ReceiverProductId { get; set; }
    public string Notes {get; set;} = string.Empty;
}