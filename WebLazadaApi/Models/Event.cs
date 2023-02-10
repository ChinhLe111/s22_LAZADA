using System;
using System.Collections.Generic;

#nullable disable

namespace WebLazadaApi.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string StartDay { get; set; }
        public string EndDay { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
