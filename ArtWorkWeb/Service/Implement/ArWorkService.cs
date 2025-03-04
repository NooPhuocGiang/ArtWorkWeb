﻿using ArtWorkWeb.Service.Interfaces;
using AutoMapper;
using BussinessTier.Constants;
using BussinessTier.Payload;
using BussinessTier.Payload.ArtWork;
using DataTier.Models;
using DataTier.Repository.Implement;
using DataTier.Repository.Interface;

namespace ArtWorkWeb.Service.Implement
{
    public class ArWorkService: BaseService<ArWorkService>, IArtWorkService
    {
        public ArWorkService(IUnitOfWork<projectSWDContext> unitOfWork, ILogger<ArWorkService> logger, IMapper mapper,
           IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<int> CreateNewArtwork(CreateArtWorkRequest request)
        {

            Artwork newArtWork = _mapper.Map<Artwork>(request);

            await _unitOfWork.GetRepository<Artwork>().InsertAsync(newArtWork);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            return newArtWork.IdArtwork;
        }

        public async Task<GetArtWorkResponse> GetArtWrokById(int artWorkId)
        {
            Artwork artwork = await _unitOfWork.GetRepository<Artwork>().SingleOrDefaultAsync(
                predicate: x => x.IdArtwork.Equals(artWorkId))
                ?? throw new BadHttpRequestException(MessageConstant.ArtWork.ArtWorkNotFoundMessage);

            GetArtWorkResponse response = _mapper.Map<GetArtWorkResponse>(artwork);
            return response;
        }

        public async Task<IPaginate<GetArtWorkResponse>> GetArtWorks(ArtWorkFilter filter, PagingModel pagingModel)
        {
            IPaginate<GetArtWorkResponse> response = await _unitOfWork.GetRepository<Artwork>().GetPagingListAsync(
                selector: x => _mapper.Map<GetArtWorkResponse>(x),
                filter: filter,
                orderBy: x => x.OrderBy(x => x.Name),
                page: pagingModel.page,
                size: pagingModel.size
                );
            return response;
        }

        public async Task<bool> UpdateArtWorkInfo(int artWorkID, UpdateArtWorkRequest request)
        {
            _logger.LogInformation($"Start updating product: {artWorkID}");


            Artwork updateArtWork = await _unitOfWork.GetRepository<Artwork>().SingleOrDefaultAsync(
                predicate: x => x.IdArtwork.Equals(artWorkID))
                ?? throw new BadHttpRequestException(MessageConstant.ArtWork.ArtWorkNotFoundMessage);

            updateArtWork.Name = string.IsNullOrEmpty(request.Name) ? updateArtWork.Name : request.Name;
            updateArtWork.Owner = request.Owner;
            updateArtWork.Price = request.Price;
            updateArtWork.Status = request.Status;
            updateArtWork.Owner = request.Owner;
            _unitOfWork.GetRepository<Artwork>().UpdateAsync(updateArtWork);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<bool> DeleteArtWork(int artWorkId)
        {
            Artwork artWork = await _unitOfWork.GetRepository<Artwork>().SingleOrDefaultAsync(
                predicate: x => x.IdArtwork.Equals(artWorkId))
                ?? throw new BadHttpRequestException(MessageConstant.ArtWork.ArtWorkNotFoundMessage);

            artWork.Status = "Inactive";

            _unitOfWork.GetRepository<Artwork>().UpdateAsync(artWork);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

      
    }
}
