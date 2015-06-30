using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmn = Domain;
using Dto = Service.Dto;

namespace Service.Converters
{
    public static class WebsiteConverter
    {
        public static Dto.Website ToDto(Dmn.Website domain)
        {
            return new Dto.Website()
            {
                Id = domain.Id,
                Name = domain.Name,
                Url = domain.Url
            };
        }

        public static IEnumerable<Dto.Website> ToDto(IEnumerable<Dmn.Website> domain)
        {
            return domain.Select(i => ToDto(i));
        }

        public static Dmn.Website ToNewDmn(Dto.Website dataObject)
        {
            return new Dmn.Website(dataObject.Name, dataObject.Url);
        }
    }
}
