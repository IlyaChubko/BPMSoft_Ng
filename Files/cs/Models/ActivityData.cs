using System;
using System.Runtime.Serialization;

namespace BPMSoft_NgExample.Models
{
    [DataContract]
    public class ActivityBaseData
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "startDate")]
        public string StartDate { get; set; }

        [DataMember(Name = "statusId")]
        public Guid StatusId { get; set; }
    }

    [DataContract]
    public class ActivityFullData : ActivityBaseData
    {
        [DataMember(Name = "endDate")]
        public string EndDate { get; set; }

        [DataMember(Name = "owner")]
        public string Owner { get; set; }

        [DataMember(Name = "category")]
        public string Category { get; set; }
    }

    [DataContract]
    public class StatusData
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "isFinal")]
        public bool IsFinal { get; set; }
    }
}