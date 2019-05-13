using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    using Entity.Attributes;
    using DataFramework.Class;

    [Table("Member")]
    public class Member : Class.BaseClass
    {

        [Field("Member_ID", IsPrimaryKey = true)]
        public Guid Member_ID { get; set; }

        [CSetNumber(0)]
        [Field("编号")]
        public string Member_Num { get; set; }

        [CRequired(ErrorMessage = "{name}不能为空")]
        [Field("会员名称")]
        public string Member_Name { get; set; }

        [Field("会员电话")]
        public int? Member_Phone { get; set; }

        [Field("性别")]
        public string Member_Sex { get; set; }

        [Field("生日")]
        public DateTime? Member_Birthday { get; set; }

        [Field("头像")]
        public string Member_Photo { get; set; }

        [Field("帐户ID")]
        public Guid? Member_UserID { get; set; }

        [Field("介绍")]
        public string Member_Introduce { get; set; }

        [Field("文件")]
        public string Member_FilePath { get; set; }

        [Field("创建时间", IsIgnore = true)]
        public DateTime? Member_CreateTime { get; set; }


    }
}
