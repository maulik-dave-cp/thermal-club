using System.IO;

namespace ThermalClub.Modules.Core.Notifications
{
    public class EmailAttachmentDto
    {
        public Stream FileContent { get; set; }
        public string FileName { get; set; }
    }
}
