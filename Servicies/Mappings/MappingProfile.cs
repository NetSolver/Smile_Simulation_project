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
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count));


            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.UserId ?? 0))
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : "Anonymous"))
                .ForMember(dest => dest.PublisherImage, opt => opt.MapFrom(src => src.User != null ? src.User.ProfileImage : string.Empty));


            CreateMap<Like, LikeDto>();
        }
    }
}
