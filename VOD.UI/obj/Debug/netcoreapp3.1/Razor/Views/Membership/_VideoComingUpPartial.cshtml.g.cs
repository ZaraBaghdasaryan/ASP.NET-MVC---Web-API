#pragma checksum "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9c7e7bd07e04b3da5219c0687d4a67a3cfaa979a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Membership__VideoComingUpPartial), @"mvc.1.0.view", @"/Views/Membership/_VideoComingUpPartial.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 2 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\_ViewImports.cshtml"
using VOD.UI;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\_ViewImports.cshtml"
using VOD.UI.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\_ViewImports.cshtml"
using VOD.Common.Entities;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\_ViewImports.cshtml"
using VOD.Common.DTOModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\_ViewImports.cshtml"
using VOD.UI.Models.MembershipViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9c7e7bd07e04b3da5219c0687d4a67a3cfaa979a", @"/Views/Membership/_VideoComingUpPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"da674a10e8ccbc6e1c92bee4c90a008a0a1e8e26", @"/Views/_ViewImports.cshtml")]
    public class Views_Membership__VideoComingUpPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LessonInfoDTO>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-default"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
 if (Model.PreviousVideoId > 0 || Model.NextVideoId > 0)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"card coming-up no-border-radius\">\r\n");
#nullable restore
#line 6 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
     if (Model.NextVideoId == 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<img");
            BeginWriteAttribute("src", " src=\"", 175, "\"", 209, 1);
#nullable restore
#line 7 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
WriteAttributeValue("", 181, Model.CurrentVideoThumbnail, 181, 28, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("\r\n          class=\"img-responsive\">\r\n");
#nullable restore
#line 9 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(" <img");
            BeginWriteAttribute("src", " src=\"", 274, "\"", 305, 1);
#nullable restore
#line 11 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
WriteAttributeValue("", 280, Model.NextVideoThumbnail, 280, 25, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-responsive\">\r\n");
#nullable restore
#line 12 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"card-body\">\r\n");
#nullable restore
#line 14 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
     if (Model.NextVideoId == 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>COURSE COMPLETED</p> <h5>");
#nullable restore
#line 16 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
                               Write(Model.CurrentVideoTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n");
#nullable restore
#line 17 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p>COMING UP</p> <h5>");
#nullable restore
#line 20 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
                        Write(Model.NextVideoTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n");
#nullable restore
#line 21 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"btn-group\" role=\"group\">\r\n\r\n");
#nullable restore
#line 24 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
     if (Model.PreviousVideoId == 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <a class=\"btn\" disabled>Previous</a>\r\n");
#nullable restore
#line 27 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9c7e7bd07e04b3da5219c0687d4a67a3cfaa979a8193", async() => {
                WriteLiteral("\r\n            Previous\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 756, "~/Membership/Video/", 756, 19, true);
#nullable restore
#line 30 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
AddHtmlAttributeValue("", 775, Model.PreviousVideoId, 775, 22, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 33 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 35 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
     if (Model.NextVideoId == 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <a class=\"btn\" disabled>Next</a>\r\n");
#nullable restore
#line 38 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9c7e7bd07e04b3da5219c0687d4a67a3cfaa979a10679", async() => {
                WriteLiteral("Next");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 994, "~/Membership/Video/", 994, 19, true);
#nullable restore
#line 41 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
AddHtmlAttributeValue("", 1013, Model.NextVideoId, 1013, 18, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
#nullable restore
#line 42 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n</div>\r\n</div>\r\n");
#nullable restore
#line 46 "C:\Users\Admin\OneDrive\Desktop\Studier\MVC\VideoOnDemand\VOD.UI\Views\Membership\_VideoComingUpPartial.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LessonInfoDTO> Html { get; private set; }
    }
}
#pragma warning restore 1591
