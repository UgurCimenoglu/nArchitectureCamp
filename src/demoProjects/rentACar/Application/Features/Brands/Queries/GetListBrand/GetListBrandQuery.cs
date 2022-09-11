using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetListBrand
{
    public class GetListBrandQuery : IRequest<BrandListModel>
    {
        public PageRequest PageRequest { get; set; }
        public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, BrandListModel>
        {
            IBrandRepository _brandRepository;
            IMapper _Mapper;

            public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _Mapper = mapper;
            }

            public async Task<BrandListModel> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Brand> brands = await _brandRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
                BrandListModel mappedBrandListModel = _Mapper.Map<BrandListModel>(brands);
                return mappedBrandListModel;
            }
        }
    }
}
