
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noti_service.Model
{
    [Table("tb_noti")]
    public class SqlNoti
    {
        [Key]
        public long ID { get; set; }

        public string time { get; set; } = DateTime.Now.Ticks.ToString();
        public string body { get; set; }

        public SqlUser user { get; set; }

    }
}
