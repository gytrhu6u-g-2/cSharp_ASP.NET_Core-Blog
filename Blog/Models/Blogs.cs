using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
	public class Blogs
	{
		public Blogs()
		{
		}
        public int Id { get; set; }
        [DisplayName("名前")]
        public string Name { get; set; }
        [DisplayName("内容")]
        public string Content { get; set; }
        [DisplayName("タイトル")]
        public string Title { get; set; }
        [DisplayName("作成日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created_date { get; set; }
        [DisplayName("更新日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Updated_date { get; set; }
    }
}

