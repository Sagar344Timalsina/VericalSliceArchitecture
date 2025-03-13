using System.ComponentModel.DataAnnotations.Schema;

namespace verticalSliceArchitecture.Domain
{
    [Table("tblRoles")]
    public class Role:EntityBase
    {
        public string Name { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public Role() { }

        public Role(string name)
        {
            Name = name;
        }
    }
}
