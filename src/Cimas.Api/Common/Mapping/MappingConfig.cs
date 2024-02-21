using Cimas.Application.Features.Cinemas.Commands.UpdateCinema;
using Cimas.Contracts.Cinemas;
using Mapster;

namespace Cimas.Api.Common.Mapping
{
    public class MappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(Guid UserId, Guid CinemaId, UpdateCinemaRequest requset), UpdateCinemaCommand>()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.CinemaId, src => src.CinemaId)
                .Map(dest => dest, src => src.requset);
        }
    }
}
