using System;

namespace ThermalClub.Modules.Core.Data
{
    public interface ITrackable
    {
        DateTime CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}