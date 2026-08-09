using IDP.Domain.DTO;
using IDP.Domain.IRepositories.Commands;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace IDP.Infra.Repositories.Commands
{
    public class OtpRedisRepository(IDistributedCache _distributedCache, IConfiguration _configuration) : IOtpRedisRepository
    {
      
        public async Task<bool> Delete(OTP   entity)
        {
            _distributedCache.RemoveAsync(entity.UserName.ToString());
            return true;
        }

        public async Task<OTP> Getdata(string mobile)
        {
            var data = _distributedCache.GetString(mobile);
            if (data == null) return null;
            var otpobj = JsonConvert.DeserializeObject<OTP>(data);
            return otpobj;
        }

        public async Task<OTP> Insert(OTP entity)
        {
            int time = Convert.ToInt32(_configuration.GetSection("Otp:OtpTime").Value);
            _distributedCache.SetString(entity.UserName.ToString(), JsonConvert.SerializeObject(entity), new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(time)).SetAbsoluteExpiration(TimeSpan.FromMinutes(time)));

            return null;
        }

        public Task<bool> Update(OTP entity)
        {
            throw new NotImplementedException();
        }
    }
}
