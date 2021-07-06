using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace MidDosyaYonetim.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    [NonPersistent]
    public class SartnameFile : BaseObject
    {
        private IObjectSpace objectSpace;
        public SartnameFile(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private FileData file;
        [DevExpress.Xpo.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData File
        {
            get { return file; }
            set
            {
                SetPropertyValue("File", ref file, value);
            }
        }
    }
}