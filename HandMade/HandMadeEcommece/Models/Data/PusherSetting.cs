using System;
using System.Collections.Generic;

namespace HandMadeEcommece.Models.Data;

public partial class PusherSetting
{
    public int Id { get; set; }

    public string PusherAppId { get; set; } = null!;

    public string PusherKey { get; set; } = null!;

    public string PusherSecret { get; set; } = null!;

    public string PusherCluster { get; set; } = null!;

    public TimeOnly? CreatedAt { get; set; }

    public TimeOnly? UpdatedAt { get; set; }
}
