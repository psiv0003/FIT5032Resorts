//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFolio_Take10.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Booking
    {
        public int Id { get; set; }
        public System.DateTime BookingDateTime { get; set; }
        public int RoomID { get; set; }
        public string GuestID { get; set; }

        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }


        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChildren { get; set; }
        public decimal TotalCharge { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Comment { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Room Room { get; set; }
    }
}
