using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMSSolution.Application.DataTransferObjects.Admin.Request
{
    public class ChangeUserRoleRequestDto
    {
        public Guid UserId { get; set; }
        public Guid NewRoleId { get; set; }
    }
}
