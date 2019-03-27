using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Data;
using Utility.Extensions;
using Utility.EntityFramework.Extensions;
using Utility.Framework.Test.Datas;

namespace Utility.Framework.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork<IMasterDbContext> _unitOfWorkMaster;
        private readonly IUnitOfWork<ISlaveDbContext> _unitOfWorkSlave;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWorkMaster"></param>
        public ValuesController(IUnitOfWork<IMasterDbContext> unitOfWorkMaster,
            IUnitOfWork<ISlaveDbContext> unitOfWorkSlave)
        {
            _unitOfWorkMaster = unitOfWorkMaster;
            _unitOfWorkSlave = unitOfWorkSlave;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            var role = new SysRole
            {
                Name = "Role Test",
                Description = "123456"
            };
            var infos = _unitOfWorkMaster.GetRepository<SysUser, Guid>()
                .GetAllAsNoTracking()
                .Select<UserInfor>()
                .ToList();

            if (infos.Any())
            {

            }

            var users = _unitOfWorkMaster.GetRepository<SysUser, Guid>().GetAllAsNoTracking().ToArray();
            var us = new List<object>();
            foreach (var u in users)
            {
                _unitOfWorkMaster.GetRepository<SysUser, Guid>().SetValues(u.Id,
                    new
                    {
                        CreatedBy = $"CreatedBy {id}",
                        UpdateBy = $"Update {id}",
                        RoleId = role.Id
                    });
            }
            //_unitOfWorkMaster.GetRepository<SysUser, Guid>().UpdateRange(users);
            var result = _unitOfWorkMaster.Commit() > 0;

            if (result)
            {
                return "添加成功！";
            }
            return "添加失败~";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
