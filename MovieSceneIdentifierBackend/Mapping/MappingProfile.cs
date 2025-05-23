using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UploadedClip, UploadMovieRequest>();
        // CreateMap<CreateMovieDto, Movie>();
    }
}