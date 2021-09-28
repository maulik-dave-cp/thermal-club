using Microsoft.EntityFrameworkCore;

namespace ThermalClub.Modules.Core.Data
{
    public interface ISeedConfiguration
    {
        void Map(ModelBuilder builder);
    }
}