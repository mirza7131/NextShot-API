using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class Menu
{
    public Guid MenuId { get; set; }

    public Guid? ModuleId { get; set; }

    public string? Name { get; set; }

    public string? DisplayName { get; set; }

    public string? Url { get; set; }

    public string? ImageUrl { get; set; }

    public string? Icon { get; set; }

    public int? SeqNo { get; set; }

    public Guid? ParentId { get; set; }

    public bool? IsApi { get; set; }

    public bool? IsLabel { get; set; }

    public bool? IsModule { get; set; }

    public bool IsDisplayMenu { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public long? UserLogId { get; set; }

    public byte ActionTypeId { get; set; }

    public virtual ICollection<RoleMenu> RoleMenus { get; } = new List<RoleMenu>();
}
