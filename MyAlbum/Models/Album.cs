//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyAlbum.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Album
    {
        public int SN { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Nullable<int> Permission { get; set; }
        public string Path { get; set; }
        public string download { get; set; }
        public string sctrl { get; set; }
        public string remark { get; set; }
        public string cruser { get; set; }
        public Nullable<System.DateTime> crdate { get; set; }
        public string eduser { get; set; }
        public Nullable<System.DateTime> eddate { get; set; }
    }
}
