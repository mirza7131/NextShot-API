using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Indicator
{
    public class ViewIndicatorDTO
    {
        public ViewIndicatorDTO()
        {
            SubCategories = new List<SubCategoriesIndicatorDTO>();
            Indicators = new List<IndicatorDTO>();
        }
        public int CaegoryId { get; set; }
        public string CategoryName { get; set; }
        public int ApplicationType { get; set; }
        public int ModuleId { get; set; }
        public int HfTypeId { get; set; }
        public int ShiftId { get; set; }
        public string HfId { get; set; }
        public string FormId { get; set; }
        public List<SubCategoriesIndicatorDTO> SubCategories { get; set; }
        public List<IndicatorDTO> Indicators { get; set; }
    }
    public class CategoryIndicatorDTO
    {
        public CategoryIndicatorDTO()
        {
            //SubCategories = new List<SubCategoriesIndicatorDTO>();
            Indicators = new List<IndicatorDTO>();
        }
        public int CaegoryId { get; set; }
        public string CategoryName { get; set; }

        public bool? IsRequired { get; set; }
        public int ApplicationType { get; set; }
        public int ModuleId { get; set; }
        public int SequenceNo { get; set; }
        public List<IndicatorDTO> Indicators { get; set; }
    }
    public class SubCategoriesIndicatorDTO
    {
        public SubCategoriesIndicatorDTO()
        {
            Indicators = new List<IndicatorDTO>();
        }
        public int CategoryId { get; set; }
        public int SubCaegoryId { get; set; }
        public bool? IsRequired { get; set; }
        public string SubCategoryName { get; set; }
        public List<IndicatorDTO> Indicators { get; set; }
    }
    public class IndicatorDTO
    {
        public IndicatorDTO()
        {
            SubIndicators = new List<IndicatorDTO>();
            Shifts = new List<int>();
            HFTypes = new List<int>();
            HFs = new List<int>();
            Options = new List<OptionIndicatorDTO>();
        }
        public string AnswerId { get; set; }
        public string Answer { get; set; }
        public int CategoryId { get; set; }
        public int IndicatorId { get; set; }
        public string Question { get; set; }
        public int? showInCase { get; set; }
        public int? showRemarksInCase { get; set; }
        public int? ParentIndicatorId { get; set; }
        public int CategorySequenceNo { get; set; }
        public int SequenceNo { get; set; }
        public string OptionsType { get; set; }
        public string Form { get; set; }
        public bool IsOptionTotal { get; set; }
        public bool IsOptionCalculation { get; set; }
        public bool IsOptionEditable { get; set; }
        public bool IsOptionTagged { get; set; }
        public string DefaultValue { get; set; }
        public bool IsRequired { get; set; }
        public bool IsCalculation { get; set; }
        public bool IsPhysicalView { get; set; }
        public string InputType { get; set; }
        public string remarkValue { get; set; }
        public string selectedValue { get; set; }
        public List<OptionIndicatorDTO> Options { get; set; }
        public List<int> Shifts { get; set; }
        public List<int> HFTypes { get; set; }
        public List<int> HFs { get; set; }
        public List<IndicatorDTO> SubIndicators { get; set; }
    }


    public class FloodIndicatorDTO
    {
        public FloodIndicatorDTO()
        {
            SubIndicators = new List<FloodIndicatorDTO>();
            Options = new List<OptionIndicatorDTO>();
        }
        public string AnswerId { get; set; }
        public string Answer { get; set; }
        public int CategoryId { get; set; }
        public int IndicatorId { get; set; }
        public string Question { get; set; }
        public int? showInCase { get; set; }
        public int? showRemarksInCase { get; set; }
        public int? ParentIndicatorId { get; set; }
        public int CategorySequenceNo { get; set; }
        public int SequenceNo { get; set; }
        public string OptionsType { get; set; }
        public string Form { get; set; }
        public bool IsOptionTotal { get; set; }
        public bool IsOptionCalculation { get; set; }
        public bool IsOptionEditable { get; set; }
        public bool IsOptionTagged { get; set; }
        public string DefaultValue { get; set; }
        public bool IsRequired { get; set; }
        public bool IsCalculation { get; set; }
        public bool IsPhysicalView { get; set; }
        public string InputType { get; set; }
        public string remarkValue { get; set; }
        public string selectedValue { get; set; }
        public List<OptionIndicatorDTO> Options { get; set; }

        public List<FloodIndicatorDTO> SubIndicators { get; set; }
    }

    public class OptionIndicatorDTO
    {
        public int OptionId { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public bool IsOptionTotal { get; set; }
        public bool IsOptionCalculation { get; set; }
        public bool IsOptionEditable { get; set; }
        public bool IsOptionTagged { get; set; }
        public string DefaultValue { get; set; }
        public string InputType { get; set; }
    }
    public class ShiftIndicatorDTO
    {
        public int ShiftId { get; set; }
        //  public string ShiftName { get; set; }
    }
    public class HFTypeIndicatorDTO
    {
        public int HFTypeId { get; set; }
        // public string HFTypeName { get; set; }
    }
    public class HFIndicatorDTO
    {
        public int HFId { get; set; }
        // public string HFName { get; set; }
    }
}
