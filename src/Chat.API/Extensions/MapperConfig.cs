using AutoMapper;
using Chat.API.Data.Entities;
using DTOs;

namespace Chat.API.Extensions
{
    public class MapperConfig: Profile
    {
        public static Mapper InitializeAutomapper()
            {
                //Provide all the Mapping Configuration
                var config = new MapperConfiguration(cfg =>
                {
                    //Configuring 
                    cfg.CreateMap<User, LoginDTO>();

                    //Any Other Mapping Configuration ....
                });
                //Create an Instance of Mapper and return that Instance
                var mapper = new Mapper(config);
                return mapper;
            }
        
    }
}
