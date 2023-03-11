using Domain.Utilities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Page.Commands.Create
{
    public class CreatePageCommand : IRequest<IResult>
    {
        public string Content { get;  set; }
        public string Header { get;  set; }
        public string PageDescription { get;  set; }
        public string PageSeoTag { get;  set; }
        public string PageSeoDescription { get;  set; }
    }
}
