namespace AppointmentManager
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Xml.Serialization;
    [Serializable]
    public partial class AspNetUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetRoles = new HashSet<AspNetRole>();
        }

        [JsonIgnore]
        public string Id { get; set; }

        [EditableProperty]
        [StringLength(256)]
        public string Email { get; set; }

        [EditableProperty]
        public string Nickname { get; set; }

        [EditableProperty]
        public string Address { get; set; }

        [EditableProperty]
        public string Details { get; set; }

        [EditableProperty]
        public string CNP { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public bool isDeleted { get; set; }

        #region Other Properties

        public bool EmailConfirmed { get; set; }

        public int AccessFailedCount { get; set; }

        [JsonIgnore]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public string SecurityStamp { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        [Display(Name = "Phone number")]
        [EditableProperty]
        public string PhoneNumber { get; set; }

        #endregion

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }

        [JsonIgnore]
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
