using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using AutoMapper;
using Core.Entities;
namespace Application.Mappings
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImage));


            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name))
                .ForMember(dest => dest.PublisherProfileImageUrl, opt => opt.MapFrom(src => src.Publisher.ProfileImage))
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ReverseMap();
            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.PublisherImage,
                    opt => opt.MapFrom(src => src.User != null ? src.User.ProfileImage : string.Empty))
                .ForMember(dest=>dest.CommentID, opt=>opt.MapFrom(src=>src.Id))
                .ReverseMap()  
               .ForMember(dest => dest.User, opt => opt.Ignore());





            CreateMap<Like, LikeDto>();
        }
    }
}
