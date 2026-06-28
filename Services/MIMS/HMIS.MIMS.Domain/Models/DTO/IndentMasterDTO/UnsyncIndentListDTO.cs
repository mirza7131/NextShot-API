using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO
{
    public class UnsyncIndentListDTO
    {
        public int IndentId { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool IsSync { get; set; }

        public class ResponseUnSyncIndentListDto
        {
            public string? Message { get; set; }
            public bool Status { get; set; }
            public List<UnsyncIndentListDTO> Data { get; set; }

        }
    }
}