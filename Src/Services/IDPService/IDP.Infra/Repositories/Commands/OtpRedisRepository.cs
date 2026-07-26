using IDP.Domain.DTO;
using IDP.Domain.IRepositories.Commands;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace IDP.Infra.Repositories.Commands
{
    public class OtpRedisRepository(IDistributedCache _distributedCache, IConfiguration _configuration) : IOtpRedisRepository
    {
        public async Task<bool> Delete(OTP entity)
        {
            _distributedCache.RemoveAsync(entity.UserId.ToString());
            return true;
        }

        public async Task<bool> Insert(OTP entity)
        {
            int time = Convert.ToInt32(_configuration.GetSection("Otp:OtpTime").Value);

            _distributedCache.SetString(entity.UserId.ToString(), JsonSerializer.Serialize(entity),
                new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(time)).SetAbsoluteExpiration(TimeSpan.FromMinutes(time)));

            return true;
        }

        public Task<bool> Update(OTP entity)
        {
            throw new NotImplementedException();
        }
    }
}
