#if NETCORE
using Microsoft.AspNetCore.Mvc;

namespace KalikoCMS.Mvc.Controllers {
    public class CmsMessageController : Controller {
        public ActionResult Startup() {
            return Content("<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\"><meta http-equiv=\"refresh\" content=\"5\"><title>System is starting up</title></head><body>Please wait while the system starts up, this page will reload..</body></html>", "text/html");
        }
    }
}

#endif