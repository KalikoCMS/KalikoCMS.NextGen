using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KalikoCMS.Services.Interfaces;

namespace KalikoCMS.Services {
    public class DemoService : IDemoService {
        public string HelloWorld() {
            return "This is a demo service!";
        }
    }
}
