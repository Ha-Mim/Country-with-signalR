using System.ComponentModel;

namespace CountryWithSignalR.Controllers
{
    public class City
    {
        public int Id { set; get; }
       
        public string Name { set; get; }
       
        public string About { set; get; }
        [DisplayName("No of dwellers")]
        public int NoOfDwellers { set; get; }
        public string Location { set; get; }
        public string Weather { set; get; }
        public int CountryId { set; get; }
        public Country ACountry { set; get; }
    }
}