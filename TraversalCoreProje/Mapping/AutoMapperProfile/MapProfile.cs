using AutoMapper;
using DTOLayer.DTOs.AnnouncementDTOs;
using DTOLayer.DTOs.AppUserDTOs;
using DTOLayer.DTOs.ContactDTOs;
using EntityLayer.Concrete;

namespace TraversalCoreProje.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        //AutoMapper; farklı sınıflardaki nesneleri eşlemek için kullanılan ve nesneden nesneye eşlemeyi sağlayan popüler bir kütüphanedir.
        public MapProfile()
        {
            //CreateMap<AnnouncementAddDTOs, Announcement>();
            //CreateMap<Announcement, AnnouncementAddDTOs>();
            //Yukarıdaki iki satır kod yerine tek satırda aşağıdaki gibi çözüm sağlayabiliriz. İkisi de aynı amaca hizmet etmektedir.
            CreateMap<AnnouncementAddDto, Announcement>().ReverseMap();
            CreateMap<AppUserRegisterDto, AppUser>().ReverseMap(); // DTO ismi , entity ismi
            CreateMap<AppUserLoginDto, AppUser>().ReverseMap();
            CreateMap<AnnouncementListDto, Announcement>().ReverseMap();  
            CreateMap<AnnouncementUpdateDto, Announcement>().ReverseMap();  
            CreateMap<SendMessageDto, ContactUs>().ReverseMap();  
        }
    }
}
