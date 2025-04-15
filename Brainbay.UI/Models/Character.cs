using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brainbay.UI.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public OriginLocation Origin { get; set; } = new();
        public OriginLocation Location { get; set; } = new();
        public string Image { get; set; } = string.Empty;
        public List<string> Episode { get; set; } = new();
        public string Url { get; set; } = string.Empty;
        public DateTime Created { get; set; }
    }

    public class OriginLocation
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
