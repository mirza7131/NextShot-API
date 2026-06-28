using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Indicator
{
    public class IndicatorAddDTO
    {
        public IndicatorAddDTO()
        {
            FormSubIndicator = new List<IndicatorAddDTO>();
            FormIndicatorOptions = new List<IndicatorOptionDTO>();
        }
        public int IndicatorId { get; set; }
        public int? SequenceNo { get; set; }
        public int CategoryId { get; set; }
        public int ApplicationTypeId { get; set; }
        public int ModuleId { get; set; }
        public int? ParentIndicatorId { get; set; }
        public int? SubCategoryId { get; set; }
        public List<int> HfTypes { get; set; }
        public List<int> Shifts { get; set; }
        public List<int> Hf { get; set; }
        public string Question { get; set; }
        public int QuestionType { get; set; }
        public int? ShowInCase { get; set; }
        public int? ShowRemarksInCase { get; set; }
        public string FormType { get; set; }
        public Boolean? IsRemarkShow { get; set; }
        public Boolean? IsRemarksMandatory { get; set; }
        public Boolean? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int InputTypeId { get; set; }
        public bool IsOptionTotal { get; set; }
        public bool IsOptionCalculation { get; set; }
        public bool IsOptionTagged { get; set; }
        public bool IsOptionEditable { get; set; }
        public bool IsRequired { get; set; }
        public bool IsCalculation { get; set; }
        public string DefaultValue { get; set; }
        public List<IndicatorOptionDTO> FormIndicatorOptions { get; set; }
        public List<IndicatorAddDTO> FormSubIndicator { get; set; }
    }
    public class IndicatorOptionDTO
    {
        public int OptionIndicatorId { get; set; }
        public int? IndicatorId { get; set; }
        public int OptionType { get; set; }
        public bool IsOptionTotal { get; set; }
        public bool IsOptionCalculation { get; set; }
        public bool IsOptionTagged { get; set; }
        public string IndicatorOptionName { get; set; }
        public bool IsOptionEditable { get; set; }
        public bool? IsActive { get; set; }
        public bool ShowRemarksIfSelect { get; set; }
        public bool ShowSubIndicatorIfSelect { get; set; }
        public string DefaultValue { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int InputTypeId { get; set; }
    }
}
