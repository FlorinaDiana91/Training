using AutoMapper;

namespace Training.NetCore.Services.JobQueue.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Training.Task.Api.Models.Task, TaskResource>();
        }
    }
}
