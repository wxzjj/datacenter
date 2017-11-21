
namespace WxjzgcjczyTimerService.model
{
    public class Qyxx
    {
        //企业从事业务类型ID
        
        public string csywlxID { get; set; }
        //企业从事业务类型
        public string csywlx { get; set; }

        //企业证书类型
        public string zslx { get; set; }
        //企业证书类型ID
        public string zslxID { get; set; }

        public Qyxx()
        {
            csywlxID = string.Empty;
            csywlx = string.Empty;
            zslx = string.Empty;
            zslxID = string.Empty; 
        }
    }
}
