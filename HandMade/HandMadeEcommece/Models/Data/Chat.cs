using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class Chat
{
    public int Id { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string Message { get; set; } = null!;

    public int Seen { get; set; }

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }
}
