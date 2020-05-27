
namespace WxjzgcjczyTimerService.model
{
    public class ReturnBean
    {
       
        public int SuccessSize { get; set; }
        
        public string code { get; set; }

        public string msg { get; set; }
        
        public int size { get; set; }

        public ReturnBean()
        {
            SuccessSize = 0;
            code = string.Empty;
            msg = string.Empty;
            size = 0; 
        }
    }
}
