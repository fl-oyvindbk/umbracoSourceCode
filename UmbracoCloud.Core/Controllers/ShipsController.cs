using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Umbraco.Web.WebApi;
using UmbracoCloud.Core;
using Umbraco.Web;
using System.Diagnostics;

namespace UmbracoCloud.Core.Controllers
{
    [Route("api/[controller]")]
    public class ShipsController : UmbracoApiController
    {
        /*
        [HttpGet]
        public string GetAllShips()
        {

            return "All those ships";

          //  return new[] { "NB87", "BF", "FC", "OF" };
        }
        */
        public String GetShip()
        {
            string ship = "BF";

            return ship;
        }

        public IEnumerable<Comment> GetAllShipsTest()
        {
            // Create an UmbracoHelper for retrieving published content
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            // Get all comments from the Umbraco tree (this is not a optimized way of doing this, since it queries the complete Umbraco tree)
            var ships = umbracoHelper.TypedContentAtRoot().DescendantsOrSelf("ships");
            Debug.Write(ships);

            // Map the found nodes from IPublishedContent to a strongly typed object of type Comment (defined below)
            var mappedComments = ships.Select(x => new Comment
            {
                Name = x.Name                              // Map name of the document
                //Code = x.ShipCode,                          
                //Text = x.GetPropertyValue<string>("ShipDescription")   // Map custom property "text"
            });

            return mappedComments;
        }

        public class Comment
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Text { get; set; }
        }

        public List<NewsItem> GetallShips()
        {
            var newsItems = new List<NewsItem>();
            var items = Umbraco.ContentAtXPath("//ship").Where("Visible").OrderBy("createDate desc");
           

            foreach (var item in items)
            {
                var newsItem = new NewsItem();
                newsItem.Headline = item.GetPropertyValue("newsItemHeadline").ToString();
                newsItem.Body = item.GetPropertyValue("newsItemBody").ToString();
                newsItem.CreateDate = Convert.ToDateTime(item.CreateDate);
                newsItems.Add(newsItem);
                Debug.Write(newsItem.Headline);
            }
            return newsItems;
        }


        

        public class NewsItem
        {
            public string Headline { get; set; }
            public string Body { get; set; }
            public DateTime CreateDate { get; set; }
        }
    }
}
