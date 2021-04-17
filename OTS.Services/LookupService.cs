using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OTS.Models;
using OTS.Repositories.Interfaces;
using OTS.Services.Common;
using OTS.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace OTS.Services
{
    public class LookupService : ILookupService
    {
        private readonly ILookupRepository _lookupRepository;
        private readonly ISaveRepository _unitOfWork;
        private readonly IMemoryCache _cache;
        private readonly ILogger<LookupService> _logger;

        public LookupService(
            ILookupRepository lookupsRepository
            , ISaveRepository unitOfWork
            , IMemoryCache cache
            , ILogger<LookupService> logger)
        {
            _lookupRepository = lookupsRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
            _logger = logger;
        }

        public async Task<IEnumerable<Lookup>> ListAsync()
        {
            // Get the Search Engines list from the memory cache.
            // If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire 12 hours ahead and
            // returning the Task that lists the Search Engines from the repository.
            var payload = await _cache.GetOrCreateAsync(CacheKeys.SearchEngines, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12);

                return _lookupRepository.ListAsync();
            });

            return payload;
        }

        public async Task<Response<Lookup>> FindByIdAsync(int id)
        {
            var payload = await _lookupRepository.FindByIdAsync(id);

            return payload == null ? 
                new Response<Lookup>("Search engine not found.") : 
                new Response<Lookup>(payload);
        }

        public async Task<Response<Lookup>> SaveAsync(Lookup payload)
        {
            try
            {
                _lookupRepository.Add(payload);

                await _unitOfWork.CompleteAsync();

                return new Response<Lookup>(payload);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(LookupService)} {nameof(SaveAsync)} {ex.Message}");

                return new Response<Lookup>(ex.Message);
            }
        }

        public async Task<Response<Lookup>> UpdateAsync(int id, Lookup payload)
        {
            var existingLookup = await _lookupRepository.FindByIdAsync(id);

            if (existingLookup == null)
            {
                return new Response<Lookup>("Search engine not found.");
            }

            existingLookup.Name = payload.Name;

            try
            {
                _lookupRepository.Update(existingLookup);

                await _unitOfWork.CompleteAsync();

                return new Response<Lookup>(existingLookup);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(LookupService)} {nameof(UpdateAsync)} {ex.Message}");

                return new Response<Lookup>(ex.Message);                
            }
        }

        public async Task<Response<Lookup>> DeleteAsync(int id)
        {
            var existingLookup = await _lookupRepository.FindByIdAsync(id);

            if (existingLookup == null)
            {
                return new Response<Lookup>("Search engine not found.");
            }

            try
            {
                _lookupRepository.Remove(existingLookup);

                await _unitOfWork.CompleteAsync();

                return new Response<Lookup>(existingLookup);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(LookupService)} {nameof(DeleteAsync)} {ex.Message}");

                return new Response<Lookup>(ex.Message);
            }
        }
    }
}
