using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fawaterk.Service
{
    public class FawaterkReturn
    {
        public FawaterkReuslt Result { get; set; }
        public string RedirectUrl { get; set; }
    }
    public enum FawaterkReuslt
    {
        Failed=0,
        Success=1,
        Error=2
    }
    public class PackageUserReservation
    {
        [Key, Column(Order = 0)]
        public long UserId { get; set; }
        [Key, Column(Order = 1)]
        public long PackageId { get; set; }
        [Key, Column(Order = 2)]
        public long FatoraId { get; set; }
       
    }
    public class UserHotelsWhishList
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string CityId { get; set; }
        public string HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelImage { get; set; }
        public string HotelAddress { get; set; }
        public int HotelRate { get; set; }
        public double Price { get; set; }
        public string ApiName { get; set; }
    }

    public class PopUserModel
    {
        [Key, Column(Order = 0)]
        public long PopHotelsId { get; set; }
        [Key, Column(Order = 1)]
        public long UserId { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumOfRooms { get; set; }
        public int NumOfAdults { get; set; }
        public int NumOfKids { get; set; }
        public string MPhone { get; set; }

    }
    public class PopHotelsModel
    {
        [Key]
        public long Id { get; set; }
        public string HotelName { get; set; }
        public string FolderImgsName { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string PopHotelsDescription { get; set; }
        public string MainIamgeName { get; set; }
        public string logoIamgeName { get; set; }
        public double AvgPrice { get; set; }
        public int Stars { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ImagesCount { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class UserRoomReservation
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string CityId { get; set; }
        public string HotelId { get; set; }
        public string HotelName { get; set; }
        public string Description { get; set; } //x
        public string RoomTypeCode { get; set; }
        public string ConfNum { get; set; }
        public string APIName { get; set; }
        public string AllocationDetails { get; set; }
        public double Price { get; set; }//x
        public DateTime CreationDate { get; set; }
        [DefaultValue(ReservationStatus.UnBooked)]
        public ReservationStatus Status { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        [DefaultValue(false)]
        public bool IsChecked { get; set; }//x
        public long FatoraId { get; set; }
    }



    public enum ReservationStatus
    {
        UnBooked = 0,
        Booked = 1,
        Failed=2
    }

    public class Package
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime OfferStartDate { get; set; }
        public DateTime OfferEndDate { get; set; }
        public DateTime PackageStartDate { get; set; }
        public DateTime PackageEndDate { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        [DefaultValue(false)]
        public bool IsArchived { get; set; }
        [DefaultValue(false)]
        public bool OutOfDated { get; set; }
        public double Price { get; set; }
        public int Units { get; set; }
        public string MainImageName { get; set; }
        public string FolderImgsName { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string PackageDescription { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ImagesCount { get; set; }

    }

    public class NewsModel
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string FolderImgsName { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string DescriptionFileName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }

    public class OurServiceModel
    {
        [Key]
        public long Id { get; set; }
        public string ServiceName { get; set; }
        public string ImgName { get; set; }
        public string Description { get; set; }

    }

    public class OurCustomerFavDestModel
    {
        [Key]
        public long Id { get; set; }
        public string DestinationName { get; set; }
        public string Description { get; set; }
        public string ImgName { get; set; }
        public int Reviews { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class PopularHotelsModel
    {
        [Key]
        public long Id { get; set; }
        public string CityId { get; set; }
        public string HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelImage { get; set; }
        public string HotelAddress { get; set; }
        public int HotelRate { get; set; }
        public double Price { get; set; }
        public string ApiName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class MailNewsModel
    {
        [Key]
        public long Id { get; set; }
        public string Email { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        [DefaultValue(true)]
        public bool isEnabled { get; set; }
    }

}