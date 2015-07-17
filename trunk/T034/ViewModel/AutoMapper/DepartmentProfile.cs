using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Db.Entity;
using Db.Entity.Directory;
using Db.Entity.Vgiik;

namespace T034.ViewModel.AutoMapper
{
    public class DepartmentProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Department, DepartmentViewModel>()
                .ForMember(x => x.Albums, t => t.Ignore())
                .ForMember(dest => dest.Nodes, opt => opt.MapFrom(src => src.Nodes == null || !src.Nodes.Any() ? "" : src.Nodes.Select(n => n.Id.ToString()).Aggregate((i, j) => i.ToString() + "," + j.ToString())))
                .ForMember(dest => dest.Videos, opt => opt.MapFrom(src => src.Nodes.Where(n => n.NodeType == NodeType.Video).Select(n => n.Path)));

            Mapper.CreateMap<DepartmentViewModel, Department>()
                .ForMember(dest => dest.Nodes, opt => opt.MapFrom(src => new List<Node>(src.Nodes.Split(new string[] { "," }, StringSplitOptions.None).Select(n => new Node { Id = Convert.ToInt32(n) }))));
        }
    }
}