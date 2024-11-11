// Exchange.cs
using System.ComponentModel.DataAnnotations;

namespace monchotradebackend.models.entities;

public class Exchange
{
    public int Id { get; set; }

    [Required]
    public int InitiatorUserId { get; set; }

    [Required]
    public int ReceiverUserId { get; set; }

    [Required]
    public int InitiatorProductId { get; set; }

    [Required]
    public int ReceiverProductId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    [Required]
    public ExchangeStatus Status { get; set; } = ExchangeStatus.Pending;

    public string? RejectionReason { get; set; }
    public string? Notes { get; set; }

    // Propiedades de navegación
    public virtual User InitiatorUser { get; set; } = null!;
    public virtual User ReceiverUser { get; set; } = null!;
    public virtual Product InitiatorProduct { get; set; } = null!;
    public virtual Product ReceiverProduct { get; set; } = null!;
}
public enum ExchangeStatus
{
    Pending,
    Accepted,
    Rejected,
    Completed,
    Cancelled
}