using AutoMapper;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Profiles
{
    public class CursoProfile : Profile
    {
        public CursoProfile()
        {
            CreateMap<Curso, CursoDto>();
            CreateMap<CursoDto, Curso>();
        }
    }
}
